using System.Diagnostics;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Workflow.Impl
{
    public class CallProgram : Node
    {
        private readonly string _programPath;
        private readonly string _arguments;

        public string WorkDirectory { get; set; }


        public CallProgram([NotNull] string programPath, string arguments)
            : base($"{programPath} {arguments}")
        {
            _programPath = programPath;
            _arguments = arguments;
        }

        public override int Execute(SharedVariables sharedVariables)
        {
            var arguments = _arguments;

            Logger.Info($"/> Workflow.CallProgram: [{_programPath}] [{_arguments}]");


            if (string.IsNullOrEmpty(_programPath))
            {
                ExitWithError("!> Workflow.CallProgram: programPath can NOT be empty");
                return 1;
            }

            var programPath = _programPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            var workingDirectory = WorkDirectory;
            if (string.IsNullOrEmpty(workingDirectory))
            {
                workingDirectory = Path.GetDirectoryName(programPath) ?? "";
            }

            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            startInfo.FileName = programPath;
            startInfo.WorkingDirectory = workingDirectory;
            startInfo.Arguments = arguments;

            startInfo.StandardErrorEncoding = Encoding.UTF8;
            startInfo.StandardOutputEncoding = Encoding.UTF8;

            var process = Process.Start(startInfo);
            process.WaitForExit();

            var output = new StringBuilder();

            output.Append(process.StandardOutput.ReadToEnd());
            output.Append(process.StandardError.ReadToEnd());


            ExitCode = process.ExitCode;

            sharedVariables?.SetValue(Name, output.ToString());

            DumpOutput(ExitCode, output.ToString());

            if (ExitCode != 0)
            {
                Logger.Error(output.ToString());
                Logger.Error($" exit code {ExitCode}: [{programPath}] [{arguments}]");
            }
            else
            {
                Logger.Info(output.ToString());
                Logger.Info($"/> exit code 0: [{programPath}] [{arguments}]");
            }

            process.Close();

            return ExitCode;
        }

        public virtual void DumpOutput(int exitCode, string text)
        {
        }
    }
}
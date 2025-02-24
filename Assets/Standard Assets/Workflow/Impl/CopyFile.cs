using System;
using System.IO;
using System.Text;

namespace Workflow.Impl
{
    public class CopyFile : Node
    {
        private enum Method
        {
            File2File,
            Folder2Folder,
            Pattern2Folder
        }

        private Method _method;
        private string _src;
        private string _dst;
        private string _pattern;

        private CopyFile(string name)
            : base(name)
        {
        }

        public static CopyFile File2File(string src, string dst)
        {
            var cp = new CopyFile($"{src} {dst}");
            cp._method = Method.File2File;
            cp._src = src;
            cp._dst = dst;
            return cp;
        }

        public static CopyFile Folder2Folder(string src, string dst)
        {
            var cp = new CopyFile($"{src} {dst}");
            cp._method = Method.Folder2Folder;
            cp._src = src;
            cp._dst = dst;
            return cp;
        }

        public static CopyFile Pattern2Folder(string src, string pattern, string dst)
        {
            var cp = new CopyFile($"{src} {pattern} {dst}");
            cp._method = Method.Pattern2Folder;
            cp._src = src;
            cp._dst = dst;
            cp._pattern = pattern;
            return cp;
        }

        public override int Execute(SharedVariables sharedVariables)
        {
            switch (_method)
            {
                case Method.File2File:
                    ExecuteFile2File();
                    break;
                case Method.Folder2Folder:
                    ExecuteFolder2Folder("*.*");
                    break;
                case Method.Pattern2Folder:
                    ExecuteFolder2Folder(_pattern);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return ExitCode;
        }


        private void ExecuteFile2File()
        {
            if (!File.Exists(_src))
            {
                ExitWithError($"/> Workflow.CopyFile: file NOT exists [{_src}] ");
                return;
            }

            try
            {
                CreateFileDirectory(_dst);
                File.Copy(_src, _dst, true);
                Logger.Info($"/> Workflow.CopyFile: [{_src}] to [{_dst}]");
            }
            catch (Exception e)
            {
                ExitWithError($"!> Workflow.CopyFile: error: {e.Message} ");
            }
        }

        private void ExecuteFolder2Folder(string pattern)
        {
            if (!Directory.Exists(_src))
            {
                Logger.Warn($"Workflow.CopyFile: src NOT exists [{_src}].");
                return;
            }

            try
            {
                var inf = new StringBuilder();

                var files = Directory.GetFiles(_src, pattern, SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var rel = file.Replace(_src, "");
                    var dst = Path.GetFullPath(Path.Combine(_dst, rel));
                    CreateFileDirectory(dst);
                    File.Copy(file, dst, true);
                    inf.AppendLine(dst);
                }

                inf.AppendLine($"\ntotal files: {files.Length}");
                Logger.Info(inf.ToString());
            }
            catch (Exception e)
            {
                ExitWithError($"!> Workflow.CopyFile: error: {e.Message} ");
            }
        }

        private string CreateFileDirectory(string filepath)
        {
            var dir = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return dir;
        }
    }
}
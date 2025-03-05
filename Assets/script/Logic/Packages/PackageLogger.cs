using System;
using YooAsset;

namespace GoFire
{
    public class PackageLogger : ILogger
    {
        public void Log(string message)
        {
            GoFire.Kernel.Log.Info(message);
        }

        public void Warning(string message)
        {
            GoFire.Kernel.Log.Warning(message);
        }

        public void Error(string message)
        {
            GoFire.Kernel.Log.Error(message);
        }

        public void Exception(Exception exception)
        {
            GoFire.Kernel.Log.Error(exception.Message);
        }
    }
}
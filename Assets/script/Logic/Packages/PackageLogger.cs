using System;
using YooAsset;

namespace GoFire
{
    public class PackageLogger : ILogger
    {
        public void Log(string message)
        {
            GoFire.Log.Info(message);
        }

        public void Warning(string message)
        {
            GoFire.Log.Warning(message);
        }

        public void Error(string message)
        {
            GoFire.Log.Error(message);
        }

        public void Exception(Exception exception)
        {
            GoFire.Log.Error(exception.Message);
        }
    }
}
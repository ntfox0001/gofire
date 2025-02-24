using System;

namespace Workflow
{
    public static class Logger
    {
        public static Action<string> Info;
        public static Action<string> Error;
        public static Action<string> Warn;
    }
}
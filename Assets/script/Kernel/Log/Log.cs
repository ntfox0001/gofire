using System;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using Logger = NLog.Logger;

namespace GoFire.Kernel
{
    public class Log : Singleton<Log>, IManager
    {
        public string fluentdUrl = "tcp://127.0.0.1:24224";
        
        private Logger _logger;

        public void Init()
        {
            var config = new LoggingConfiguration();
            // 创建一个 Fluentd 目标
            var fluentdTarget = new Fluentd();
            fluentdTarget.Name = "GoFire";
            fluentdTarget.Layout = new SimpleLayout("${message:withexception=true}");
            
            config.AddTarget(fluentdTarget);
            // 创建一个日志规则，将所有日志记录到 Fluentd
            var rule = new LoggingRule("*", LogLevel.Trace, fluentdTarget);
            config.LoggingRules.Add(rule);
            // 将配置应用到 LogManager
            LogManager.Configuration = config;

            // 初始化 Logger
            _logger = LogManager.GetCurrentClassLogger();

            // 检查 Fluentd 连接是否成功
            try
            {
                _logger.Trace("Testing connection to Fluentd");
            }
            catch (Exception ex)
            {
                // 如果连接不成功，直接返回
                return;
            }
        }

        public static void Debug(string message)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.Log(message);
                return;
            }
            GetSingleton()._logger.Debug(message);
        }

        public static void Debug(string format, params object[] args)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogFormat(format, args);
                return;
            }
            GetSingleton()._logger.Debug(format, args);
        }

        public static void Info(string message)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.Log(message);
                return;
            }
            GetSingleton()._logger.Info(message);
        }

        public static void Info(string format, params object[] args)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogFormat(format, args);
                return;
            }
            GetSingleton()._logger.Info(format, args);
        }

        public static void Warning(string message)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogWarning(message);
                return;
            }
            GetSingleton()._logger.Warn(message);
        }

        public static void Warning(string format, params object[] args)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogWarningFormat(format, args);
                return;
            }
            GetSingleton()._logger.Warn(format, args);
        }

        public static void Error(string message)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogError(message);
                return;
            }
            GetSingleton()._logger.Error(message);
        }

        public static void Error(string format, params object[] args)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogErrorFormat(format, args);
                return;
            }
            GetSingleton()._logger.Error(format, args);
        }

        public static void Fatal(string message)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogError(message);
                return;
            }
            GetSingleton()._logger.Fatal(message);
        }

        public static void Fatal(string format, params object[] args)
        {
            if (GetSingleton()._logger == null)
            {
                UnityEngine.Debug.LogErrorFormat(format, args);
                return;
            }
            GetSingleton()._logger.Fatal(format, args);
        }

        public void Update()
        {
            
        }

        public void Release()
        {
            
        }
    }
}
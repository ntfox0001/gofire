using System;
using System.Collections;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using Logger = NLog.Logger;

namespace GoFire.Kernel
{
    public class Log : Singleton<Log>, IManager
    {
        public string fluentdHost = "127.0.0.1";
        public int fluentdPort = 24224;
        
        private Logger _logger;

        public IEnumerator Init()
        {
            var config = new LoggingConfiguration();
            // 创建一个 Fluentd 目标
            var fluentdTarget = new Fluentd();
            fluentdTarget.Name = "GoFire";
            fluentdTarget.Layout = new SimpleLayout("${message:withexception=true}");
            fluentdTarget.Host = fluentdHost;
            fluentdTarget.Port = fluentdPort;
            
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
            catch (Exception)
            {
                // 如果连接不成功，直接返回
            }

            yield return null;
        }

        public static void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Debug(message);
            }
        }

        public static void Debug(string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(format, args);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Debug(format, args);
            }
        }

        public static void Info(string message)
        {
            UnityEngine.Debug.Log(message);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Info(message);
            }
        }

        public static void Info(string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(format, args);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Info(format, args);
            }
        }

        public static void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Warn(message);
            }
        }

        public static void Warning(string format, params object[] args)
        {
            if (GetSingleton()._logger != null)
            {
                UnityEngine.Debug.LogWarningFormat(format, args);
                return;
            }
            GetSingleton()._logger.Warn(format, args);
        }

        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Error(message);
            }
        }

        public static void Error(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Error(format, args);
            }
        }

        public static void Fatal(string message)
        {
            UnityEngine.Debug.LogError(message);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Fatal(message);    
            }
            throw new Exception(message);
        }

        public static void Fatal(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
            if (GetSingleton()._logger != null)
            {
                GetSingleton()._logger.Fatal(format, args);
            }
            throw new Exception(string.Format(format, args));
        }

        public void Update()
        {
            
        }

        public void Release()
        {
            
        }
    }
}
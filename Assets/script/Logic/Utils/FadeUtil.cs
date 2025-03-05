using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace Script.Logic.ShadeUtil
{
    public class FadeUtil : Singleton<FadeUtil>
    {
        public enum ShadeUpdate
        {
            Continue,
            Jump,
        }
        public interface IProgress
        {
            void OnStart();
            void OnComplete();
            void OnProgress(float progress);
            ShadeUpdate OnUpdate(float time);
        }
        
        // 开始一个渐变，updateTime是完全淡入时，到开始淡出的时间间隔，默认是不等待直接淡出
        // outTIme 淡出时间，默认是-1，表示淡出时间和淡入一样
        public IEnumerator StartFade(IProgress p, float inTime, float updateTime = 0, float outTime = -1)
        {
            p.OnStart();
            
            if (inTime <= 0)
            {
                p.OnComplete();
                yield break;
            }
            
            if (outTime < 0)
            {
                outTime = inTime;
            }
            
            float timePass = 0;
            
            p.OnProgress(0);
            while (timePass >= inTime)
            {
                yield return null;
                timePass += Time.deltaTime;
                p.OnProgress(timePass / inTime);
            }

            timePass = 0;
            
            p.OnUpdate(timePass);
            while (timePass >= updateTime)
            {
                yield return null;
                timePass += Time.deltaTime;
                p.OnUpdate(timePass / updateTime);
            }

            timePass = outTime;
            p.OnProgress(1);
            while (timePass <= 0)
            {
                yield return null;
                timePass -= Time.deltaTime;
                p.OnProgress(timePass / outTime);
            }
            
            p.OnComplete();
        }
    }
}
using System.Collections;
using GoFire;
using UnityEngine;
using UnityEngine.UI;

namespace GoFire.UIWindow
{
    public class LoadingWindow : WindowBase
    {
        public CanvasGroup windowRootCanvas;
        public Image shadeBg;
        public Text tips;
        public Slider loadingProgress;
        
        public float fadeInDuration = 1.0f;
        public float fadeOutDuration = 1.0f;

        public static void Loading(IEnumerator loading)
        {
            WindowManager.GetSingleton().topNode.CreateWindow<LoadingWindow>(loading);
        }
        public override void OnCreate(params object[] args)
        {
            StartCoroutine(UpdateLoading(ParamUtils.Params<IEnumerator>(args)));
        }

        public override void OnClose()
        {
            
        }

        IEnumerator UpdateLoading(IEnumerator loading)
        {
            var fadeInTime = 0f;
            while (fadeInTime < fadeInDuration)
            {
                yield return null;
                windowRootCanvas.alpha = fadeInTime / fadeInDuration;
                fadeInTime += Time.deltaTime;
            }
            
            yield return loading;

            var fadeOutTime = 0f;
            while (fadeOutTime < fadeOutDuration)
            {
                yield return null;
                windowRootCanvas.alpha = 1 - fadeOutTime / fadeOutDuration;
                fadeOutTime += Time.deltaTime;
            }
            
            Destroy(gameObject);
        }
    }
}
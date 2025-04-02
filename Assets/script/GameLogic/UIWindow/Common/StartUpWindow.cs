using System.Collections;
using System.Collections.Generic;
using Script.Logic.SceneRoot;
using Script.Logic.ShadeUtil;
using UnityEngine;
using UnityEngine.UI;

namespace GoFire.UIWindow
{
    public class StartUpWindow : WindowBase
    {
        public Image image;
        public CanvasGroup imageAlpha;
        public Image background;
        public ClickEvent clickEvent;
        
        public Sprite[] sprites;
        public float fadeTime;
        public float fadeIntervalTime; // 图像展示时间

        private bool _jumpFadeInterval = false;
        public override void OnCreate(params object[] args)
        {
            WindowManager.GetSingleton().StartUp();
            
            clickEvent.OnClick.AddListener(OnClick);
            StartCoroutine(StartShade());
        }

        public override void OnClose()
        {
            Debug.Log("StartUP end");
        }

        void OnClick()
        {
            _jumpFadeInterval = true;
        }

        IEnumerator StartShade()
        {
            foreach (var sprite in sprites)
            {
                _jumpFadeInterval = false;
                image.sprite = sprite;
                float t = 0;
                while (t <= fadeTime)
                {
                    t += Time.deltaTime;
                    imageAlpha.alpha = Mathf.Lerp(0, 1, t / fadeTime);
                    yield return null;
                }

                t = 0;
                while (t <= fadeIntervalTime && !_jumpFadeInterval)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = fadeTime;
                while (t >= 0)
                {
                    t -= Time.deltaTime;
                    imageAlpha.alpha = Mathf.Lerp(0, 1, t / fadeTime);
                    yield return null;
                }
            }
            yield return null;

            while (ProcedureManager.GetSingleton().InSwitch)
            {
                yield return null;
            }

            BattleStartData battleStartData = new BattleStartData
            {
                Root = SceneRoot.GetSingleton().CreateNode("battle scene node"),
                PlayerSettings = new[]
                {
                    new PlayerSetting
                    {
                        AirplaneName = "plane1",
                        AirplanePackageName = "Airplane",
                        AmmoName = "player_ammo",
                        Input = InputManager.GetSingleton()
                            .GetPlayer1Input(),
                        Speed = 10
                    }
                },
                LandName = "Land_1",
                MainViewName = "MainView",
                UIWindowPackageNames = new[]
                {
                    "UIBattle"
                },
                LandPackageName = "Land",
                MainViewPackageName = "MainView",
                AmmoPackageNames = new[]
                {
                    "Ammo"
                }
            };
            LoadingWindow.Loading(ProcedureManager.GetSingleton().Switch(new BattleProcedure(), battleStartData));
        }
    }
}
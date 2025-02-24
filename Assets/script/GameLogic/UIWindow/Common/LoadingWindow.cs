using GoFire;
using UnityEngine;
using UnityEngine.UI;

namespace GoFire.UIWindow
{
    public class LoadingWindow : WindowBase
    {
        public Image shadeBg;
        public Text tips;
        
        public override void OnCreate(params object[] args)
        {
            
        }

        public override void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }
}
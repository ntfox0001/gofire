using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire{
    public class GameScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        public static int Height
        {
            get
            {
                if (Application.isEditor)
                {
                    return 1920;
                }
                return Screen.height;
            }
        }
        public static int Width
        {
            get
            {
                if (Application.isEditor)
                {
                    return 1080;
                }
                return Screen.width;
            }
        }

        [ContextMenu("tttt")]
        private void Test()
        {
            Debug.Log("==height: " + Screen.height + ", width: " + Screen.width);
            Debug.Log("height: " + Screen.currentResolution.height + ", width: " + Screen.currentResolution.width);

        }
    }

}

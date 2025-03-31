using UnityEngine;

namespace GoFire
{
    public static class ScreenUtils
    {
        public static int Width
        {
            get
            {
                return UnityEngine.Screen.width;
            }
        }
        public static int Height
        {
            get
            {
                return UnityEngine.Screen.height;
            }
        }
        
        public static float Ratio
        {
            get
            {
                return (float)Width / (float)Height;
            }
        }
        static public bool IsPortrait
        {
            get
            {
                if (Screen.height > Screen.width)
                {
                    return true;
                }

                return false;
            }
        }
    }

}

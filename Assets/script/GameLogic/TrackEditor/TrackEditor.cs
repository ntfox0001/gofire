using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class TrackEditor : MonoBehaviour
    {
        public bool showFakeScreen;
        private Vector3[] _screenRangeLine;
        
        public void OnDrawGizmos()
        {
            if (!showFakeScreen)
            {
                return;
            }
            
            var height = GameConfig.CameraSize;
            var rate = GameConfig.ScreenWidth / GameConfig.ScreenHeight;
            var width = height * rate;
            if (_screenRangeLine == null)
            {
                _screenRangeLine = new Vector3[8];
                _screenRangeLine[0] = new Vector3(-width, 0, -height);
                _screenRangeLine[1] = new Vector3(width, 0, -height);
            
                _screenRangeLine[2] = new Vector3(width, 0, height);
                _screenRangeLine[3] = new Vector3(-width, 0, height);
            
                _screenRangeLine[4] = new Vector3(-width, 0, -height);
                _screenRangeLine[5] = new Vector3(-width, 0, height);
            
                _screenRangeLine[6] = new Vector3(width, 0, -height);
                _screenRangeLine[7] = new Vector3(width, 0, height);
            }
            
            Gizmos.DrawLineList(_screenRangeLine);
        }
    }
}
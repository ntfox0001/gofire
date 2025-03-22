namespace GoFire
{
    public class LandTimeProgressHandler
    {
        private AirplaneMarker[] _airplanes;
        private int _idx = 0;
        public void OnProgress(float timeProgress)
        {
            for (int i = _idx; i < _airplanes.Length; i++)
            {
                if (_airplanes[i].TimeProgress < timeProgress)
                {
                    CreatePlane(_airplanes[i]);
                }
                else
                {
                    return;
                }
            }
        }

        void CreatePlane(AirplaneMarker marker)
        {
            
        }
    }
}
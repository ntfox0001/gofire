using System;
namespace GoFire
{
    public class TimeUtils 
    {
        public class Time
        {
            long _preTime = 0;
            public long DeltaTicks
            {
                get;private set;
            }

            public float DeltaSec
            {
                get
                {
                    var ts = new TimeSpan(DeltaTicks);
                    return (float)ts.TotalSeconds;
                }
            }
            public Time()
            {
                Update();
            }
            public void Update()
            {
                var now = DateTime.Now.Ticks;
                if (_preTime == 0)
                {
                    DeltaTicks = 0;
                }
                else
                {
                    DeltaTicks = now - _preTime;
                }
                _preTime = now;
            }
        }
    }
}

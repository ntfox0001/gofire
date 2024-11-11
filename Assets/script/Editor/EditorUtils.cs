
using System;

namespace GoFire
{
    public class EditorUtils 
    {
        public class Time
        {
            long preTime = 0;
            public long DeltaTicks
            {
                get;private set;
            }

            public float DeltaSec
            {
                get
                {
                    TimeSpan ts = new TimeSpan(DeltaTicks);
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
                if (preTime == 0)
                {
                    DeltaTicks = 0;
                }
                else
                {
                    DeltaTicks = now - preTime;
                }
                preTime = now;
            }
        }
    }


}

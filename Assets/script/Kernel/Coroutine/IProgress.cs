namespace GoFire.Kernel
{

    public struct ProgressData
    {
        public float Progress;
        public float Total;
        
        public override string ToString()
        {
            return $"{Progress}/{Total}, ({Progress / Total * 100}%)";
        }

        public float Percent()
        {
            return Progress / Total;
        }

        public static ProgressData operator +(ProgressData a, ProgressData b)
        {
            return new ProgressData
            {
                Progress = a.Progress + b.Progress,
                Total = a.Total + b.Total
            };
        }

        public static ProgressData Empty = new()
        {
            Progress = 0,
            Total = 1
        };

        public static ProgressData Full = new()
        {
            Progress = 1,
            Total = 1
        };
    }
    public interface IProgress
    {
        ProgressData GetProgress();
    }

    public interface IReportProgress
    {
        void Report(float progress);
    }
}
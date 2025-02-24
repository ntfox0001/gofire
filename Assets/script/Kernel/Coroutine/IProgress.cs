namespace GoFire
{
    public interface IProgress
    {
        float GetProgress();
    }

    public interface IReportProgress
    {
        void Report(float progress);
    }
}
using GoFire.Kernel;

namespace GoFire
{
    public static class DebugUtils
    {
        public static void Assert<T>(T obj) where T : class
        {
            #if UNITY_EDITOR 
            if (obj == null)
            {
                Log.Error("Assert Failed");
                throw new System.Exception("Assert Failed");
            }
            #endif
        }
    }
}
namespace GoFire
{
    public static class ParamUtils
    {
        public static T Params<T>(object[] args)
        {
            return (T)args[0];
        }
        public static (T1,T2) Params<T1, T2>(object[] args)
        {
            return ((T1)args[0], (T2)args[1]);
        }
        public static (T1,T2,T3) Params<T1, T2, T3>(object[] args)
        {
            return ((T1)args[0], (T2)args[1], (T3)args[2]);
        }
    }
}
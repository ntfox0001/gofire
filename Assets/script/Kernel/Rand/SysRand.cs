using System;
namespace GoFire.Kernel
{
    public class SysRand
    {
        private Random _random;
        public SysRand(int seed)
        {
            _random = new Random(seed);
        }
        
        public double Rnd()
        {
            return _random.NextDouble();
        }

        public int RndInt(int r)
        {
            return _random.Next(r);
        }
    }
}

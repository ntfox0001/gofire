using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GoFire
{
    public class Rand
    {
        private System.Random _random;
        public Rand(int seed)
        {
            _random = new System.Random(seed);
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

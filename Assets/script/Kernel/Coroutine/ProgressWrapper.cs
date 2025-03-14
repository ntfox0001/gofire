using System.Collections;
using UnityEngine;

namespace GoFire.Kernel
{
    public class ProgressWrapper : IProgress
    {
        private readonly IEnumerator _instruction;

        ProgressWrapper(IEnumerator instruction)
        {
            _instruction = instruction;
        }

        public float GetProgress()
        {
            return _instruction.Current == null ? 1 : 0;
        }
        
        public static IProgress Wrap(IEnumerator instruction)
        {
            if (instruction is IProgress progress)
            {
                return progress;
            }
            
            return new ProgressWrapper(instruction);
        }
    }
}
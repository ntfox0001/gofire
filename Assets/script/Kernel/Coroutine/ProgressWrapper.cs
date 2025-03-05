using UnityEngine;

namespace GoFire.Kernel
{
    public class ProgressWrapper : IProgress
    {
        private readonly CustomYieldInstruction _instruction;

        ProgressWrapper(CustomYieldInstruction instruction)
        {
            _instruction = instruction;
        }

        public float GetProgress()
        {
            return _instruction.keepWaiting ? 0 : 1;
        }
        
        public static IProgress Wrap(CustomYieldInstruction instruction)
        {
            if (instruction is IProgress progress)
            {
                return progress;
            }
            
            return new ProgressWrapper(instruction);
        }
    }
}
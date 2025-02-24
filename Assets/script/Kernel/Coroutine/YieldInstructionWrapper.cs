using System.Collections;
using UnityEngine;

namespace GoFire
{
    public class YieldInstructionWrapper : CustomYieldInstruction
    {
        IEnumerator _enumerator;
        YieldInstructionWrapper(IEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        public override bool keepWaiting => _enumerator.MoveNext();

        public static CustomYieldInstruction Wrap(IEnumerator enumerator)
        {
            if (enumerator is CustomYieldInstruction instruction)
            {
                return instruction;
            }
            return new YieldInstructionWrapper(enumerator);
        }
    }
}
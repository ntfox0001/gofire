using System.Collections;
using System.Linq;
using UnityEngine;

namespace GoFire
{
     public class WaitForObjects : CustomYieldInstruction, IProgress
     {
         public enum WaitReturn
         {
             Continue,
             Wait,
         }
         public enum WaitForType
         {
             WaitForAny,
             WaitForAll,
         }
         CustomYieldInstruction[] _waitObject;
         IProgress[] _progress;
         WaitForType _comboType;
         /// <summary>
         /// 等待多个对象，当真时，继续等待，当假时不等待
         /// </summary>
         public WaitForObjects(params CustomYieldInstruction[] objects) : this(WaitForType.WaitForAll, objects)
         {}

         public WaitForObjects(params IEnumerator[] objects) : this(objects.Select(x => YieldInstructionWrapper.Wrap(x)).ToArray())
         {}
         
         public WaitForObjects(WaitForType comboType, params CustomYieldInstruction[] objects)
         {
             _comboType = comboType;
             _waitObject = objects;
             _progress = objects.Select(x => ProgressWrapper.Wrap(x)).ToArray();
         }
         public override bool keepWaiting
         {
             get
             {
                 switch (_comboType)
                 {
                     case WaitForType.WaitForAll:
                     {
                         bool keepwait = false;
                         foreach (CustomYieldInstruction obj in _waitObject)
                         {
                             keepwait = keepwait || obj.keepWaiting;
                         }
                         return keepwait;    
                     }
                     case WaitForType.WaitForAny:
                     {
                         bool keepwait = true;
                         foreach (CustomYieldInstruction obj in _waitObject)
                         {
                             keepwait = keepwait && obj.keepWaiting;
                         }
                         return keepwait;    
                     }
                     default:
                         throw new System.NotImplementedException();
                 }
             }
         }

         public float GetProgress()
         {
             var total = 0f;
             foreach (var p in _progress)
             {
                 total += p.GetProgress();
             }
             return total / _progress.Length;
         }
     }
}
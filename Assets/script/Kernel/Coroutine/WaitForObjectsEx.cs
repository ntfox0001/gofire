using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoFire.Kernel
{
    public class WaitForObjectsEx : IEnumerator, IProgress
    {

         IEnumerator[] _waitObject;
         IProgress[] _progress;


         public WaitForObjectsEx(params IEnumerator[] objects)
         {
             _waitObject = objects;
             _progress = objects.Select(x => ProgressWrapper.Wrap(x)).ToArray();
         }

         public bool MoveNext()
         {
             bool keepwait = false;
             List<IEnumerator> nestedList = new List<IEnumerator>();
             foreach (var obj in _waitObject)
             {
                 if (obj.MoveNext())
                 {
                     if (obj.Current is IEnumerator nested)
                     {
                         nestedList.Add(nested);
                     }
                     keepwait = true;
                 }
             }

             Current = new WaitForObjectsEx(nestedList.ToArray());
             return keepwait;    
         }

         public void Reset()
         {
             
         }

         public object Current { get; private set; }

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
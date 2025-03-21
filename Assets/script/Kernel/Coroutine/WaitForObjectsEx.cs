using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoFire.Kernel
{
    public class WaitForObjectsEx : IEnumerator
    {
        private readonly IEnumerator[] _waitObject;
        
         public WaitForObjectsEx(params IEnumerator[] objects)
         {
             _waitObject = objects;
         }

         public bool MoveNext()
         {
             var next = false;
             IEnumerator currObj = null;
             foreach (var obj in _waitObject)
             {
                 if (obj.MoveNext())
                 {
                     var current = obj.Current;
                     if (current is IEnumerator enu)
                     {
                         currObj = enu;
                     }
                     
                     next = true;
                     break;
                 }
             }
             
             Current = currObj;
             
             return next;    
         }

         public void Reset()
         {
             
         }

         public object Current { get; private set; }
    }
}
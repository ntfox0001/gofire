using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Script.Logic.Utils
{
    public static class ComponentUtils
    {
        public static T1 Get<T1>(GameObject go) where T1 : MonoBehaviour
        {
            return go.GetComponent<T1>();
        }
        
        public static (T1,T2) Get<T1, T2>(GameObject go) where T1 : MonoBehaviour where T2 : MonoBehaviour
        {
            return (go.GetComponent<T1>(), go.GetComponent<T2>());
        } 
    }
}
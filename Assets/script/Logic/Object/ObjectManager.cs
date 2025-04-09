using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class ObjectManager : Singleton<ObjectManager>
    {
        // public new static T Instantiate<T>(T obj) where T : Object
        // {
        //     return UnityEngine.Object.Instantiate<T>(obj);
        // }
        //
        // public new static T Instantiate<T>(T obj, Transform parent) where T : Object
        // {
        //     return UnityEngine.Object.Instantiate<T>(obj, parent);
        // }

        public static void Destroy(GameObject obj)
        {
            switch (Pool.GetSingleton().CanReturn(obj))
            {
                case Pool.ReturnCode.Success:
                    foreach (var d in obj.GetComponents<IDestroy>())
                    {
                        d.OnWillDestroy(DestroyStyle.Recycle);
                    }
                    Pool.GetSingleton().Return(obj);
                    break;
                case Pool.ReturnCode.AlreadyReturn:
                    Log.Error("Pool: " + obj.name + " is already return");
                    break;
                case Pool.ReturnCode.NotPoolObject:
                    foreach (var d in obj.GetComponents<IDestroy>())
                    {
                        d.OnWillDestroy(DestroyStyle.Destroy);
                    }
                    Object.Destroy(obj);
                    break;
            }
        }
    }
}
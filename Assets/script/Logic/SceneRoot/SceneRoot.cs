using GoFire.Kernel;
using UnityEngine;

namespace Script.Logic.SceneRoot
{
    public class SceneRoot : Singleton<SceneRoot>
    {
        public GameObject CreateNode(string nodeName)
        {
            var go = new GameObject(nodeName);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            return go;
        }
    }
}
using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class EffectManager : Singleton<EffectManager>
    {
        private const string EffectName = "EffectName";
        
        private readonly PackageGroup _packageGroup = new();
        private SceneParent _sceneParent;
        
        public IEnumerator LoadPackage(SceneParent parent, params string[] packageNames)
        {
            _sceneParent = parent;
            yield return _packageGroup.LoadPackage(packageNames);
            
        }
        
        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }

        public Effect Create(string effectName, Vector3 pos)
        {
            var raw = _packageGroup.GetComponent<Effect>(effectName);
            if (raw == null)
            {
                Log.Error("effect not found {0}", effectName);
                return null;
            }
            
            var ef = ObjectManager.Instantiate(raw, pos, Quaternion.identity, _sceneParent.Screen.transform);
            return ef.GetComponent<Effect>();
        }
    }
}
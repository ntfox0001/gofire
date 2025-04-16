using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class DeathCtrl : MonoBehaviour, IDestroy
    {
        public string[] triggerNames;
        public TriggerBase[] triggerList;
        
        private bool _isDead;
        
        public void Die()
        {
            if (_isDead)
            {
                return;
            }

            _isDead = true;
            foreach (var evName in triggerNames)
            {
                TriggerManager.GetSingleton().Play(evName, transform.position);
            }

            foreach (var ev in triggerList)
            {
                ev.Play(transform.position);
            }
            
            ObjectManager.Destroy(gameObject);
        }

        public void OnWillDestroy(DestroyStyle style)
        {
            if (style == DestroyStyle.Recycle)
            {
                _isDead = false;    
            }
        }
    }
}
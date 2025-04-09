using UnityEngine;

namespace GoFire
{
    public class DeathCtrl : MonoBehaviour, IDestroy
    {
        public string[] eventName;
        public EventBase[] eventList;
        
        private bool _isDead;
        
        public void Die()
        {
            if (_isDead)
            {
                return;
            }

            _isDead = true;
            foreach (var evName in eventName)
            {
                EventManager.GetSingleton().Play(evName, transform.position);
            }

            foreach (var ev in eventList)
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
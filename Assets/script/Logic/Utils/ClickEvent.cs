using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GoFire
{
    public class ClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent OnClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}
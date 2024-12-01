using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    public class Sence : MonoBehaviour
    {
        public CameraCtrl MainCameraCtrl;
        public PlayerCtrl Player;
        public Transform PlayerBornPos;
        public EventHorizon EventHorizon;
        public Land Land;
        EventBase[] events;
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            events = GetComponentsInChildren<EventBase>();
            Array.Sort(events, (EventBase a, EventBase b) => (a.Time < b.Time) ? 1 : -1);

            StartCoroutine(Run());

            Player = GameObject.Instantiate<PlayerCtrl>(Player, transform, false);
            Player.transform.position = PlayerBornPos.position;
            Player.MainCamera = MainCameraCtrl.MainCamera;
            Player.GroundRange = EventHorizon.ViewRange;
            MainCameraCtrl.Set(Player.transform, EventHorizon.ViewRange.x);
            GlobalVar.GetSingleton().MainCamera = MainCameraCtrl;
        }

        IEnumerator Run()
        {
            for (int i = 0; i < events.Length; i++)
            {
                yield return new WaitForSeconds(events[i].Time);
                events[i].OnTouch(transform);
            }
        }
    }
}
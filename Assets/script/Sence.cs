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
        public EventHorizon EventHorizon { get; private set; }
        EventBase[] events;
        // Start is called before the first frame update
        void Start()
        {
            EventHorizon = GetComponentInChildren<EventHorizon>();

            events = GetComponentsInChildren<EventBase>();
            Array.Sort(events, (EventBase a, EventBase b) =>
            {
                return (a.Time < b.Time) ? 1 : -1;
            });

            StartCoroutine(Run());

            Player = GameObject.Instantiate<PlayerCtrl>(Player);
            Player.transform.SetParent(transform, false);
            Player.transform.position = PlayerBornPos.position;
            Player.MainCamera = MainCameraCtrl.MainCamera;
            Player.GroundRange = EventHorizon.ViewRange;
            MainCameraCtrl.Target = Player.transform;
            MainCameraCtrl.GroundWide = EventHorizon.ViewRange.x;
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
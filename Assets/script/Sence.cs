using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    public class Sence : MonoBehaviour
    {
        public Camera MainCamera;
        public PlayerCtrl Player;
        public Transform PlayerBornPos;
        EventBase[] events;
        // Start is called before the first frame update
        void Start()
        {
            events = GetComponentsInChildren<EventBase>();
            Array.Sort(events, (EventBase a, EventBase b) =>
            {
                return (a.Time < b.Time) ? 1 : -1;
            });

            StartCoroutine(Run());

            Player = GameObject.Instantiate<PlayerCtrl>(Player);
            Player.transform.SetParent(PlayerBornPos, false);
            Player.MainCamera = MainCamera;
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
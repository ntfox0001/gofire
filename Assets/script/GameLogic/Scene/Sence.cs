// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// namespace GoFire
// {
//     public class Sence : MonoBehaviour
//     {
//         public PlayerCtrl Player;
//         public Transform PlayerBornPos;
//         public EventHorizon EventHorizon;
//         public Land Land;
//         EventBase[] events;
//         // Start is called before the first frame update
//         void Start()
//         {
//             Init();
//         }
//
//         void Init()
//         {
//             events = GetComponentsInChildren<EventBase>();
//             Array.Sort(events, (EventBase a, EventBase b) => (a.Time < b.Time) ? 1 : -1);
//
//             StartCoroutine(Run());
//
//             Player = GameObject.Instantiate<PlayerCtrl>(Player, transform, false);
//             Player.transform.position = PlayerBornPos.position;
//             Player.GroundRange = EventHorizon.ViewRange;
//             CameraCtrl.GetSingleton().Set(Player.transform, EventHorizon.ViewRange.x);
//         }
//
//         IEnumerator Run()
//         {
//             for (int i = 0; i < events.Length; i++)
//             {
//                 yield return new WaitForSeconds(events[i].Time);
//                 events[i].OnTouch(transform);
//             }
//         }
//     }
// }
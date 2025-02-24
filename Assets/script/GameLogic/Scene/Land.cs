// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEditor.SearchService;
// using UnityEngine;
// using UnityEngine.Serialization;
// using uTools;
//
// namespace GoFire
// {
//     public class Land : MonoBehaviour
//     {
//         public GameObject objectsNode;
//         public GameObject groundTracksNode;
//         public GameObject screenTracksNode;
//         
//         private Ground[] _grounds;
//         private float _currentPos;
//         public TrackManager TrackManager;
//         public class BodyInfo
//         {
//             public IBody Body;
//             public TrackInfo Track;
//         }
//
//         public BodyInfo[] bodiesByTracks = Array.Empty<BodyInfo>();
//         
//         private HashSet<IBody> _objects;
//         public void Initialize()
//         {
//             TrackManager = new TrackManager(groundTracksNode, screenTracksNode);
//             
//             _grounds = GetComponentsInChildren<Ground>();
//             foreach (var ground in _grounds)
//             {
//                 ground.AdjustChildrenPos();
//             }
//         }
//         public void RefreshAllTracks()
//         {
//             TrackManager.RefreshAllTracks();
//         }
//         public void RefreshAllObjects()
//         {
//             _objects = (objectsNode?.GetComponentsInChildren<IBody>() ?? Array.Empty<IBody>()).ToHashSet();
//         }
//         
//         public void ResetAttach()
//         {
//             bodiesByTracks = Array.Empty<BodyInfo>();
//         }
//         public void AttachObject(IBody body, TrackInfo track)
//         {
//             if (!_objects.Contains(body) || !this.TrackManager.GetAllTrackInfos().Contains(track))
//             {
//                 return;
//             }
//             for (int i = 0; i < bodiesByTracks.Length; i++)
//             {
//                 if (bodiesByTracks[i].Body == body)
//                 {
//                     bodiesByTracks[i].Track = track;
//                     return;
//                 }
//             }
//             bodiesByTracks = (BodyInfo[])bodiesByTracks.Append(new BodyInfo() { Body = body, Track = track });
//         }
//
//         public BodyInfo GetBodyByTrack(IBody body)
//         {
//             for (int i = 0; i < bodiesByTracks.Length; i++)
//             {
//                 if (bodiesByTracks[i].Body == body)
//                 {
//                     return bodiesByTracks[i];
//                 }
//             }
//             var bodyInfo = new BodyInfo() { Body = body, Track = null };
//             bodiesByTracks = (BodyInfo[])bodiesByTracks.Append(bodyInfo);
//             return bodyInfo;
//         }
//         private void Update()
//         {
//             var pos = transform.localPosition;
//             var deltaPos = UpdatePos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime, pos.z, _grounds);
//             
//             pos.z -= deltaPos;
//             transform.localPosition = pos;
//         }
//
//         public static float UpdatePos(float deltaTime, float currentPos, Ground[] grounds) 
//         {
//             var ground = GetGroundByPos(currentPos, grounds);
//             if (!ground)
//             {
//                 return 0;
//             }
//             
//             currentPos += ground.GetDeltaPos(deltaTime);
//             // Debug.LogError("deltaTime:" + deltaTime + "currPos:" + currentPos);
//             return currentPos;
//         }
//
//         private static Ground GetGroundByPos(float pos, Ground[] grounds)
//         {
//             float preLen = 0;
//             for (int i = 0; i < grounds.Length; i++)
//             {
//                 var ground = grounds[i]; 
//                 if (pos < ground.GetLength() + preLen)
//                 {
//                     return ground;
//                 }
//
//                 preLen += ground.GetLength();
//             }
//
//             return null;
//         }
//     }
//
// }

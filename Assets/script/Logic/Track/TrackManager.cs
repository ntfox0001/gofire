using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public interface IRailcar
    {
        void SetPosition(Vector3 pos);
        void OnArrive(); // 到达目的地
    }

    public class TrackManager : Singleton<TrackManager>, IManager
    {
        private Dictionary<string, ITrack> _tracks = new();

        private struct NodeTrack
        {
            public GameObject Node;
            public ITrack Track;
        }
        private Dictionary<string, NodeTrack> _localTracks = new();
        private readonly PackageGroup _packageGroup = new();
        
        public IEnumerator Init()
        {
            yield return null;
        }
        public void Update()
        {
            
        }

        public void Release()
        {
            
        }

        public void LoadTrackByNode(GameObject node)
        {
            var tracks = node.GetComponentsInChildren<ITrack>();
            foreach (var track in tracks)
            {
                _localTracks.Add(track.Name, new NodeTrack()
                {
                    Node = node,
                    Track = track
                });
            }
        }
        
        public IEnumerator LoadPackage(Transform parent, params string[] packageNames)
        {
            yield return _packageGroup.LoadPackage(packageNames);
            foreach (var trackName in _packageGroup.GetAllAssetsNameList())
            {
                var raw = _packageGroup.GetAsset<GameObject>(trackName);
                var go = ObjectManager.Instantiate(raw, parent);
                
                //只归零位置，不归零其他属性
                go.transform.position = Vector3.zero;
                
                var track = go.GetComponent<ITrack>();
                if (track != null)
                {
                    _tracks.Add(raw.name, track);
                }

                yield return null;
            }
        }
        
        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }
        
        public ITrack GetTrack(string trackName)
        {
            if (_tracks.TryGetValue(trackName, out var track))
            {
                return track;
            }
            return _localTracks.TryGetValue(trackName, out var localTrack) ? localTrack.Track : null;
        }
    }
}
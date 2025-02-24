using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire;
using UnityEngine;
using YooAsset;

namespace GoFire
{
    public interface IRailcar
    {
        void SetPosition(Vector3 pos);
        void OnArrive(); // 到达目的地
    }

    public class TrackManager : Singleton<TrackManager>, IManager
    {
        public string packageName = "Tracks";
        private ResourcePackage _trackPackage;
        
        GameObject _groundTracksNode;
        GameObject _screenTracksNode;
        private HashSet<TrackInfo> _trackInfos;
        
        PackageGroup _packageGroup = new();
        struct RailcarInfo
        {
            public float Time;
        }

        private Dictionary<IRailcar, RailcarInfo> _railcars = new();

        public void Init(GameObject groundTracksNode, GameObject screenTracksNode)
        {
            _groundTracksNode = groundTracksNode;
            _screenTracksNode = screenTracksNode;
        }
        public void RefreshAllTracks()
        {
            // 获取groundTracks和screenTracks的所有TrackInfo组件
            var groundTrackInfos = _groundTracksNode?.GetComponentsInChildren<TrackInfo>() ?? Array.Empty<TrackInfo>();
            var screenTrackInfos = _screenTracksNode?.GetComponentsInChildren<TrackInfo>() ?? Array.Empty<TrackInfo>();

            // 合并两个数组并更新_trackInfos
            _trackInfos = groundTrackInfos.Concat(screenTrackInfos).ToArray().ToHashSet();
            // Debug.LogError("trackInfos.Count:" + _trackInfos.Count);
        }
        
        public HashSet<TrackInfo> GetAllTrackInfos()
        {
            return _trackInfos;
        }

        public void Init()
        {
        }
        public void Update()
        {
            
        }

        public void Release()
        {
        }

        public IEnumerator LoadPackage(string[] pakcageNames)
        {
            yield return _packageGroup.LoadPackage(pakcageNames);
        }
        
        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }
    }
}
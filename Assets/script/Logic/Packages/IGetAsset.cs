using UnityEngine;

namespace GoFire
{
    public interface IGetAsset
    {
        public T GetAsset<T>(string assetName) where T : Object;
    }
}
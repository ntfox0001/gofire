using UnityEngine;

namespace GoFire
{
    public class AirplaneManager
    {
        private IGetAsset _asset;
        
        public AirplaneManager(IGetAsset asset)
        {
            _asset = asset;
        }
        
        public Airplane CreatePlayer(cfg.Airplane config, IInput input)
        {
            var go = _asset.GetAsset<GameObject>(config.AssetName);
            var airplane = go.AddComponent<Airplane>();
            airplane.Init(config, input);
            return airplane;
        } 
    }
}
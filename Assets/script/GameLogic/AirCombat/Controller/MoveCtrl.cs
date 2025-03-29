using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public interface IMoveRange
    {
        Vector3 AdjustPos(Vector3 pos);
    }
    public class MoveCtrl : MonoBehaviour, IMovable
    {
        private float _speed;
        public IMoveRange MoveRange;
        public Vector3 GetPos()
        {
            return ObjectUtils.GetPosition(gameObject);
        }

        public void SetPos(Vector3 pos)
        {
            if (MoveRange != null)
            {
                pos = MoveRange.AdjustPos(pos);
            }
            
            ObjectUtils.SetPosition(gameObject, pos);
        }

        //目前dir就是前进方向并且也是面朝方向
        public void SetDir(Vector3 pos)
        {
            ObjectUtils.SetDir(gameObject, pos, GameConfig.Up);
        }

        public Vector3 GetDir()
        {
            return ObjectUtils.GetDir(gameObject, GameConfig.Front);
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetSpeed(float s)
        {
            _speed = s;
        }
    }
}
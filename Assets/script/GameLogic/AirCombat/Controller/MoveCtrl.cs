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
            return MovableObjUtils.GetPosition(gameObject);
        }

        public void SetPos(Vector3 pos)
        {
            if (MoveRange != null)
            {
                pos = MoveRange.AdjustPos(pos);
            }
            
            MovableObjUtils.SetPosition(gameObject, pos);
        }

        public void SetRot(Quaternion rot)
        {
            MovableObjUtils.SetRotation(gameObject, rot);
        }

        //目前dir就是前进方向并且也是面朝方向
        public void SetDir(Vector3 pos)
        {
            MovableObjUtils.SetDir(gameObject, pos, GameConfig.Up);
        }

        public Vector3 GetDir()
        {
            return MovableObjUtils.GetDir(gameObject, GameConfig.Front);
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
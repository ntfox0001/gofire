using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class MoveCtrl : MonoBehaviour, IMovable
    {
        public float speed;
        public Vector3 GetPos()
        {
            return ObjectUtils.GetPosition(gameObject);
        }

        public void SetPos(Vector3 pos)
        {
            ObjectUtils.SetPosition(gameObject, pos * speed);
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
            return speed;
        }

        public void SetSpeed(float s)
        {
            speed = s;
        }
    }
}
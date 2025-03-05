using UnityEngine;

namespace GoFire
{
    public class ObjectUtils
    {
        public static Vector3 GetPosition(GameObject go)
        {
            return go.transform.position;
        }

        public static void SetPosition(GameObject go, Vector3 pos)
        {
            go.transform.position = pos;
        }

        public static Vector3 GetDir(GameObject go, Vector3 front)
        {
            return go.transform.rotation * front;
        }

        public static void SetDir(GameObject go, Vector3 dir, Vector3 up)
        {
            go.transform.rotation = Quaternion.LookRotation(dir, up);
        }
    }
}
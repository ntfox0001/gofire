using UnityEngine;

namespace GoFire
{
    public class ObjectUtils
    {
        public static Vector3 GetPosition(GameObject go)
        {
            return go.transform.localPosition;
        }

        public static void SetPosition(GameObject go, Vector3 pos)
        {
            go.transform.localPosition = pos;
        }

        public static Vector3 GetDir(GameObject go, Vector3 front)
        {
            return go.transform.localRotation * front;
        }

        public static void SetDir(GameObject go, Vector3 dir, Vector3 up)
        {
            go.transform.localRotation = Quaternion.LookRotation(dir, up);
        }
    }
}
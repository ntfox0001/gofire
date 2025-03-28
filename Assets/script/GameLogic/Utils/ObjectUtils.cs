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

        public static void ResetTransform(GameObject go)
        {
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }

        public static void ResetTransform(GameObject go, Vector3 front)
        {
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.LookRotation(front, Vector3.up);
            go.transform.localScale = Vector3.one;
        }
    }
}
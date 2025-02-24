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
    }
}
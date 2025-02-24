using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Math
    {
        static public void TrimVector2(ref Vector2 val, Vector2 rangeHalf)
        {
            val.x = val.x > rangeHalf.x ? rangeHalf.x : val.x < -rangeHalf.x ? -rangeHalf.x : val.x;
            val.y = val.y > rangeHalf.y ? rangeHalf.y : val.y < -rangeHalf.y ? -rangeHalf.y : val.y;
        }

        static public void TrimVector3From2(ref Vector3 val, Vector2 rangeHalf)
        {
            val.x = val.x > rangeHalf.x ? rangeHalf.x : val.x < -rangeHalf.x ? -rangeHalf.x : val.x;
            val.z = val.z > rangeHalf.y ? rangeHalf.y : val.z < -rangeHalf.y ? -rangeHalf.y : val.z;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public interface IRailcar
    {
        void SetPosition(Vector3 pos);
        void OnArrive(); // 到达目的地
    }
}

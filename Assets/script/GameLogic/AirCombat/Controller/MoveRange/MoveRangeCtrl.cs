using UnityEngine;

namespace GoFire
{
    public class MoveRangeCtrl : IMoveRange
    {
        public Bounds Range { get; private set; }

        public MoveRangeCtrl(Bounds range, float sideWidth)
        {
            var sw = new Vector3(sideWidth, 0, sideWidth);
            range.max -= sw;
            range.min += sw;
            Range = range;
        }
        
        public Vector3 AdjustPos(Vector3 pos)
        {
            // 调整位置到范围内
            if (pos.x < Range.min.x)
            {
                pos.x = Range.min.x;
            }
            if (pos.x > Range.max.x)
            {
                pos.x = Range.max.x;
            }
            if (pos.z < Range.min.z)
            {
                pos.z = Range.min.z;
            }
            if (pos.z > Range.max.z)
            {
                pos.z = Range.max.z;
            }
            // 调整高度到范围内
            if (pos.y < Range.min.y)
            {
                pos.y = Range.min.y;
            }
            if (pos.y > Range.max.y)
            {
                pos.y = Range.max.y;
            }
            return pos;
        }
    }
}
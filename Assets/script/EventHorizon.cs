using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire {
    // 事件视界，管理可视范围
    public class EventHorizon : MonoBehaviour
    {
        public Vector2 Range { get
            {
                return OutRange + ViewRange;
            } 
        }
        public Vector2 OutRange;
        public Vector2 ViewRange;

        public float Height = 50;
        public float Weight = 50;
        public float Thickness = 50;
        
        private void Start()
        {
            OnResize();
        }
        [ContextMenu("resize")]
        public void OnResize()
        {
            var boxes = GetComponents<BoxCollider>();
            if (boxes == null)
            {
                return;
            }

            boxes[0].center = new Vector3(0, 0, Range.y / 2 + Thickness / 2);
            boxes[0].size = new Vector3(Weight, Height, Thickness);
            boxes[1].center = new Vector3(0,0, -(Range.y / 2 + Thickness / 2));
            boxes[1].size = new Vector3(Weight, Height, Thickness);
            boxes[2].center = new Vector3(Range.y / 2 + Thickness / 2, 0, 0);
            boxes[2].size = new Vector3(Thickness, Height, Weight);
            boxes[3].center = new Vector3(-(Range.y / 2 + Thickness / 2), 0, 0);
            boxes[3].size = new Vector3(Thickness, Height, Weight);
        }
    }

}

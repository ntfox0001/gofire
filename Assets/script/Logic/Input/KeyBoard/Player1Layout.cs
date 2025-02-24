using UnityEngine;

namespace GoFire
{
    public class Player1Layout
    {
        public KeyCode[] FrontKeys = { KeyCode.W, KeyCode.UpArrow };
        public KeyCode[] BackKeys = { KeyCode.S, KeyCode.DownArrow };
        public KeyCode[] LeftKeys = { KeyCode.A, KeyCode.LeftArrow };
        public KeyCode[] RightKeys = { KeyCode.D, KeyCode.RightArrow };
        public KeyCode[] FireKeys = { KeyCode.RightMeta };
        public KeyCode[] Action1Keys = { KeyCode.RightShift };
        public KeyCode[] Action2Keys = { KeyCode.KeypadEnter };
    }
}
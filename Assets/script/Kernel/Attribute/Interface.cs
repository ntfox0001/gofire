using UnityEngine;

namespace GoFire.Kernel
{
    public class InterfaceAttribute : PropertyAttribute
    {
        public System.Type type;
        public InterfaceAttribute(System.Type type)
        {
            this.type = type;
        }
    }
}
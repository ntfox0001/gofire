using UnityEngine;
using System.Collections;

namespace GoFire
{
    public class DontDestroyObject : MonoBehaviour {

        // Use this for initialization
        void Start () {
            DontDestroyOnLoad(this.gameObject);
        }
    }    
}

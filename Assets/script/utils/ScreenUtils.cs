using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils
{
    static public bool IsPortrait
    {
        get
        {
            if (Screen.height > Screen.width)
            {
                return true;
            }

            return false;
        }
    }
}

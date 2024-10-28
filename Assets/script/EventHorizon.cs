using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHorizon : MonoBehaviour
{
    public void OnResize()
    {
        var box = GetComponent<BoxCollider>();
        if (box == null)
        {
            return;
        }

        var cam = GetComponent<Camera>();
        if (cam == null)
        {
            return;
        }

        var size = box.size;

        box.size = new Vector3((float)Screen.width / Screen.height * cam.orthographicSize * 2f, cam.orthographicSize * 2f, size.z);

    }
}

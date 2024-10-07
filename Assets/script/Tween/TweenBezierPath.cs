﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools
{

    //public class TweenBezierPath: Tweener
    //{
    //    public PathCreation.PathCreator Path;
    //    public bool RotationFollow = false;
    //    RectTransform mRectTransform = null;
    //    Transform mTransform = null;

    //    bool mIs3D = true;
    //    float from = 0f;
    //    float to = 1f;
    //    private bool is3D
    //    {
    //        get
    //        {
    //            if (mTransform == null)
    //            {
    //                mTransform = transform;
    //                RectTransform rect = cachedTransform as RectTransform;
    //                mIs3D = (rect != null) ? false : true;
    //            }
    //            return mIs3D;

    //        }
    //        set
    //        {
    //            mIs3D = value;
    //        }
    //    }

    //    Transform cachedTransform
    //    {
    //        get
    //        {
    //            if (mTransform == null)
    //            {
    //                mTransform = transform;
    //                RectTransform rect = cachedTransform as RectTransform;
    //                is3D = (rect != null) ? false : true;
    //            }
    //            return mTransform;
    //        }
    //    }


    //    RectTransform cachedRectTransform
    //    {
    //        get
    //        {
    //            if (mRectTransform == null)
    //            {
    //                mRectTransform = cachedTransform as RectTransform;
    //                is3D = (mRectTransform != null) ? false : true;
    //            }
    //            return mRectTransform;
    //        }
    //    }

    //    public Vector3 value
    //    {
    //        get
    //        {
    //            if (is3D)
    //            {
    //                return cachedTransform.localPosition;
    //            }
    //            else
    //            {
    //                return cachedRectTransform.anchoredPosition;
    //            }
    //        }
    //        set
    //        {
    //            if (is3D)
    //            {
    //                cachedTransform.localPosition = value;
    //            }
    //            else
    //            {
    //                cachedRectTransform.anchoredPosition = value;
    //            }
    //        }
    //    }
    //    public Quaternion rotValue
    //    {
    //        get
    //        {
    //            if (is3D)
    //            {
    //                return cachedTransform.localRotation;
    //            }
    //            else
    //            {
    //                return cachedRectTransform.localRotation;
    //            }
    //        }
    //        set
    //        {
    //            if (is3D)
    //            {
    //                cachedTransform.localRotation = value;
    //            }
    //            else
    //            {
    //                cachedRectTransform.localRotation = value;
    //            }
    //        }
    //    }

    //    protected override void OnUpdate(float factor, bool isFinished)
    //    {
    //        float f = from + factor * (to - from);

    //        value = Path.path.GetPoint(f, PathCreation.EndOfPathInstruction.Loop) - Path.transform.position;

    //        if (RotationFollow)
    //        {
    //            rotValue = Path.path.GetRotation(f, PathCreation.EndOfPathInstruction.Loop);
    //        }
            
    //        //transform.rotation = Path.path.GetRotationAtDistance(f, PathCreation.EndOfPathInstruction.Loop);
    //        //Debug.Log("factor:" + f + " pos:" + value.ToString());

    //    }

    //    public static TweenPosition Begin(GameObject go, Vector3 from, Vector3 to, float duration = 1f, float delay = 0f)
    //    {
    //        TweenPosition comp = Tweener.Begin<TweenPosition>(go, duration);
    //        comp.value = from;
    //        comp.from = from;
    //        comp.to = to;
    //        comp.duration = duration;
    //        comp.delay = delay;
    //        if (duration <= 0)
    //        {
    //            comp.Sample(1, true);
    //            comp.enabled = false;
    //        }
    //        return comp;
    //    }
    //}
}

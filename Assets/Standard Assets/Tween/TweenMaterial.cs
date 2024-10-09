using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;

namespace uTools
{

    public class TweenMaterial : Tween<float>
    {
        public enum UVType
        {
            U,
            V
        }

        public bool includeChildren = false;
        private bool isCanvasGroup = false;
        public UVType UV;
        float mValue = 0f;

        Material[] mMaterials;
        Material[] CachedMaterial
        {
            get
            {
                if (mMaterials == null)
                {
                    var rder = includeChildren ? gameObject.GetComponentsInChildren<Renderer>() : gameObject.GetComponents<Renderer>();
                    mMaterials = new Material[rder.Length];

                    for (int i = 0; i < rder.Length; i++)
                    {
                        mMaterials[i] = rder[i].material;
                    }
                }
                return mMaterials;
            }
        }

        Graphic[] mGraphics;
        Graphic[] CachedGraphics
        {
            get
            {
                if (mGraphics == null)
                {
                    mGraphics = includeChildren ? gameObject.GetComponentsInChildren<Graphic>() : gameObject.GetComponents<Graphic>();
                }
                return mGraphics;
            }
        }

        CanvasGroup mCanvasGroup;
        CanvasGroup CacheCanvasGroup
        {
            get
            {
                if (mCanvasGroup == null)
                {
                    mCanvasGroup = gameObject.GetComponent<CanvasGroup>();
                }
                return mCanvasGroup;
            }
        }


        protected override void Start()
        {
            base.Start();
            // edit by fox
            // ∑≈µΩawake»•
            //if (CacheCanvasGroup != null)
            //{
            //    isCanvasGroup = true;
            //}
        }
        protected void Awake()
        {
            if (CacheCanvasGroup != null)
            {
                isCanvasGroup = true;
            }
        }

        public override float value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                SetValue(CachedMaterial, mValue);
            }
        }

        protected override void OnUpdate(float factor, bool isFinished)
        {
            value = from + factor * (to - from);
        }

        void SetValue(Material[] mats, float val)
        {
            var uv = new Vector2(UV==UVType.U? val:0, UV==UVType.V? val:0);
            
            foreach (var item in mats)
            {
                item.SetTextureOffset("_MainTex", uv);
            }
        }

        public static TweenAlpha Begin(GameObject go, float from, float to, float duration = 1f, float delay = 0f)
        {
            TweenAlpha comp = Begin<TweenAlpha>(go, duration);
            comp.value = from;
            comp.from = from;
            comp.to = to;
            comp.duration = duration;
            comp.delay = delay;
            if (duration <= 0)
            {
                comp.Sample(1, true);
                comp.enabled = false;
            }
            return comp;
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(RectMask2D))]
    public class StatBarFrameUI : MonoBehaviour
    {
        private Vector4 paddingFormat = new Vector4(12, 11, 12, 11);
        private RectTransform frameRectTransform;
        private RectMask2D rectMask;
        private void Awake()
        {
            frameRectTransform = GetComponent<RectTransform>();
            rectMask = GetComponent<RectMask2D>();
        }

        private void Start()
        {
            FormatRectMask();
        }

        public void SetFrameRectTransformSizeDelta(float sizeDeltaX)
        {
            frameRectTransform.sizeDelta = new Vector2(sizeDeltaX, frameRectTransform.sizeDelta.y);
        }

        public Vector2 GetFrameRectTransformSizeDelta()
        {
            return frameRectTransform.sizeDelta;
        }

        private void FormatRectMask()
        {
            rectMask.padding.Set(paddingFormat.x, paddingFormat.y, paddingFormat.z, paddingFormat.w);
        }
    }
}


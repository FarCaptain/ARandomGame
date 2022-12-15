using System.Collections;
using System.Collections.Generic;
//using Crystal;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CRTMonitor
{
    [ExecuteInEditMode]
    public class CRTSafeArea : MonoBehaviour
    {
        RectTransform Panel;
        
        [Range(0.0f , 0.25f)]
        public float MarginScale = 0.025f;

        private bool LastEnable = false;
        private float LastMarginSize = 0;
        Vector2Int LastScreenSize = new Vector2Int (0, 0);

        //void Awake()
        //{
        //    Panel = GetComponent<RectTransform> ();

        //    if (Panel == null)
        //    {
        //        Debug.LogError ("Cannot apply safe area - no RectTransform found on " + name);
        //        Destroy (gameObject);
        //    }

        //    Refresh ();
        //}

        //void Update ()
        //{
        //    Refresh ();
        //}

        //void Refresh ()
        //{
        //    Rect safeArea = GetSafeArea ();

        //    if (LastEnable != PPS_CRTRenderer.IsEnabled
        //        || MarginScale != LastMarginSize
        //        || Screen.width != LastScreenSize.x
        //        || Screen.height != LastScreenSize.y)
        //    {
        //        LastScreenSize.x = Screen.width;
        //        LastScreenSize.y = Screen.height;
        //        LastMarginSize = MarginScale;
        //        LastEnable = PPS_CRTRenderer.IsEnabled;

        //        ApplySafeArea (safeArea);
        //    }
        //}

        //Rect GetSafeArea()
        //{
        //    Rect safeArea = new Rect(0, 0, Screen.width, Screen.height);
        //    if (PPS_CRTRenderer.IsEnabled && enabled)
        //    {
        //        safeArea.xMin += MarginScale * Screen.width;
        //        safeArea.xMax -= MarginScale * Screen.width;
        //        safeArea.yMin += MarginScale * Screen.height;
        //        safeArea.yMax -= MarginScale * Screen.height;

        //    }
        //    return safeArea;
        //}

        //void ApplySafeArea (Rect r)
        //{
        //    // Check for invalid screen startup state on some Samsung devices (see below)
        //    if (Screen.width > 0 && Screen.height > 0)
        //    {
        //        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        //        Vector2 anchorMin = r.position;
        //        Vector2 anchorMax = r.position + r.size;
        //        anchorMin.x /= Screen.width;
        //        anchorMin.y /= Screen.height;
        //        anchorMax.x /= Screen.width;
        //        anchorMax.y /= Screen.height;
                
        //        if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
        //        {
        //            Panel.anchorMin = anchorMin;
        //            Panel.anchorMax = anchorMax;
        //        }
        //    }
        //}
    }
}

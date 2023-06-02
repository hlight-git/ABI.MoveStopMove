using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnit : GameUnit
{
    RectTransform rtf;
    public RectTransform RectTF
    {
        get
        {
            if (rtf == null)
            {
                rtf = GetComponent<RectTransform>();
            }
            return rtf;
        }
    }
}

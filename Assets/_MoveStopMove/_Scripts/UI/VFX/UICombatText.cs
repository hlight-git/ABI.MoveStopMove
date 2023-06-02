using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombatText : UIUnit
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] float lifeTime;
    [SerializeField] Vector3 offset;

    public void OnSpawn(Vector3 position, string text, Color color)
    {
        txt.text = text;
        txt.color = color;
        RectTF.position = CameraFollower.Ins.Camera.WorldToScreenPoint(position);
        RectTF.localScale = Vector3.one;
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        float frames = lifeTime / Time.deltaTime;
        while (frames > 0)
        {
            RectTF.position = Vector3.Lerp(RectTF.position, RectTF.position + offset, Time.deltaTime);
            RectTF.localScale = Vector3.Lerp(RectTF.localScale, Vector3.zero, Time.deltaTime);
            yield return Cache.WaitForEndOfFrame;
            frames -= 1;
        }
        SimplePool.Despawn(this);
    }
}

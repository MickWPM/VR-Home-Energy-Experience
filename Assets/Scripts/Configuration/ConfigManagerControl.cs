using System;
using UnityEngine;

public class ConfigManagerControl : MonoBehaviour
{
    public float showPosX = -1.65f, hidePosX = -1.7f;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    internal void Hide()
    {
        var currentPos = rectTransform.position;
        currentPos.x = hidePosX;
        rectTransform.position = currentPos;
    }

    internal void Show()
    {
        var currentPos = rectTransform.position;
        currentPos.x = showPosX;
        rectTransform.position = currentPos;
    }

}

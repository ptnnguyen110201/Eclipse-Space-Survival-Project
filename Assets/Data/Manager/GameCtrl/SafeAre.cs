using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private const float FixedWidth = 1080f; 
    private const float FixedHeight = 1920f; 

    private void Awake()
    {
        Rect safeArea = Screen.safeArea;
        RectTransform rect = GetComponent<RectTransform>();
        float scaleX = FixedWidth / Screen.width;
        float scaleY = FixedHeight / Screen.height;
        Vector2 fixedOffsetMin = new Vector2(safeArea.xMin * scaleX, safeArea.yMin * scaleY);
        Vector2 fixedOffsetMax = new Vector2((safeArea.xMax - Screen.width) * scaleX,
                                             (safeArea.yMax - Screen.height) * scaleY);

        rect.offsetMin = fixedOffsetMin;
        rect.offsetMax = fixedOffsetMax;
    }
}

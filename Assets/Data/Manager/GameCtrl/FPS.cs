using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    private void Update()
    {
        // T?nh deltaTime ©¢? t?nh FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        // T?nh FPS
        float fps = 1.0f / deltaTime;

        // Hi?n th? FPS ? g?c tr?i m?n h?nh
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(50, 50, 100, 30);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 24; // C? ch?
        style.normal.textColor = Color.white; // M?u ch?

        string text = string.Format("{0:0.} FPS", fps);
        GUI.Label(rect, text, style);
    }
}

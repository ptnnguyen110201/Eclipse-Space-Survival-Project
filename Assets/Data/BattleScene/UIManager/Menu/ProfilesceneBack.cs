using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ProfilesceneBack : ButtonBase
{
    protected override void OnClick()
    {
        MenuManager.Instance.CloseMenu();
        GameManager.Instance.TriggerGameEnd(false);
    }
   
}
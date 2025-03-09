using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSelectableLevel : FuncManager
{
    [Header("Icon Images for Levels")]
    [SerializeField] protected List<Image> LevelIcons;
    [SerializeField] protected int currentLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImages();
    }

    protected virtual void LoadImages()
    {
        if (this.LevelIcons.Count > 0) return;
        foreach (Transform transform in this.transform)
        {
            Image image = transform.GetComponent<Image>();
            this.LevelIcons.Add(image);
        }
        Debug.Log(transform.name + "Load Images", gameObject);
    }

    public void SetLevel(int level, bool advanced)
    {
        this.currentLevel = Mathf.Min(level, this.LevelIcons.Count);
        this.UpdateIcons(level, advanced);
    }

    protected virtual void UpdateIcons(int level, bool advanced)
    {
        // D?n d?p danh s?ch tr??c khi s? d?ng
        this.CleanUpList();

        Color32 advancedColor = new(255, 197, 0, 255); // M?u v?ng s?ng khi advanced
        Color32 defaultIconColor = Color.white; // M?u tr?ng m?c ©¢?nh

        if (advanced && this.currentLevel >= this.LevelIcons.Count)
        {
            // Ch? ©¢? advanced v?i c?p ©¢? v??t m?c t?i ©¢a
            foreach (Image image in this.LevelIcons)
            {
                if (image != null)
                    image.gameObject.SetActive(false);
            }

            if (this.LevelIcons.Count > 0 && this.LevelIcons[0] != null)
            {
                this.LevelIcons[0].gameObject.SetActive(true);
                this.LevelIcons[0].color = advancedColor;
            }
        }
        else
        {
            foreach (Image image in this.LevelIcons)
            {
                if (image != null)
                {
                    image.gameObject.SetActive(true);
                    image.color = Color.black;
                }

                for (int i = 0; i < this.currentLevel && i < this.LevelIcons.Count; i++)
                {
                    if (this.LevelIcons[i] != null)
                    {
                        this.LevelIcons[i].color = defaultIconColor;
                    }
                }
            }
        }
    }

    protected virtual void CleanUpList()
    {
        this.LevelIcons.RemoveAll(icon => icon == null);
    }
}

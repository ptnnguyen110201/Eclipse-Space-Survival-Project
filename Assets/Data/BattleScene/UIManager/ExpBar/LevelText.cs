
using UnityEngine;

public class LevelText : TextBase
{
    [SerializeField] protected int levelText;
    private void LevelShowing()
    {
        if (this.levelText < 0)
        {
            this.levelText = 0;
            this.Text.text = $"Lv.{this.levelText}";
            return;
        }

        this.Text.text = $"Lv.{this.levelText}";
    }

    public virtual void SetLevelText(int currentLevel)
    {
        this.levelText = currentLevel;
        this.LevelShowing();
    }
}

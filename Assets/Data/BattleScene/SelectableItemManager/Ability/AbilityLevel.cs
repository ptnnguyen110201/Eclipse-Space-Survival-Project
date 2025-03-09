using UnityEngine;

public class AbilityLevel : AbilityAbstract
{
    [SerializeField] protected int currentLevel = 0;
    [SerializeField] protected int maxLevel;
    [SerializeField] protected bool levelIsMax;
    [SerializeField] protected bool evoLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.SetLevel();
    }
    private void SetLevel()
    {
        Selectable selectable = this.abilityCtrl.GetSelectable();
        if (selectable == null) return;
        if (selectable.selectableType == SelectableType.Ability)
        {
            AbilityData abilityData = selectable as AbilityData;

            this.maxLevel = abilityData.levels.Count;
            if (abilityData.UltimateLevel == null) return;
            this.maxLevel += 1;
        }
        else if (selectable.selectableType == SelectableType.AbilityBuff )
        {
            AbilityBuffData abilityBuffData = selectable as AbilityBuffData;

            this.maxLevel = abilityBuffData.levels.Count;
        }
        else if (selectable.selectableType == SelectableType.AbilityBuffShip)
        {
            AbilityBuffShipData abilityBuffShipData = selectable as AbilityBuffShipData;

            this.maxLevel = abilityBuffShipData.levels.Count;
        }
    }
    public bool LevelUp(int levelsToAdd)
    {
        if (levelsToAdd <= 0 || this.LevelIsMax())
        {
            return false;
        }

        int newLevel = this.currentLevel + levelsToAdd;

        if (newLevel >= this.maxLevel)
        {
            this.currentLevel = this.maxLevel;
            this.levelIsMax = true;
        }
        else
        {
            this.currentLevel = Mathf.Clamp(newLevel, 1, this.maxLevel);
            this.levelIsMax = false;


            if (this.currentLevel == this.maxLevel - 1)
            {
                this.evoLevel = true;
            }
        }

        return this.currentLevel > 0;
    }
    public bool LevelIsMax() => this.levelIsMax;
    public bool EvoLevel() => this.evoLevel;
    public int GetMaxLevel() => this.maxLevel;
    public int GetCurrentLevel() => this.currentLevel;
}

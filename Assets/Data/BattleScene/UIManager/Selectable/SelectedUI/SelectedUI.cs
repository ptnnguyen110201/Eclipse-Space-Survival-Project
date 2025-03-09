using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUI : FuncManager
{
    [SerializeField] protected List<Image> abilitySkill;
    [SerializeField] protected List<Image> abilityBuff;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilitySkill();
        this.LoadAbilityBuff();
    }

    public virtual void ShowUI(List<AbilityCtrl> abilityCtrls)
    {
        this.CleanUpList(this.abilitySkill);
        this.CleanUpList(this.abilityBuff);

        if (abilityCtrls == null || abilityCtrls.Count <= 0) return;

        int skillIndex = 0;
        int buffIndex = 0;

        foreach (AbilityCtrl abilityCtrl in abilityCtrls)
        {
            if (abilityCtrl == null || !abilityCtrl.gameObject.activeSelf) continue;

            var selectable = abilityCtrl.GetSelectable();
            if (selectable == null) continue;

            if (selectable.selectableType == SelectableType.Ability)
            {
                if (skillIndex < abilitySkill.Count && abilitySkill[skillIndex] != null)
                {
                    abilitySkill[skillIndex].gameObject.SetActive(true);
                    abilitySkill[skillIndex].sprite = selectable.itemSprite;
                    skillIndex++;
                }
            }
            else if (selectable.selectableType == SelectableType.AbilityBuff || selectable.selectableType == SelectableType.AbilityBuffShip)
            {
                if (buffIndex < abilityBuff.Count && abilityBuff[buffIndex] != null)
                {
                    abilityBuff[buffIndex].gameObject.SetActive(true);
                    abilityBuff[buffIndex].sprite = selectable.itemSprite;
                    buffIndex++;
                }
            }
        }
    }
    protected virtual void LoadAbilitySkill()
    {
        if (this.abilitySkill.Count > 0) return;

        Transform skillSlots = this.transform.Find("SkillAbility/SkillAbilitySLots");
        if (skillSlots == null) return;

        foreach (Transform obj in skillSlots)
        {
            if (obj == null) continue;

            Image image = obj.transform.Find("Image")?.GetComponent<Image>();
            if (image != null) this.abilitySkill.Add(image);
        }

        this.HidePrefabs(this.abilitySkill);
    }
    protected virtual void LoadAbilityBuff()
    {
        if (this.abilityBuff.Count > 0) return;

        Transform buffSlots = this.transform.Find("BuffAbility/BuffAbilitySLots");
        if (buffSlots == null) return;

        foreach (Transform obj in buffSlots)
        {
            if (obj == null) continue;

            Image image = obj.transform.Find("Image")?.GetComponent<Image>();
            if (image != null) this.abilityBuff.Add(image);
        }

        this.HidePrefabs(this.abilityBuff);
    }
    protected virtual void HidePrefabs(List<Image> obj)
    {
        foreach (Image prefab in obj)
        {
            if (prefab != null)
            {
                prefab.gameObject.SetActive(false);
            }
        }
    }
    protected virtual void CleanUpList(List<Image> list)
    {
        list.RemoveAll(item => item == null);
    }
}

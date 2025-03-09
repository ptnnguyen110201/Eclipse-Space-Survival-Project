using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAbility : FuncManager
{
    [SerializeField] protected ShipAbilityCtrl shipAbilityCtrl;
    [SerializeField] protected List<AbilityCtrl> abilityCtrls;

    protected override void Start()
    {
        base.Start();
        this.LoadImageAbility();
        this.UnlockAbility(this.abilityCtrls[0].GetSelectable());
    }
  
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipAbilityCtrl();
        this.LoadSelectableCtrls();
    }
    protected virtual void LoadImageAbility()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        foreach (AbilityCtrl ability in this.abilityCtrls)
        {
            AbilitySkillCtrl abilitySkill = ability as AbilitySkillCtrl;
            if (abilitySkill == null) return;
            abilitySkill.GetAbilityData().SetImage(playerData);
        }
    }
    protected virtual void LoadShipAbilityCtrl()
    {
        if (this.shipAbilityCtrl != null) return;
        this.shipAbilityCtrl = transform.GetComponent<ShipAbilityCtrl>();
        Debug.Log(transform.name + "Load AbilityManagerCtrl ", gameObject);
    }
    protected virtual void LoadSelectableCtrls()
    {
        if (this.abilityCtrls.Count > 0) return;
        foreach (Transform obj in this.transform)
        {
            if (obj == null) continue;
            AbilityCtrl selectableCtrl = obj.GetComponent<AbilityCtrl>();
            this.abilityCtrls.Add(selectableCtrl);
        }
        this.HidePrefabs();
        Debug.Log(transform.name + "Load SelectableCtrls ", gameObject);
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.transform)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public void UnlockAbility(Selectable selectable)
    {
        if (this.abilityCtrls.Count <= 0) return;
        bool AbilityIsActive = this.AbilityIsActive(selectable);
        if (AbilityIsActive)
        {
            this.LevelUp(selectable);
            return;
        }
        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            abilityCtrl.gameObject.SetActive(true);
            this.shipAbilityCtrl.AddAbilityCtrl(selectable, abilityCtrl);
            this.LevelUp(selectable);
            break;
        }

    }

    private bool AbilityIsActive(Selectable selectable)
    {
        if (this.abilityCtrls.Count <= 0) return false;

        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            if (abilityCtrl.gameObject.activeSelf)

                return true;
        }
        return false;
    }
    private bool LevelUp(Selectable selectable)
    {
        if (this.abilityCtrls.Count <= 0) return false;
        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            bool Uplevel = abilityCtrl.GetAbilityLevel().LevelUp(1);
            this.shipAbilityCtrl.UpdateAbility();
            return Uplevel;
        }
        return false;
    }

    public int AbilityLevels(Selectable selectable)
    {
        if (this.abilityCtrls == null || this.abilityCtrls.Count <= 0)
            return 0;
        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            int levels = abilityCtrl.GetAbilityLevel().GetCurrentLevel();
            return levels;
        }
        return 0;
    }

    public bool ItemIsLevelMax(Selectable selectable)
    {
        if (this.abilityCtrls == null || this.abilityCtrls.Count <= 0)
            return false;
        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            bool isMax = abilityCtrl.GetAbilityLevel().LevelIsMax();
            return isMax;
        }
        return false;
    }
    public bool IsEvoLevel(Selectable selectable)
    {
        if (this.abilityCtrls == null || this.abilityCtrls.Count <= 0)
            return false;
        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {
            if (abilityCtrl.GetSelectable() != selectable) continue;
            bool isEvoLevel = abilityCtrl.GetAbilityLevel().EvoLevel();
            return isEvoLevel;
        }
        return false;
    }
    public bool IsEvoItem(Selectable selectable)
    {
        if (selectable == null) return false;
        if (selectable.selectableType != SelectableType.Ability) return false;

        List<Selectable> abilitybuffs = new List<Selectable>();


        foreach (AbilityCtrl abilityCtrl in this.abilityCtrls)
        {

            if (!abilityCtrl.transform.gameObject.activeSelf)
                continue;

            Selectable abilitybuff = abilityCtrl.GetSelectable();
            if (abilitybuff is AbilityBuffData abilityBuffData)
                abilitybuffs.Add(abilityBuffData);
        }


        if (abilitybuffs.Count == 0) return false;

        AbilityData abilityData = selectable as AbilityData;
        if (abilityData == null) return false;

        foreach (AbilityBuffData abilityBuff in abilitybuffs)
        {
            if (abilityData == abilityBuff.abilityData)
                return true;
            else if (abilityData.abilityBuffData == abilityBuff)
                return true;
        }

        return false;
    }


    public ShipAbilityCtrl GetShipAbilityCtrl() => this.shipAbilityCtrl;
    public List<AbilityCtrl> GetAbilityCtrls() => this.abilityCtrls;
}

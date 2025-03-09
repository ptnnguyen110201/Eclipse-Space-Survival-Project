using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipAbilityCtrl : ShipAbstract
{
    [SerializeField] protected int AbilityCount = 4;
    [SerializeField] protected List<AbilityCtrl> abilityCtrls = new List<AbilityCtrl>();
    [SerializeField] protected List<AbilityCtrl> abilityBuffCtrls = new List<AbilityCtrl>();

    public bool IsAbilityListFull()
    {
        return this.abilityCtrls.Count >= this.AbilityCount;
    }
    public bool IsAbilityBuffListFull()
    {
        return this.abilityBuffCtrls.Count >= this.AbilityCount;
    }

    public virtual void AddAbilityCtrl(Selectable selectable, AbilityCtrl abilityCtrl)
    {
        if (selectable == null)
        {
            Debug.Log("AbilityCtrl is null");
            return;
        }
        switch (selectable.selectableType)
        {
            case SelectableType.Ability:
                this.abilityCtrls.Add(abilityCtrl);
                break;
            case SelectableType.AbilityBuff:
                this.abilityBuffCtrls.Add(abilityCtrl);
                break;
            case SelectableType.AbilityBuffShip:
                this.abilityBuffCtrls.Add(abilityCtrl);
                break;
        }
        this.UpdateAbility();
    }

    public void UpdateAbility()
    {
        List<AbilityBuffCtrl> buffCtrls = new List<AbilityBuffCtrl>();
        List<AbilityBuffShipCtrl> buffShipCtrls = new List<AbilityBuffShipCtrl>();
        foreach (AbilityCtrl abilityCtrl in this.abilityBuffCtrls)
        {
            if (abilityCtrl is AbilityBuffCtrl abilityBuffCtrl)
            {
                buffCtrls.Add(abilityBuffCtrl);
            }
            else if (abilityCtrl is AbilityBuffShipCtrl buffShipCtrl)
            {
                buffShipCtrls.Add(buffShipCtrl);
            }
        }


        ShipAttributes baseAttributes = this.shipCtrl.GetBaseShipAttributes();
        ShipAttributes buffedAttributes = this.shipCtrl.GetShipAttributes();
        buffedAttributes.SetAttributes(baseAttributes);

        foreach (AbilityBuffShipCtrl abilityCtrl in buffShipCtrls)
        {
            AbilityBuffShipLevelData buffData = abilityCtrl.GetAbilityBuffLevelData();
            if (buffData == null) continue;
            buffData.ApplyBuff(baseAttributes, buffedAttributes);

        }
        this.shipCtrl.GetShipAttributes().SetAttributes(buffedAttributes);


        foreach (AbilitySkillCtrl abilitySkill in abilityCtrls)
        {
            if (abilitySkill == null)
            {
                Debug.LogWarning("AbilitySkillCtrl is null. Skipping.");
                continue;
            }

            AbilityData abilityData = abilitySkill.GetAbilityData();
            if (abilityData == null)
            {
                Debug.LogWarning("AbilityData is null for this skill. Skipping.");
                continue;
            }

            AbilityBuffLevelData matchingBuffLevelData = null;

            foreach (AbilityBuffCtrl abilityBuff in buffCtrls)
            {
                if (abilityBuff == null)
                {
                    Debug.LogWarning("AbilityBuffCtrl is null. Skipping.");
                    continue;
                }

                AbilityBuffData abilityBuffData = abilityBuff.GetAbilityBuffData();
                if (abilityBuffData == null || abilityBuffData.abilityData != abilityData) continue;


                matchingBuffLevelData = abilityBuff.GetAbilityBuffLevelData();
                break;
            }
            abilitySkill.SetAbilityLevelData(matchingBuffLevelData);
        }
    }
   

    public List<AbilityCtrl> GetAbilityCtrls() => this.abilityCtrls;
    public List<AbilityCtrl> GetAbilityBuffCtrls() => this.abilityBuffCtrls;
}

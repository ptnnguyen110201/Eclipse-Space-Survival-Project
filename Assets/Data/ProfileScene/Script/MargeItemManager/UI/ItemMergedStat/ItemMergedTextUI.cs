using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemMergedTextUI : FuncManager
{

    [SerializeField] protected TextMeshProUGUI Item_Amount;
    [SerializeField] protected TextMeshProUGUI Item_nextteAmount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_BoosteAmount();
    }

    public void ShowMergedItem(string currentValue, string nextValue)
    {
        AudioManager.Instance.SpawnSFX(SoundCode.Merge);
        this.Item_Amount.text = currentValue;
        this.Item_nextteAmount.text = nextValue;
    }

    protected virtual void LoadItem_BoosteAmount()
    {
        if (this.Item_nextteAmount != null || this.Item_Amount != null) return;
        this.Item_Amount = transform.Find("item_currenValue").GetComponent<TextMeshProUGUI>();
        this.Item_nextteAmount = transform.Find("item_nextValue").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_BoosteAmount", gameObject);
    }


}

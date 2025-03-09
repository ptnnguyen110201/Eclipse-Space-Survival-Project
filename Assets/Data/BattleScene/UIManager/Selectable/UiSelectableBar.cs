using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UiSelectableBar : FuncManager
{
    private static UiSelectableBar instance;
    public static UiSelectableBar Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("UiSelectableBar instance is missing or destroyed!");
            }
            return instance;
        }
    }

    [SerializeField] protected List<Selectable> Items = new List<Selectable>();
    [SerializeField] protected List<UiSelectableSlot> uiSelectableSlots = new List<UiSelectableSlot>();
    [SerializeField] protected SelectedUI selectedUI;
    protected override void Awake()
    {
        if (UiSelectableBar.instance != null)
        {
            Debug.LogError("Only one UiSelectableBar instance is allowed!");
            return;
        }
        UiSelectableBar.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSelectObjData();
        this.LoadUIItemSlots();
        this.LoadSelectedUI();
    }

    protected override void Start()
    {
        GameEvents.Subscribe(GameEventType.LevelUp, OpenItemBar);
        this.gameObject.SetActive(false);

    }

    protected void OnDestroy()
    {
        GameEvents.Unsubscribe(GameEventType.LevelUp, OpenItemBar);
        if (UiSelectableBar.instance == this)
        {
            UiSelectableBar.instance = null;
        }
    }

    public virtual void OpenItemBar()
    {
        if (UiSelectableBar.Instance == null)
        {
            Debug.LogWarning("UiSelectableBar instance is destroyed, skipping OpenItemBar.");
            return;
        }
        this.ShowUI();
        Time.timeScale = 0;
        UIManager.Instance.OpenScaleUp(this.gameObject, 0.25f);

    }

    public virtual void CloseItemBar()
    {
        if (UiSelectableBar.Instance == null)
        {
            Debug.LogWarning("UiSelectableBar instance is destroyed, skipping CloseItemBar.");
            return;
        }

        UIManager.Instance.CloseElement(gameObject);
        Time.timeScale = 1f;
    }

    protected virtual List<Selectable> GetSelectableBarItems()
    {
        List<Selectable> items = new List<Selectable>();
        if (this.Items.Count <= 0) return items;

        List<AbilityCtrl> abilityCtrls = ShipManager.Instance.GetShipCtrl().GetShipAbility().GetAbilityCtrls();
        foreach (AbilityCtrl ability in abilityCtrls)
        {
            if (ability.GetAbilityLevel().LevelIsMax()) continue;

            Selectable selectable = ability.GetSelectable();
            switch (selectable.selectableType)
            {
                case SelectableType.AbilityBuffShip:
                    items.Add(selectable);
                    break;
                case SelectableType.AbilityBuff:
                    items.Add(selectable);
                    break;

                case SelectableType.Ability:
                    bool isEvoItem = ShipManager.Instance.GetShipCtrl().GetShipAbility().IsEvoItem(selectable);
                    if (!ability.GetAbilityLevel().EvoLevel() || (isEvoItem && ability.GetAbilityLevel().EvoLevel()))
                        items.Add(selectable);
                    break;
            }
        }


        return items;
    }

    protected void ShowUI()
    {
        List<Selectable> selectedBar = new List<Selectable>(this.GetSelectableBarItems());

        ShipAbilityCtrl shipAbilityCtrl = ShipManager.Instance.GetShipCtrl().GetShipAbility().GetShipAbilityCtrl();
        List<AbilityCtrl> abilityCtrls = shipAbilityCtrl.GetAbilityCtrls();
        List<AbilityCtrl> abilityBuffCtrls = shipAbilityCtrl.GetAbilityBuffCtrls();
        this.selectedUI.ShowUI(abilityCtrls);
        this.selectedUI.ShowUI(abilityBuffCtrls);
        ShuffleList(selectedBar);

        if (shipAbilityCtrl.IsAbilityListFull())
            this.FilterSelectable(selectedBar, abilityCtrls, SelectableType.Ability);

        if (shipAbilityCtrl.IsAbilityBuffListFull())
        {
            this.FilterSelectable(selectedBar, abilityBuffCtrls, SelectableType.AbilityBuff);
            this.FilterSelectable(selectedBar, abilityBuffCtrls, SelectableType.AbilityBuffShip);
        }


        while (selectedBar.Count < this.uiSelectableSlots.Count)
        {
            int rand = Random.Range(0, this.Items.Count);
            selectedBar.Add(this.Items[rand]);
        }

        for (int i = 0; i < this.uiSelectableSlots.Count; i++)
        {
            if (i >= selectedBar.Count) break;
            Selectable selectable = selectedBar[i];
            int level = ShipManager.Instance.GetShipCtrl().GetShipAbility().AbilityLevels(selectable);
            bool isEvoLevels = ShipManager.Instance.GetShipCtrl().GetShipAbility().IsEvoLevel(selectable);

            this.uiSelectableSlots[i].SetUI(selectable, level, isEvoLevels);
            this.uiSelectableSlots[i].OnItemSelected -= HandleItemSelected;
            this.uiSelectableSlots[i].OnItemSelected += HandleItemSelected;
        }
    }

    private void HandleItemSelected(Selectable selectedItem)
    {
        if (selectedItem.selectableType == SelectableType.Item)
        {
            ItemData itemData = selectedItem as ItemData;
            if (itemData == null) return;

            switch (itemData.itemType)
            {
                case ItemType.Health:
                    ShipDamageReceiver shipDamageReceiver = ShipManager.Instance.GetShipCtrl().GetShipDamageReceiver();
                    if (shipDamageReceiver == null) return;
                    int HpHeal = ((int)shipDamageReceiver.HPMax * itemData.Amount) / 100;
                    shipDamageReceiver.Add(HpHeal);
                    break;

                case ItemType.Gold:
                    MapStatistics.Instance.AddEarnings(itemData.Amount);
                    break;
            }
        }
        ShipManager.Instance.GetShipCtrl().GetShipAbility().UnlockAbility(selectedItem);
        this.CloseItemBar();
    }

    protected void FilterSelectable(List<Selectable> selectedBar, List<AbilityCtrl> abilityCtrls, SelectableType type)
    {
        HashSet<Selectable> validSelectables = new HashSet<Selectable>();
        foreach (AbilityCtrl abilityCtrl in abilityCtrls)
        {
            Selectable selectable = abilityCtrl.GetSelectable();
            if (selectable != null && selectable.selectableType == type)
            {
                validSelectables.Add(selectable);
            }
        }
        for (int i = selectedBar.Count - 1; i >= 0; i--)
        {
            if (selectedBar[i].selectableType == type && !validSelectables.Contains(selectedBar[i]))
            {
                selectedBar.RemoveAt(i);
            }
        }
    }

    public static void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    protected virtual void LoadUIItemSlots()
    {
        if (this.uiSelectableSlots.Count > 0) return;

        foreach (Transform obj in transform.Find("AbilityBar"))
        {
            UiSelectableSlot uiItemSlot = obj.GetComponent<UiSelectableSlot>();
            if (uiItemSlot != null) this.uiSelectableSlots.Add(uiItemSlot);
        }
        Debug.Log($"{transform.name}: Loaded {this.uiSelectableSlots.Count} Item Slots", gameObject);
    }

    protected virtual void LoadSelectObjData()
    {
        if (this.Items.Count > 0) return;

        this.Items = new List<Selectable>(Resources.LoadAll<Selectable>("SelectableItem/Item"));
        Debug.Log(transform.name + " LoadSelectObjData", gameObject);
    }

    protected virtual void LoadSelectedUI()
    {
        if (this.selectedUI != null) return;

        this.selectedUI = transform.GetComponentInChildren<SelectedUI>();
        Debug.Log(transform.name + " LoadSelectedUI", gameObject);
    }
}

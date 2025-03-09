using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MapSelectBar : FuncManager, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] protected event Action<int> OnItemCentered = delegate { };

    [SerializeField] protected float time = 1f;
    [SerializeField] protected Ease ease = Ease.OutExpo;
    [SerializeField] protected float selectedItemScale = 1f;
    [SerializeField] protected float unselectedItemScale = 0.75f;

    [SerializeField] protected ScrollRect scrollRect;
    [SerializeField]
    protected float ScrollValue
    {
        get => this.scrollRect.horizontalNormalizedPosition;
        set => this.scrollRect.horizontalNormalizedPosition = value;
    }

    [SerializeField] protected Tweener tweener;
    [SerializeField] protected List<MapSlot> mapSlots = new List<MapSlot>();

    [SerializeField] protected MapSlot currentSelectedItem;

    public MapResult GetMapResult() 
    {
        return this.currentSelectedItem.GetMapResult();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScrollRect();
    }


    protected virtual void LoadScrollRect()
    {
        if (this.scrollRect != null) return;
        this.scrollRect = GetComponent<ScrollRect>();
        Debug.Log(transform.name + " Load ScrollRect", gameObject);
    }

    public virtual void LoadContent()
    {
        this.mapSlots.Clear();
        int childCount = this.scrollRect.content.childCount;

        for (int i = 0; i < childCount; i++)
        {
            MapSlot mapSlot = this.scrollRect.content.GetChild(i).GetComponent<MapSlot>();
            if (mapSlot == null) continue;
            this.mapSlots.Add(mapSlot);
        }

        if (this.currentSelectedItem == null && this.mapSlots.Count > 0)
        {
            this.OnItemCentered(0);
            this.UpdateSelectedItem(0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.tweener != null && this.tweener.IsActive())
        {
            this.tweener.Kill();
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int itemCount = mapSlots.Count;
        float step = (itemCount > 2) ? 1f / (itemCount - 1) : 1f;

        float target = Mathf.Clamp01(Mathf.RoundToInt(this.ScrollValue / step) * step);

        this.tweener = DOTween.To(() => this.ScrollValue, value => this.ScrollValue = value, target, this.time).SetEase(this.ease);

        int index = Mathf.RoundToInt(target * (itemCount - 1));
        this.OnItemCentered(index);
        this.UpdateSelectedItem(index);
    }

 


    protected virtual void UpdateSelectedItem(int index)
    {
        MapSlot selectedItem = this.mapSlots[index];

        if (this.currentSelectedItem == selectedItem) return;

        this.currentSelectedItem = selectedItem;

        foreach (var item in this.mapSlots)
        {
            float scale = (item == this.currentSelectedItem) ? this.selectedItemScale : this.unselectedItemScale;
            item.transform.DOKill();
            item.transform.DOScale(Vector3.one * scale, this.time / 2).SetEase(this.ease);
        }
    }
}

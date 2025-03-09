using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSlot : FuncManager
{
    [SerializeField] protected TextMeshProUGUI Map_Name;
    [SerializeField] protected List<Image> Map_Enemy;
    [SerializeField] protected MapRewardBtn MapRewardBtn;
    [SerializeField] protected MapResult MapResult;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMap_Name();
        this.LoadEnemyImage();
        this.LoadMapRewardBtn();
    }
    public void ShowUI(MapResult mapResult)
    {
        if (mapResult == null) return;
        this.MapResult = mapResult;
        this.Map_Name.text = $"{mapResult.mapdata.MapName}";
        this.MapRewardBtn.ShowUI(mapResult);
        for (int i = 0; i < mapResult.mapdata.mapEnemySprite.Count; i++)
        {
            this.Map_Enemy[i].sprite = mapResult.mapdata.mapEnemySprite[i];
        }

    }
    protected virtual void LoadMap_Name()
    {
        if (this.Map_Name != null) return;
        this.Map_Name = transform.Find("Map_Detail/Map_Name").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadMap_Name", gameObject);
    }
    protected virtual void LoadMapRewardBtn()
    {
        if (this.MapRewardBtn != null) return;
        this.MapRewardBtn = transform.Find("MapReward").GetComponentInChildren<MapRewardBtn>();
        Debug.Log(transform.name + " LoadMapRewardBtn", gameObject);
    }
    protected virtual void LoadEnemyImage()
    {
        if (this.Map_Enemy == null || this.Map_Enemy.Count > 0) return;

        foreach (Transform obj in this.transform.Find("Map_Detail/Map_Enemy"))
        {
            Image image = obj.GetComponentInChildren<Image>();
            if (image != null)
            {
                this.Map_Enemy.Add(image);
            }
        }

        Debug.Log(transform.name + " LoadEnemyImage", gameObject);
    }
    public MapResult GetMapResult() => this.MapResult;
}

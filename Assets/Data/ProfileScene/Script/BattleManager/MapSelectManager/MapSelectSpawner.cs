using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSelectSpawner : Spawner
{
    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Viewport/Content");
        Debug.Log(transform.name + " Load Holder ", gameObject);
    }

    public void SpawnMap(List<MapResult> mapResults)
    {
        this.ClearMap();
        if (mapResults.Count < 0) return;

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
       mapResults.Sort((a, b) => a.mapdata.MapID.CompareTo(b.mapdata.MapID));
        for (int i = 0; i < mapResults.Count; i++)
        {
            MapResult mapResult = mapResults[i];
            if (!mapResult.IsUnlocked) continue;

            Transform newMap = this.Spawn("Map", pos, rot);
            MapSlot mapSlot = newMap.GetComponent<MapSlot>();
            mapSlot.ShowUI(mapResults[i]);
            newMap.localScale = Vector3.one;
            newMap.gameObject.SetActive(true);


        }
    }
    private void ClearMap()
    {
        if (this.holder.childCount <= 0) return;
        foreach (Transform obj in this.holder)
        {
            this.Despawn(obj);
        }
    }
}

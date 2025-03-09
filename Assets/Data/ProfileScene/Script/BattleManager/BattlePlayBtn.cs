using UnityEngine;

public class BattlePlayBtn : ButtonBase
{
    [SerializeField] protected MapManager mapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapManager();
    }

    protected virtual void LoadMapManager()
    {
        if (this.mapManager != null) return;
        this.mapManager = transform.parent.parent.GetComponent<MapManager>();
        Debug.Log(transform.name + " Load MapManager ", gameObject);
    }

 

    protected override void OnClick()
    {
        MapResult mapResult = this.mapManager.GetMapSelectBar().GetMapResult();
        this.StartCoroutine(GameManager.Instance.LoadBattleScene(mapResult));
    }
}

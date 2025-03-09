using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemImpart : FuncManager
{
    [SerializeField] protected ItemCtrl itemCtrl;
    [SerializeField] protected CircleCollider2D sphereCollider;

    protected override void OnDisable()
    {
        base.OnDisable();
        DOTween.Kill(gameObject);
        this.StopAllCoroutines();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadItemCtrl();
    }

    protected virtual void LoadCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<CircleCollider2D>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 0.1f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadItemCtrl()
    {
        if (this.itemCtrl != null) return;
        this.itemCtrl = transform.parent.GetComponent<ItemCtrl>();
        Debug.Log(transform.name + "LoadItemCtrl", gameObject);
    }

    public virtual IEnumerator FlyOutAndReturn(Transform returnPos)
    {
        ItemMove itemMove = this.itemCtrl.GetItemMove();
        yield return this.StartCoroutine(itemMove.FlyOutAndReturn(returnPos));

    }
 
}

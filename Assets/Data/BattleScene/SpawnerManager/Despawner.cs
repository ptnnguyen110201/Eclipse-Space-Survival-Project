using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Despawner : FuncManager
{

    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }
    public virtual void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }

    protected abstract bool CanDespawn();
}

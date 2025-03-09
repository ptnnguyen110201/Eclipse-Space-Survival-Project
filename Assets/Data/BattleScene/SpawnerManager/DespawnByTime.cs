using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawner
{
    [SerializeField] protected float delay;

    protected override bool CanDespawn()
    {
        return true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(this.DespawnAfterDelay());
    }

    private IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(this.delay);
        this.Despawning();
    }
}

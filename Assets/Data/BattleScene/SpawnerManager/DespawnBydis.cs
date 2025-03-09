using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawner
{
    [SerializeField] protected float disLimit;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Transform mainCam;

    protected override void LoadComponents()
    {
        this.LoadCamera();
      
    }

    protected virtual void LoadCamera()
    {
        if (this.mainCam != null) return;
        this.mainCam = Transform.FindObjectOfType<Camera>().transform;
        Debug.Log(transform.parent.name + ": LoadCamera", gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(CheckDespawnDistance()); 
    }

    private IEnumerator CheckDespawnDistance()
    {
        while (true)
        {
            if (this.CanDespawn())
            {
                this.DespawnObject();
                yield break; 
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.parent.position, this.mainCam.position);
        return this.distance > this.disLimit;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class BoomSender : ItemSender
{
    public virtual void TriggerAOEDamage()
    {
        List<Transform> objs = this.FindTargetsInRange();
        foreach (Transform obj in objs)
        {
            if (obj == null) continue;
            this.Send(obj);
        }
    }
    protected virtual List<Transform> FindTargetsInRange()
    {
        List<Transform> targets = new();
        Vector3 cameraCenter = Camera.main.transform.position;
        float Width = ResolutionManager.Instance.MaxX;
        float Height = ResolutionManager.Instance.MaxY;
        Vector2 Size = new Vector2(Width, Height);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(cameraCenter, Size, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.CompareTag("Player")) continue;
            targets.Add(collider.transform);
        }
        return targets;
    }
}

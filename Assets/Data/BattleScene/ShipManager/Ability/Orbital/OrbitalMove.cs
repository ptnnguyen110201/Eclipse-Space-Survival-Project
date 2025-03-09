using UnityEngine;
using DG.Tweening;

public class OrbitalMove : Move2D
{
    [SerializeField] protected float orbitRadius;
    [SerializeField] protected Transform Pos;
    [SerializeField] protected float currentAngle;

    private Tween positionTween;
    private Tween rotationTween;

    public void SetPos(Transform Pos)
    {
        this.Pos = Pos;
        Vector3 offset = transform.parent.position - this.Pos.position;
        this.currentAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    public void SetOrbitRadius(float orbitRadius) => this.orbitRadius = orbitRadius;

    public override void Move()
    {
        this.currentAngle += this.Speed * Time.fixedDeltaTime;
        float radianAngle = this.currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * this.orbitRadius;
        Vector2 newPosition = (Vector2)(this.Pos.position + offset);
        Vector2 direction = newPosition - this.Rigidbody.position;

        KillTweens(); 
        positionTween = this.Rigidbody.DOMove(newPosition, Time.fixedDeltaTime)
            .SetEase(Ease.Linear);

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationTween = DOTween.To(
            () => this.Rigidbody.rotation,  
            angle => this.Rigidbody.rotation = angle, 
            targetAngle, 
            Time.fixedDeltaTime
        ).SetEase(Ease.Linear);
    }

    private void KillTweens()
    {
        if (positionTween != null && positionTween.IsActive())
            positionTween.Kill();

        if (rotationTween != null && rotationTween.IsActive())
            rotationTween.Kill();
    }

    protected override void OnDisable()
    {
        KillTweens(); // Cleanup when disabled
    }

    protected override void OnDestroy()
    {
        KillTweens(); // Cleanup when destroyed
    }
}

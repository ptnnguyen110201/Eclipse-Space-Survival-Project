using UnityEngine;
using DG.Tweening;

public abstract class MoveBase : FuncManager
{
    [SerializeField] protected Rigidbody2D Rigidbody;
    [SerializeField] protected float Speed;
    [SerializeField] protected Vector3 Direction = Vector3.up;

    [SerializeField] protected Tween movementTween; 

    public abstract void Move();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidy();
    }

    protected virtual void LoadRigidy()
    {
        if (this.Rigidbody != null) return;
        this.Rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " Load Rigidbody ", gameObject);
    }

    public void SetDirection(Vector3 newDirection)
    {
        this.Direction = newDirection;
    }

    public Vector3 GetDirection() => this.Direction;

    public Rigidbody2D GetRigidbody2D() => this.Rigidbody;

    protected override void OnDisable()
    {
        this.KillTween();
    }

    protected virtual void OnDestroy()
    {
        this.KillTween();
    }

    protected void KillTween()
    {
        if (this.movementTween != null && this.movementTween.IsActive())
        {
            this.movementTween.Kill();
        }
    }
}

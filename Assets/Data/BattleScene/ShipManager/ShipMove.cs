using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShipMove : ShipAbstract
{
    [SerializeField] protected Vector3 mousePosition;
    [SerializeField] protected float Speed = 2;
    [SerializeField] protected float tweenDuration = 0.1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(this.MoveShipCoroutine());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DOTween.Kill(this, true); 
        this.StopAllCoroutines();
    }

    private IEnumerator MoveShipCoroutine()
    {
        while (true)
        {
            this.GetMousePos();
            this.MoveShip();
            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void MoveShip()
    {
        if (this.mousePosition.sqrMagnitude > 0.01f)
        {
            Vector2 targetVelocity = this.mousePosition * this.Speed;

            DOTween.To(() => this.shipCtrl.GetRigidbody2D().velocity, x => this.shipCtrl.GetRigidbody2D().velocity = x, targetVelocity, this.tweenDuration)
                .SetEase(Ease.Linear)
                .SetUpdate(UpdateType.Fixed)
                .SetId(this); 
        }
        else
        {
            DOTween.To(() => this.shipCtrl.GetRigidbody2D().velocity, x => this.shipCtrl.GetRigidbody2D().velocity = x, Vector2.zero, this.tweenDuration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(UpdateType.Fixed)
                .SetId(this);
        }
    }

    protected virtual Vector3 GetMousePos() => this.mousePosition = InputManager.Instance.GetMousePos();
}

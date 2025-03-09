using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ItemMove : FuncManager
{
    [SerializeField] private float flyOutDistance = 1f;
    [SerializeField] private float flyOutDuration = 0.25f;
    [SerializeField] private float returnDuration = 0.1f;
    [SerializeField] protected Vector3 initialPosition;
    [SerializeField] protected Tweener moveTweener;

    protected override void OnDisable()
    {
        base.OnDisable();
        this.moveTweener?.Kill();
    }

    private IEnumerator MoveTo(Vector3 destination, float duration, Ease easeType, System.Action onComplete = null)
    {
        bool moveCompleted = false;

        this.moveTweener = transform.parent.DOMove(destination, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                moveCompleted = true;
                onComplete?.Invoke();
            });

        yield return new WaitUntil(() => moveCompleted);
    }

    public IEnumerator FlyOutAndReturn(Transform returnPos = null)
    {
        this.initialPosition = transform.parent.localPosition;

        Vector3 flyOutDirection = new Vector3(
            Random.insideUnitCircle.x,
            Random.insideUnitCircle.y,
            0
        ) * this.flyOutDistance;

        yield return this.MoveTo(this.initialPosition + flyOutDirection, this.flyOutDuration, Ease.OutQuad);

        Vector3 targetPosition = returnPos != null ? returnPos.position : this.initialPosition;
        yield return this.MoveTo(targetPosition, this.returnDuration, Ease.InQuad);
    }
}

using UnityEngine;
using System.Collections;

public class ShipLookAt : ShipAbstract
{
    [SerializeField] private float rotationSpeed = 180f;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(this.RotateTowardsMovementCoroutine());
    }

    private IEnumerator RotateTowardsMovementCoroutine()
    {
        while (true)
        {
            this.RotateTowardsMovement();
            yield return new WaitForFixedUpdate();
        }
    }

    private void RotateTowardsMovement()
    {
        Transform shipModel = this.GetShipCtrl().GetShipModel().transform;
        Vector2 direction = shipModel.up; 
        Transform nearestEnemy = this.GetShipCtrl().GetShipArea().GetNearestEnemy();
        if (nearestEnemy != null)
        {
            direction = (nearestEnemy.position - shipModel.position).normalized;
        }

        if (direction == Vector2.zero) return;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = shipModel.rotation.eulerAngles.z;
        float smoothedAngle = Mathf.LerpAngle(currentAngle, targetAngle - 90, this.rotationSpeed * Time.deltaTime);
        shipModel.rotation = Quaternion.Euler(0f, 0f, smoothedAngle);
    }

}

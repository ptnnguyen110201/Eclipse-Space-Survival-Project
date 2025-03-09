using UnityEngine;
using DG.Tweening;

public class EnergyBallFly : Move2D
{
    public override void Move()
    {
        this.CheckBounds();
        base.Move();
    }

    public virtual void CheckBounds()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        float maxX = cameraPosition.x + (ResolutionManager.Instance.MaxX / 2);
        float minX = cameraPosition.x - (ResolutionManager.Instance.MaxX / 2);
        float maxY = cameraPosition.y + (ResolutionManager.Instance.MaxY / 2);
        float minY = cameraPosition.y - (ResolutionManager.Instance.MaxY / 2);

        Vector3 currentPosition = this.Rigidbody.position;

        if (currentPosition.x + this.Direction.x * this.Speed * Time.fixedDeltaTime > maxX ||
            currentPosition.x + this.Direction.x * this.Speed * Time.fixedDeltaTime < minX)
        {
            this.Direction.x = -this.Direction.x;

            this.Rigidbody.velocity = Vector2.zero; 
            DOTween.To(() => this.Direction, v => this.SetDirection(v), this.Direction, 0.1f);
        }

        if (currentPosition.y + this.Direction.y * this.Speed * Time.fixedDeltaTime > maxY ||
            currentPosition.y + this.Direction.y * this.Speed * Time.fixedDeltaTime < minY)
        {
            this.Direction.y = -this.Direction.y;

            
            this.Rigidbody.velocity = Vector2.zero; // Stop current motion
            DOTween.To(() => this.Direction, v => this.SetDirection(v), this.Direction, 0.1f);
        }

        this.Rigidbody.position = new Vector3(
            Mathf.Clamp(currentPosition.x, minX, maxX),
            Mathf.Clamp(currentPosition.y, minY, maxY),
            currentPosition.z
        );
    }
}

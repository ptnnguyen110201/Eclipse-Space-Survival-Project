using System.Collections;
using UnityEngine;

public class CameraFollower : FuncManager
{
    [SerializeField] protected Transform playerPos;
    [SerializeField] protected Vector3 velocity = Vector3.zero;
    [SerializeField] protected Vector3 offset;

    [SerializeField] protected Vector3 minCameraPosition = new Vector3(-6f,-2.8f, 0f); 
    [SerializeField] protected Vector3 maxCameraPosition = new Vector3(6f, 2.8f, 0f);

    [SerializeField] protected float smoothTime = 0.75f;
    [SerializeField] protected float followThreshold = 0.75f;

    protected override void Start()
    {
        base.Start();
        this.LoadPlayerPos();
        this.StartCoroutine(FollowCoroutine());
    }

    private void LoadPlayerPos()
    {
        if (this.playerPos != null) return;
        this.playerPos = ShipManager.Instance.GetShipCtrl().transform;
        Debug.Log(transform.name + " Load PlayerPos", gameObject);
    }

    private IEnumerator FollowCoroutine()
    {
        while (true)
        {
            Follow();
            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void Follow()
    {
        if (this.playerPos == null) return;

        Vector3 desiredPosition = new Vector3(
            this.playerPos.position.x + this.offset.x,
            this.playerPos.position.y + this.offset.y,
            transform.position.z
        );

        float distance = Vector3.Distance(transform.position, desiredPosition);
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minCameraPosition.x, maxCameraPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minCameraPosition.y, maxCameraPosition.y);
        if (distance > this.followThreshold)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref this.velocity, this.smoothTime);
        }
    }
}

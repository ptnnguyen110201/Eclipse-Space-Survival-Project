using System.Collections;
using UnityEngine;

public class BulletModel : BulletAbstract
{
    [SerializeField] protected SpriteRenderer Model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (this.Model != null) return;
        this.Model = this.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + " Load Model", gameObject);
    }

    public virtual void SetBulletSprite(Sprite sprite) 
    {
        if (sprite == null) return;
        this.Model.sprite = sprite; 
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(this.CheckVisibilityCoroutine());
    }

    private IEnumerator CheckVisibilityCoroutine()
    {
        while (true)
        {
            if (!this.IsObjectVisible())
            {
                this.Model.enabled = false;
            }
            else
            {
                this.Model.enabled = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private bool IsObjectVisible()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector3 parentPosition = transform.parent.position;
        return parentPosition.x >= cameraBottomLeft.x && parentPosition.x <= cameraTopRight.x &&
               parentPosition.y >= cameraBottomLeft.y && parentPosition.y <= cameraTopRight.y;
    }
}

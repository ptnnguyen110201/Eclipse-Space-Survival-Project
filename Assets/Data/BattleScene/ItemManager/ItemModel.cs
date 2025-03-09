using System;
using System.Collections;
using UnityEngine;

public class ItemModel : FuncManager
{
    [SerializeField] protected SpriteRenderer Model;
    public SpriteRenderer GetSpriteRenderer() => this.Model;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }
    protected virtual void LoadModel()
    {
        if (this.Model != null) return;
        this.Model = this.GetComponent<SpriteRenderer>();

    }

    public virtual void SetSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.Log("Sprite is Null");
            return;
        }
        this.Model.sprite = sprite;
    }

  
    public void SetVisibility(bool isVisible)
    {
        if (this.Model == null) return;

        this.Model.enabled = isVisible;
    }
   
}

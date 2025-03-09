using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectModel : ObjectAbstract
{
    [SerializeField] protected SpriteRenderer Model;

    public SpriteRenderer GetSpriteRenderer() => this.Model;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ApplyModelSprite();
    }
    protected virtual void LoadModel()
    {
        if (this.Model != null) return;
        this.Model = this.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + "Load Model", gameObject);
    }
    protected virtual void ApplyModelSprite()
    {
        ObjectAttribute objectAttribute = this.objectCtrl.GetObjectAttribute();
        if (objectAttribute == null)
        {
            Debug.Log("ObjectAttribute is Null");
            return;
        }
        Sprite sprite = objectAttribute.objectSprite;
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

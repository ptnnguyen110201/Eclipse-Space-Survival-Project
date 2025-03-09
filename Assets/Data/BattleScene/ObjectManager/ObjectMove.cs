using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMove : ObjectAbstract
{
    [SerializeField] public float Speed;
    [SerializeField] protected Vector3 Direction = Vector3.up;
    public abstract void Move();
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetSpeed();
    }
    protected virtual void SetSpeed() 
    {
        ObjectAttribute objectAttribute = this.objectCtrl.GetObjectAttribute();
        if(objectAttribute == null) 
        {
            Debug.Log("ObjectAttribute is null");
            return; 
        }
        this.Speed = objectAttribute.objectSpeed;
    }
    public void SetDirection(Vector3 newDirection)
    {
        this.Direction = newDirection;
    }

    public Vector3 GetDirection() => this.Direction;
}

using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectData : ScriptableObject
{
    public string objName = "";
    public ObjectType objType;
    public List<ObjectAttribute> objectAttributes;

  
}

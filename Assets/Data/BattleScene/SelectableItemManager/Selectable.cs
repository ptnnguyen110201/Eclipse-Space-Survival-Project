using UnityEngine;

public abstract class Selectable : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ObjectType objectType;
    public SelectableType selectableType;
}

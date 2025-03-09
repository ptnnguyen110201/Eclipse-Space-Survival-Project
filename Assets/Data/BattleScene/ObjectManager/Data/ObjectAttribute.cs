using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectAttribute
{
    public Sprite objectSprite;
    public double objectHp;
    public double objectATK;
    public float objectSpeed;
    public List<ItemDropRate> dropList;
}
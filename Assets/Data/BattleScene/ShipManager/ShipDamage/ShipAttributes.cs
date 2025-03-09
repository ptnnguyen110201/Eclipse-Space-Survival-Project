using System;
using UnityEngine;

[Serializable]
public class ShipAttributes
{
    public double HP, ATK; 

    public void SetAttributes(double HP, double ATK)
    {
        this.HP = HP;
        this.ATK = ATK;

    }
    public void SetAttributes(ShipAttributes buffedShipAttribute)
    {
        this.HP =  buffedShipAttribute.HP;
        this.ATK = buffedShipAttribute.ATK;
    }
}


using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Object/EnemyData")]
public class EnemyData : ObjectData
{
    public EnemyID enemyID;
    public ObjectAttribute GetObjectAttribute(EnemyType enemyType) 
    {
        switch (enemyType)
        {
            case EnemyType.LightRed:
                return this.objectAttributes[0];
            case EnemyType.Gray:
                return this.objectAttributes[1];
            case EnemyType.Green:
                return this.objectAttributes[2];
            case EnemyType.DarkRed:
                return this.objectAttributes[3];
            default:
                throw new ArgumentException("Invalid Enemy Type", nameof(enemyType)); 
        }
    }




    protected void OnEnable()
    {
        this.CalculateAttributesForLevel();  
    }
    public void CalculateAttributesForLevel()
    {
        if (objectAttributes == null || objectAttributes.Count == 0)
            return;

        ObjectAttribute firstAttribute = objectAttributes[0];
        for (int i = 1; i < objectAttributes.Count; i++)
        {
            objectAttributes[i].objectATK = firstAttribute.objectATK * Mathf.Pow(1.1f, i);  
            objectAttributes[i].objectHp = firstAttribute.objectHp * Mathf.Pow(1.5f, i);   
            objectAttributes[i].objectSpeed = firstAttribute.objectSpeed * Mathf.Pow(1f, i); 
        }
    }

}

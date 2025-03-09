using System.Collections;
using UnityEngine;

public class EnemyCtrl : ObjectCtrl
{
  
    public override void SetObjectAttribute(EnemyType enemyType) 
    {
        EnemyData enemyData = this.objectSO as EnemyData;
        if (enemyData == null) 
        {
            Debug.Log("EnemyData is null");
            return;    
        }
        this.objectAttribute = enemyData.GetObjectAttribute(enemyType);
    }
    public EnemyMove GetEnemyMove() 
    {
        EnemyMove enemyMove = this.objectMove as EnemyMove;
        return enemyMove;
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyManager.Instance?.RegisterEnemy(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnemyManager.Instance?.UnregisterEnemy(this);
    }
}

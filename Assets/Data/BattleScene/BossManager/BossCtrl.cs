using System;
using System.Collections;
using UnityEngine;

public class BossCtrl : ObjectCtrl
{
    public event Action<double, double> OnHealthChanged;
    public event Action OnBossDeath;
    public void HandleDeath()
    {
        this.OnBossDeath?.Invoke();
    }
    public void UpdateHealth(double hp, double maxHp)
    {
        this.OnHealthChanged?.Invoke(hp, maxHp);
    }

    public override void SetObjectAttribute(EnemyType enemyType) 
    {
        BossData bossData = this.objectSO as BossData;
        if (bossData == null) 
        {
            Debug.Log("BossData is null");
            return;    
        }
        this.objectAttribute = bossData.GetObjectAttribute(enemyType);
    }
    public BossData GetBossData() => this.objectSO as BossData;
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }


}

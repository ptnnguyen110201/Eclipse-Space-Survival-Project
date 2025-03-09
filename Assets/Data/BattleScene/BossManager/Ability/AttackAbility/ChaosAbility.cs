using System.Collections;
using UnityEngine;

public class ChaosAbility : BossAbility
{
    [SerializeField] protected int chaosBullets = 50;
    [SerializeField] protected float xMin = -6f, xMax = 6f, yMin = -7f, yMax = 7f;

    public override IEnumerator Execute(Transform shotPoint, BossStateMachine bossStateMachine, BossState bossState)
    {
        for (int i = 0; i < this.chaosBullets; i++)
        {
            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);

            Vector2 spawnPosition = new Vector2(randomX, randomY);
            Transform obj = BulletSpawner.Instance.SpawnBullet(
                BulletType.ChaosAttack,
                spawnPosition,
                Quaternion.identity
            );

            BulletCtrl bulletCtrl = obj.GetComponent<BulletCtrl>();
            BulletManager.Instance.AddBullet(bulletCtrl);

            ObjectAttribute objectAttribute = bossStateMachine.GetBossCtrl().GetObjectAttribute();
            if (objectAttribute == null) break;
            bulletCtrl.SetBullet(objectAttribute, bossStateMachine.GetBossCtrl().transform);
            yield return new WaitForSeconds(0.1f);
        }
       
    }
}

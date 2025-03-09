using System.Collections;
using UnityEngine;

public class RainAbility : BossAbility
{
    [SerializeField] protected int numberOfBullets = 50;
    [SerializeField] protected float xMin = -6f, xMax = 6f, yMax = 7f;

    public override IEnumerator Execute(Transform shotPoint, BossStateMachine bossStateMachine, BossState bossState)
    {
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            float randomX = Random.Range(xMin, xMax);
            Vector2 spawnPosition = new Vector2(randomX, yMax);
            Transform obj = BulletSpawner.Instance.SpawnBullet(
                BulletType.RainAttack,
                spawnPosition,
                Quaternion.identity
            );

            if (obj != null)
            {
                BulletCtrl bulletCtrl = obj.GetComponent<BulletCtrl>();
                Vector2 direction = Vector2.down;
                bulletCtrl.GetMoveBase().SetDirection(direction);
                BulletManager.Instance.AddBullet(bulletCtrl);

                ObjectAttribute objectAttribute = bossStateMachine.GetBossCtrl().GetObjectAttribute();
                if (objectAttribute == null) break;
                bulletCtrl.SetBullet(objectAttribute, bossStateMachine.GetBossCtrl().transform);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

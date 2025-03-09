using System.Collections;
using UnityEngine;

public class RadialAbility : BossAbility
{
    [SerializeField] protected int numberOfBullets = 15;
    [SerializeField] protected float attackRadius = 360f; 
    [SerializeField] protected float initialRotationAngle = 0f; 
    [SerializeField] protected int volleys = 5; 
    [SerializeField] protected float delayBetweenVolleys = 0.5f; 

    public override IEnumerator Execute(Transform shotPoint, BossStateMachine bossStateMachine, BossState bossState)
    {
        float angleIncrement = this.attackRadius / this.numberOfBullets; 

        for (int volley = 0; volley < this.volleys; volley++) 
        {
            for (int i = 0; i < this.numberOfBullets; i++) 
            {
                float angle = this.initialRotationAngle + (i * angleIncrement);

                Transform obj = BulletSpawner.Instance.SpawnBullet(
                    BulletType.RadialAttack,
                    shotPoint.position,
                    Quaternion.identity
                );

                BulletCtrl bulletCtrl = obj.GetComponent<BulletCtrl>();

                Vector2 direction = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                );

                float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                obj.rotation = Quaternion.Euler(0, 0, rotationAngle - 90);
                bulletCtrl.GetMoveBase().SetDirection(direction);

                BulletManager.Instance.AddBullet(bulletCtrl);

                ObjectAttribute objectAttribute = bossStateMachine.GetBossCtrl().GetObjectAttribute();
                if (objectAttribute == null) break;
                bulletCtrl.SetBullet(objectAttribute, bossStateMachine.GetBossCtrl().transform);
            }

            this.initialRotationAngle += 10f;
            yield return new WaitForSeconds(this.delayBetweenVolleys);
        }
    }
}

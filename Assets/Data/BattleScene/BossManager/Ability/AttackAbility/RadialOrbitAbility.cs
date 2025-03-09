using System.Collections;
using UnityEngine;

public class RadialOrbitAbility : BossAbility
{
    [SerializeField] protected int bulletsCount = 12; 
    [SerializeField] protected float attackRadius = 360f; 
    [SerializeField] protected float orbitRadii = 2f; 
    [SerializeField] protected int volleys = 5; 

    public override IEnumerator Execute(Transform shotPoint, BossStateMachine bossStateMachine, BossState bossState)
    {
        float angleStep = this.attackRadius / this.bulletsCount;
        float orbitRadii = this.orbitRadii;
        for (int volley = 0; volley < volleys; volley++) 
        {
            float angle = 0f;

            for (int i = 0; i < this.bulletsCount; i++) 
            {
                float bulletX = shotPoint.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadii;
                float bulletY = shotPoint.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadii;
                Vector3 bulletPosition = new Vector3(bulletX, bulletY, shotPoint.position.z);

                Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + 90f);

                Transform obj = BulletSpawner.Instance.SpawnBullet(
                    BulletType.RadialOrbitAttack,
                    bulletPosition,
                    bulletRotation
                );

                BulletCtrl bulletCtrl = obj.GetComponent<BulletCtrl>();
                BulletManager.Instance.AddBullet(bulletCtrl);

                OrbitalMove orbitalMove = bulletCtrl.GetMoveBase() as OrbitalMove;
                orbitalMove.SetPos(bossStateMachine.GetBossCtrl().transform);
                orbitalMove.SetOrbitRadius(orbitRadii);

                ObjectAttribute objectAttribute = bossStateMachine.GetBossCtrl().GetObjectAttribute();
                if (objectAttribute == null) break;
                bulletCtrl.SetBullet(objectAttribute, bossStateMachine.GetBossCtrl().transform);

                angle += angleStep;
            }

            orbitRadii += this.orbitRadii / 4 ;

            yield return new WaitForSeconds(0.1f);
        }
    }
}

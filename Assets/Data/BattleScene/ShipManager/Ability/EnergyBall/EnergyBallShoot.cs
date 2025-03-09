using System.Collections;
using UnityEngine;

public class EnergyBallShoot : AbilityShoot
{
    protected override IEnumerator ShootType()
    {
        for (int i = 0; i < this.abilityAttributes.ShotsPerAttack; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Transform bullet = this.BulletSpawn(SoundCode.None, this.ShootPoint.position, Quaternion.identity);
            bullet.rotation = Quaternion.LookRotation(Vector3.forward, randomDirection);

            BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();
            bulletCtrl.GetMoveBase().SetDirection(randomDirection);
        }
        yield return base.ShootType();
    }
}

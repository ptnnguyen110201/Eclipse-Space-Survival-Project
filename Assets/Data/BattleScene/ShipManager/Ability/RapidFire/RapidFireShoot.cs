using System.Collections;
using UnityEngine;

public class RapidFireShoot : AbilityShoot
{

    protected override IEnumerator ShootType()
    {
        
        for (int i = 0; i < this.abilityAttributes.ShotsPerAttack; i++)
        {
            Transform target = this.ObjShot();
            if (target != null)
            {
                Vector3 directionToTarget = (target.position - this.ShootPoint.position).normalized;

                Transform bullet = this.BulletSpawn(SoundCode.RapidFireSound, this.ShootPoint.position, Quaternion.identity);
                bullet.rotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
                BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();
                bulletCtrl.GetMoveBase().SetDirection(directionToTarget);
                yield return new WaitForSeconds(this.ShootInterval);
            }
        }
        yield return base.ShootType();
    }
}

using System.Collections;
using UnityEngine;

public class RPGShoot : AbilityShoot
{
   
    protected override IEnumerator ShootType()
    {
        
        for (int i = 0; i < this.abilityAttributes.ShotsPerAttack; i++)
        {
            Transform target = this.ObjShot();
            Vector3 directionToTarget = (target.position - this.ShootPoint.position).normalized;

            Transform bullet = this.BulletSpawn(SoundCode.None, this.ShootPoint.position, Quaternion.identity);       
            bullet.rotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
            BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();
            bulletCtrl.GetMoveBase().SetDirection(directionToTarget);

            yield return new WaitForSeconds(this.ShootInterval);
        }
        yield return base.ShootType();
    }
}

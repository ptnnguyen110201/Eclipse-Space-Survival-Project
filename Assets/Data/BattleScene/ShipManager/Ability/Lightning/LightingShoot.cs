using System.Collections;
using UnityEngine;

public class LightingShoot : AbilityShoot
{

    protected override IEnumerator ShootType()
    {

        for (int i = 0; i < this.abilityAttributes.ShotsPerAttack; i++)
        {
            Transform nearEnemy = this.ObjShot();
            if (nearEnemy != null) 
            {
                this.BulletSpawn(SoundCode.LightingSound,nearEnemy.position, nearEnemy.rotation);
            }
            yield return new WaitForSeconds(this.ShootInterval);
        }
        yield return base.ShootType();
    }
}

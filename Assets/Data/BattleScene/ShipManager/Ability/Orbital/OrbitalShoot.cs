using System.Collections;
using UnityEngine;

public class OrbitalShoot : AbilityShoot
{
    [SerializeField] protected float distance = 2f;

    protected override IEnumerator ShootType()
    {
        int BulletCount = this.abilityAttributes.ShotsPerAttack;
        this.SpawnBulletsAroundPoint(this.ShootPoint.position, BulletCount);
        yield return base.ShootType();
    }

   private void SpawnBulletsAroundPoint(Vector3 center, int bulletsCount)
{
    float angleStep = 360f / bulletsCount;
    float angle = 0f;

    for (int i = 0; i < bulletsCount; i++)
    {
        float bulletX = center.x + Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float bulletY = center.y + Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
        Vector3 bulletPosition = new Vector3(bulletX, bulletY, center.z);

            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + 90f);

            Transform newBullet = this.BulletSpawn(SoundCode.None, bulletPosition, bulletRotation);
            BulletCtrl bulletCtrl = newBullet.transform.GetComponent<BulletCtrl>();
            OrbitalMove orbitalMove = bulletCtrl.GetMoveBase() as OrbitalMove;
            orbitalMove.SetPos(ShipManager.Instance.GetShipCtrl().transform);
            orbitalMove.SetOrbitRadius(distance);

        angle += angleStep;
    }
}
}

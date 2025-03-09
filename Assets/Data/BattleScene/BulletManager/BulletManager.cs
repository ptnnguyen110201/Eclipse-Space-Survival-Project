using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BulletManager : FuncManager
{
    private static BulletManager instance;
    public static BulletManager Instance => instance;
    [SerializeField] public List<BulletCtrl> bulletCtrls = new();

    protected override void Awake()
    {
        if (BulletManager.instance != null) Debug.LogError("Only 1 BulletManager allow to exist");
        BulletManager.instance = this;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(this.MoveAllBulletCoroutine());
    }
    public void AddBullet(BulletCtrl bulletCtrl)
    {
        if (bulletCtrl == null || bulletCtrls.Contains(bulletCtrl)) return;
        this.bulletCtrls.Add(bulletCtrl);
    }

   
    private IEnumerator MoveAllBulletCoroutine()
    {
        while (true)
        {
            this.MoveAllitems();
            yield return new WaitForFixedUpdate();
        }
    }

    private void MoveAllitems()
    {
        foreach (BulletCtrl bulletCtrl in this.bulletCtrls)
        {
            if (bulletCtrl != null && bulletCtrl.gameObject.activeSelf)
            {
                MoveBase moveBase = bulletCtrl.GetMoveBase();
                moveBase.Move();
            }
        }
    }


}
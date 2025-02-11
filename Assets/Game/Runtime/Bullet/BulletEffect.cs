using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 子弹特效
/// </summary>
public class BulletEffect
{
    public readonly string prefab;
    GameObject obj;
    Transform tran;
    int player;
    int weapon;

    // Transform targetTran;
    public bool flag;
    
    private bool isChallengePlay;//是否挑战玩法

    public BulletEffect(string prefab)
    {
        this.flag = false;
        this.prefab = prefab;
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(prefab);
        this.obj = Object.Instantiate(nodePrefab);
        this.tran = this.obj.transform;
        this.obj.SetActive(false);
        this.tran = this.obj.transform;
        this.tran.parent = CameraController.Instance.BulletCameraTran;
        this.tran.localPosition = Vector3.zero;
        this.tran.localEulerAngles = Vector3.zero;
        this.tran.localScale = Vector3.one;
    }

    /// <summary>
    /// 显示子弹，并产生射击效果
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="weapon">武器类型</param>
    /// <param name="from">射击起始点</param>
    /// <param name="to">射击终点</param>
    public void Show(int player, int weapon, Vector3 from, Vector3 to)
    {
        isChallengePlay = GameSceneMgr.Instance.CurScenePlayType() == GameScenePlay.Challenge;
        float speed = 150;
        Ease moveEase = Ease.Linear;
        if (weapon == 1)
        {
            
        }
        else if (weapon == 2)
        {
            moveEase = Ease.InSine;
            speed = 100;
        }
        this.flag = true;
        this.player = player;
        this.weapon = weapon;
        // this.targetTran = to;

        from.z = 100;
        from = CameraController.Instance.BulletCamera.ScreenToWorldPoint(from);
        Vector3 target = to;
        target.z = 100;
        target = CameraController.Instance.BulletCamera.ScreenToWorldPoint(target);

        this.obj.SetActive(true);
        this.tran.position = from;
        this.tran.eulerAngles = new Vector3(0, 0, angle_360(from, target) + 90);
        this.tran.DOMove(target, speed).SetSpeedBased()
            .SetEase(moveEase)
            .OnComplete(() =>
            {
                // Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(to.position);
                // if (Physics.Raycast(ray, out RaycastHit hit))
                // {
                //     var unitInfo = hit.collider.gameObject.GetComponent<UnitInfo>();
                //     if (unitInfo)
                //     {
                //         unitInfo.HitByPlayer(player);
                //         
                //         BulletEffectMgr.Instance.ShowBulletHitEffect(this.player, this.weapon, to.position);
                //     }
                // }

                if (this.weapon == 2) //火箭炮
                {
                    if (isChallengePlay)
                    {
                        List<MonsterBase> killUnits = UnitMgr.Instance.GetWeapon2KillUnits(CameraController.Instance.BulletCamera.WorldToScreenPoint(target));
                        if (killUnits is { Count: > 0 })
                        {
                            for (var i = 0; i < killUnits.Count; i++)
                            {
                                killUnits[i].HitByPlayer(player, killUnits[i].Config.health, this.weapon);
                            }
                        }
                    }
                    else
                    {
                        List<PGL_MonsterBase> killUnits = PGL_MonsterMgr.Instance.GetWeapon2KillUnits(CameraController.Instance.BulletCamera.WorldToScreenPoint(target));
                        if (killUnits is { Count: > 0 })
                        {
                            for (var i = 0; i < killUnits.Count; i++)
                            {
                                killUnits[i].HitByPlayer(player, int.MaxValue, this.weapon);
                            }
                        }
                    }
                    
                    // 发送震屏事件
                    EventMgr.Instance.Emit(CameraController.CameraShakeEvent, null);
                }
                else if (this.weapon == 1) //散弹
                {
                    Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(CameraController.Instance.BulletCamera.WorldToScreenPoint(target));
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (isChallengePlay)
                        {
                            var unitInfo = hit.collider.gameObject.GetComponent<MonsterBase>();
                            if (unitInfo)
                            {
                                unitInfo.HitByPlayer(player, 1, this.weapon);
                            }
                        }
                        else
                        {
                            var unitInfo = hit.collider.gameObject.GetComponent<PGL_MonsterBase>();
                            if (unitInfo)
                            {
                                unitInfo.HitByPlayer(player, 1, this.weapon);
                            }
                        }
                    }
                }
                else
                {
                    Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(CameraController.Instance.BulletCamera.WorldToScreenPoint(target));
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (isChallengePlay)
                        {
                            var unitInfo = hit.collider.gameObject.GetComponent<MonsterBase>();
                            if (unitInfo)
                            {
                                unitInfo.HitByPlayer(player, 1, this.weapon);
                            }
                        }
                        else
                        {
                            var unitInfo = hit.collider.gameObject.GetComponent<PGL_MonsterBase>();
                            if (unitInfo)
                            {
                                unitInfo.HitByPlayer(player, 1, this.weapon);
                            }
                        }
                    }
                }

                BulletEffectMgr.Instance.ShowBulletHitEffect(this.player, this.weapon, target); //显示击中特效
                this.flag = false;
                Recycle();

            });
    }

    // public void Update()
    // {
    //     if (!this.flag)
    //         return;
    //
    //     Vector3 target = this.targetTran.position;
    //     target.z = 100;
    //     target = CameraController.Instance.BulletCamera.ScreenToWorldPoint(target);
    //     Quaternion dir = Quaternion.Euler(0, 0, angle_360(this.tran.position, target) + 90);
    //     this.tran.rotation = Quaternion.Lerp(this.tran.rotation, dir, Time.deltaTime * 100);
    //     this.tran.position = Vector3.Lerp(this.tran.position, target, Time.deltaTime * 20);
    //
    //     if (Vector3.Distance(this.tran.position, target) < 1f)
    //     {
    //         Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(this.targetTran.position);
    //         if (Physics.Raycast(ray, out RaycastHit hit))
    //         {
    //             var unitInfo = hit.collider.gameObject.GetComponent<Unit>();
    //             if (unitInfo)
    //             {
    //                 unitInfo.HitByPlayer(player);
    //                 
    //             }
    //         }
    //         BulletEffectMgr.Instance.ShowBulletHitEffect(this.player, this.weapon, target);
    //         this.flag = false;
    //         Recycle();
    //     }
    // }

    // 回收到缓存池
    public void Recycle()
    {
        this.flag = false;
        obj.SetActive(false);
        // BulletEffectMgr.Instance.RecycleBulletEffect(this.prefab, this);
    }

    public void Destroy()
    {
        this.flag = false;
        Object.DestroyImmediate(obj);
    }

    /// <summary>
    /// 旋转特效角度 ---> 起点转向终点
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float angle_360(Vector3 from, Vector3 to)
    {
        float x = from.x - to.x;
        float y = from.y - to.y;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        float cos = x / hypotenuse;
        float radian = Mathf.Acos(cos);

        float angle = 180 / (Mathf.PI / radian);
        if (y < 0)
        {
            angle = -angle;
        }
        else if ((y == 0) && (x < 0))
        {
            angle = 180;
        }
        return angle;
    }
}


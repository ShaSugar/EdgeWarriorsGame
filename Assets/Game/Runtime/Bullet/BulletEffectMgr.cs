using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 子弹与击中特效管理
/// </summary>
public class BulletEffectMgr : UnitySingleton<BulletEffectMgr>
{
    static readonly string[] BulletEffectPrefab = new[] //子弹
    {
        "BulletEffect/LauncherBullet0",
        "BulletEffect/LauncherBullet1",
        "BulletEffect/LauncherBullet2",
        "BulletEffect/LauncherBullet3", //单射
        "BulletEffect/LauncherBullet0",
        "BulletEffect/LauncherBullet1",
        "BulletEffect/LauncherBullet2",
        "BulletEffect/LauncherBullet3", //散弹
        "BulletEffect/LauncherBullet20",
        "BulletEffect/LauncherBullet21",
        "BulletEffect/LauncherBullet22",
        "BulletEffect/LauncherBullet23", //火箭炮
    };
    static readonly string[] BulletHitEffectPrefab = new[] //击中
    {
        "BulletEffect/LauncherHit0",
        "BulletEffect/LauncherHit1",
        "BulletEffect/LauncherHit2",
        "BulletEffect/LauncherHit3",
        "BulletEffect/LauncherHit0",
        "BulletEffect/LauncherHit1",
        "BulletEffect/LauncherHit2",
        "BulletEffect/LauncherHit3",
        "BulletEffect/LauncherHit20",
        "BulletEffect/LauncherHit21",
        "BulletEffect/LauncherHit22",
        "BulletEffect/LauncherHit23",
    };

    Camera bulletCamera;
    Transform tran;
    /// <summary>
    /// 子弹存储池
    /// </summary>
    Dictionary<string, Stack<BulletEffect>> bulletEffectDic;
    /// <summary>
    /// 击中特效存储池
    /// </summary>
    Dictionary<string, Stack<BulletHitEffect>> bulletHitEffectDic;
    /// <summary>
    /// 激活子弹池
    /// </summary>
    List<BulletEffect> activeBullets;
    /// <summary>
    /// 缓存子弹
    /// </summary>
    List<BulletEffect> clearCacheBullets;
    public void Init()
    {
        this.tran = CameraController.Instance.BulletCameraTran;
        this.bulletCamera = this.tran.GetComponent<Camera>();

        bulletEffectDic = new Dictionary<string, Stack<BulletEffect>>();
        bulletHitEffectDic = new Dictionary<string, Stack<BulletHitEffect>>();
        activeBullets = new List<BulletEffect>();
        clearCacheBullets = new List<BulletEffect>();

        // 关卡开始--清除所有子弹
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, (_, udata) => { RecycleAllBullet(); });
        // 倒计时结束---扣除所有玩家血量-回收所有子弹
        EventMgr.Instance.AddListener(CountDown_UICtrl.CountDownFinishedEvent, (_, _) => { RecycleAllBullet(); });
        // 关卡完成（过关）事件--清除所有子弹
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, (_, _) => { RecycleAllBullet(); });
        // 关卡失败（游戏结束）事件--清除所有子弹
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, (_, _) => { RecycleAllBullet(); });
    }

    void Update()
    {
        if (activeBullets.Count <= 0)
            return;

        clearCacheBullets.Clear(); //清除缓存子弹
        for (var i = 0; i < activeBullets.Count; i++)
        {
            // activeBullets[i].Update();
            if (activeBullets[i].flag == false) //子弹激活时，存进缓存池
            {
                clearCacheBullets.Add(activeBullets[i]);
            }
        }

        for (var i = 0; i < clearCacheBullets.Count; i++) //释放缓存
        {
            RecycleBulletEffect(clearCacheBullets[i].prefab, clearCacheBullets[i]);
        }
    }

    /// <summary>
    /// 释放所有子弹缓存
    /// </summary>
    void RecycleAllBullet()
    {
        for (var i = 0; i < activeBullets.Count; i++)
        {
            activeBullets[i].Recycle();
        }
    }

    /// <summary>
    /// 显示子弹特效
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="weapon">武器类型</param>
    /// <param name="from">射击起点</param>
    /// <param name="to">射击终点</param>
    public void ShowBulletEffect(int player, int weapon, Vector3 from, Transform to)
    {
        string prefab = GetBulletEffectPrefab(player, weapon);

        if (weapon == 1) //散弹
        { 
            Vector3 centerPoint = to.position; //以终点为中间点
            var points = new List<Vector3>
            {
                centerPoint
            };
            //计算获取，以中间点辐射出的范围点
            points.AddRange(CalculationRadian(5, centerPoint, 50, 2));
            points.AddRange(CalculationRadian(6, centerPoint, 100, 4));
            //进行遍历显示这些点
            for (var i = 0; i < points.Count; i++)
            {
                BulletEffect bulletEffect = GetBulletEffect(prefab);
                bulletEffect.Show(player, weapon, from, points[i]);
                activeBullets.Add(bulletEffect); //显示出的子弹，存进激活缓存池
            }
        }
        else
        {
            BulletEffect bulletEffect = GetBulletEffect(prefab);
            bulletEffect.Show(player, weapon, from, to.position);
            activeBullets.Add(bulletEffect);
        }

    }

    /// <summary>
    /// 显示子弹击中特效
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="weapon">武器类型</param>
    /// <param name="pos">终点</param>
    public void ShowBulletHitEffect(int player, int weapon, Vector3 pos)
    {
        string prefab = GetBulletHitEffectPrefab(player, weapon);

        BulletHitEffect bulletHitEffect = GetBulletHitEffect(prefab);

        bulletHitEffect.Show(pos);
    }

    /// <summary>
    /// 获取子弹预制件存储名称
    /// </summary>
    /// <param name="player"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    static string GetBulletEffectPrefab(int player, int weapon)
    {
        return BulletEffectPrefab[player + weapon * 4];
    }
    /// <summary>
    /// 获取击中预制件存储名称
    /// </summary>
    /// <param name="player"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    static string GetBulletHitEffectPrefab(int player, int weapon)
    {
        return BulletHitEffectPrefab[player + weapon * 4];
    }
    /// <summary>
    /// 获取对应子弹特效对象
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    BulletEffect GetBulletEffect(string prefab)
    {
        if (this.bulletEffectDic.ContainsKey(prefab))
        {
            if (this.bulletEffectDic[prefab] != null && this.bulletEffectDic[prefab].Count > 0)
            {
                return this.bulletEffectDic[prefab].Pop();
            }
        }

        return new BulletEffect(prefab);
    }
    /// <summary>
    /// 释放子弹特效
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="bulletEffect"></param>
    public void RecycleBulletEffect(string prefab, BulletEffect bulletEffect)
    {
        if (activeBullets.Contains(bulletEffect))
            activeBullets.Remove(bulletEffect);

        if (!this.bulletEffectDic.ContainsKey(prefab))
        {
            this.bulletEffectDic.Add(prefab, new Stack<BulletEffect>());
        }

        this.bulletEffectDic[prefab].Push(bulletEffect);
    }

    /// <summary>
    /// 获取子弹击中特效
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    BulletHitEffect GetBulletHitEffect(string prefab)
    {
        if (this.bulletHitEffectDic.ContainsKey(prefab))
        {
            if (this.bulletHitEffectDic[prefab] != null && this.bulletHitEffectDic[prefab].Count > 0)
            {
                return this.bulletHitEffectDic[prefab].Pop();
            }
        }

        return new BulletHitEffect(prefab);
    }
    /// <summary>
    /// 释放子弹击中特效
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="bulletHitEffect"></param>
    public void RecycleBulletHitEffect(string prefab, BulletHitEffect bulletHitEffect)
    {
        if (!this.bulletHitEffectDic.ContainsKey(prefab))
        {
            this.bulletHitEffectDic.Add(prefab, new Stack<BulletHitEffect>());
        }

        this.bulletHitEffectDic[prefab].Push(bulletHitEffect);
    }
    /// <summary>
    /// 计算以中间点为基础，辐射周边点位位置
    /// </summary>
    /// <param name="index">指定生成的点数 ---> 圆分割的数量点</param>
    /// <param name="point">圆心的坐标</param>
    /// <param name="r"></param>
    /// <param name="count">期望的点的数量。若生成的点数超过这个值，代码会随机移除点</param>
    /// <returns></returns>
    public static List<Vector3> CalculationRadian(int index, Vector3 point, float r, int count)
    {
        var angleList = new List<Vector3>();
        float curAngle = 360 / index * Mathf.Deg2Rad;
        float angle = 0;
        //循环生成圆周上的点
        for (var i = 0; i < index; i++)
        {
            angle += curAngle; //angle 每次增加一个 curAngle，即每个点相隔的角度
            float x = point.x + r * Mathf.Cos(angle);
            float y = point.y + r * Mathf.Sin(angle);
            angleList.Add(new Vector3(x, y, 0));
        }
        //随机移除点
        while (angleList.Count > count)
        {
            angleList.RemoveAt(Random.Range(0,angleList.Count));
        }
        return angleList;
    }
    public static List<T> GetRandom<T>(List<T> nums, int count)
    {
        if (count > nums.Count)
        {
            Debug.LogError("要取的个数大于数组长度！！！！");
            return null;
        }
        var result = new List<T>();
        var id = new List<int>();
        for (var i = 0; i < nums.Count; i++)
        {
            id.Add(i);
        }
        int r;
        while (id.Count > nums.Count - count)
        {
            r = Random.Range(0, id.Count);
            result.Add(nums[id[r]]);
            id.Remove(id[r]);
        }

        return result;
    }
}

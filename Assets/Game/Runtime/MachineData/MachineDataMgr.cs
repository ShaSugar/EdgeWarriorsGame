using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EItemType
{
    /// <summary>
    /// 血量补给
    /// </summary>
    HP_SUPPLY = 1,
    /// <summary>
    /// 积分翻倍
    /// </summary>
    SCORE_DOUBLE = 2,
    /// <summary>
    /// 散弹枪
    /// </summary>
    SHELL_GUN = 3,
    /// <summary>
    /// 火箭炮
    /// </summary>
    BOLT_CANNON = 4,
}

public class MachineDataMgr : Singleton<MachineDataMgr>
{
    /// <summary>
    /// 数据更新事件
    /// </summary>
    public const string DataUpdateEvent = "MachineDataMgr_DataUpdateEvent";

    /// <summary>
    /// 大转盘礼物数量
    /// </summary>
    const int BigWheelGiftCount = 8;
    /// <summary>
    /// 最大关卡数量
    /// </summary>
    public const int MaxLevelCount = 10;
    
    /// <summary>
    /// 语言版本(0中文，1英文)
    /// </summary>
    public const string SaveKey_LanguageVersion = "SaveKey_LanguageVersion";
    /// <summary>
    /// 当前拥有币数
    /// </summary>
    const string SaveKey_CoinNumForMachine = "SaveKey_CoinNumForMachine";
    /// <summary>
    /// 玩一次游戏需要投币数量
    /// </summary>
    const string SaveKey_CoinNumForOneGame = "SaveKey_CoinNumForOneGame";
    /// <summary>
    /// 可玩玩家数
    /// </summary>
    const string SaveKey_PlayerShowCount = "SaveKey_PlayerShowCount";
    /// <summary>
    /// 关卡时间
    /// </summary>
    const string SaveKey_LevelTime = "SaveKey_LevelTime";
    /// <summary>
    /// 关卡击杀怪物目标数量
    /// </summary>
    const string SaveKey_LevelTargetMonsterCount = "SaveKey_LevelTargetMonsterCount";
    /// <summary>
    /// 关卡目标积分
    /// </summary>
    const string SaveKey_LevelTargetScore = "SaveKey_LevelTargetScore";
    /// <summary>
    /// 怪物生命值
    /// </summary>
    const string SaveKey_MonsterHP = "SaveKey_MonsterHP";
    /// <summary>
    /// 怪物攻击力
    /// </summary>
    const string SaveKey_MonsterATT = "SaveKey_MonsterATT";
    /// <summary>
    /// 玩家已玩币数
    /// </summary>
    const string SaveKey_CoinNumWithPlayer = "SaveKey_CoinNumWithPlayer";
    /// <summary>
    /// 触发大转盘所需的币数
    /// </summary>
    const string SaveKey_CoinNumForBigWheel = "SaveKey_CoinNumForBigWheel";
    /// <summary>
    /// 到达大转盘所需币数后的触发百分比
    /// </summary>
    const string SaveKey_BigWheelPercent = "SaveKey_BigWheelPercent";
    /// <summary>
    /// 大转盘每个位的权重
    /// </summary>
    const string SaveKey_BigWheelWeight = "SaveKey_BigWheelWeight";
    /// <summary>
    /// 击杀怪物获得道具百分比
    /// </summary>
    const string SaveKey_DropItemProbability = "SaveKey_DropItemProbability";
    /// <summary>
    /// 每种道具的权重
    /// </summary>
    const string SaveKey_DropItemWeight = "SaveKey_DropItemWeight";

    const string SaveKey_HP_SUPPLY = "SaveKey_HP_SUPPLY";
    const string SaveKey_SCORE_DOUBLE = "SaveKey_SCORE_DOUBLE";
    const string SaveKey_SHELL_GUN = "SaveKey_SHELL_GUN";
    const string SaveKey_BOLT_CANNON = "SaveKey_BOLT_CANNON";

    private ItemConfigData itemConfigData;
    
    const string SaveKey_Weapon_Interval = "SaveKey_Weapon_Interval_{0}";
    private WeaponConfigData weaponConfigData;
    // 获取开枪时间间隔
    public float GetWeaponInterval(WeaponType weaponType)
    {
        float interval = .3f;
        string saveKey = string.Format(SaveKey_Weapon_Interval, weaponType);
        if (PlayerPrefs.HasKey(saveKey))
            interval = PlayerPrefs.GetFloat(saveKey);
        else
        {
            interval = weaponType switch
            {
                WeaponType.Default => weaponConfigData.defaultWeapon.interval,
                WeaponType.Shotgun => weaponConfigData.shotgunWeapon.interval,
                WeaponType.RocketGun => weaponConfigData.rocketGunWeapon.interval,
                _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
            };
        }
        
        return interval;
    }
    //设置开枪时间间隔
    public void SetWeaponInterval(WeaponType weaponType, float interval)
    {
        string saveKey = string.Format(SaveKey_Weapon_Interval, weaponType);
        PlayerPrefs.SetFloat(saveKey, interval);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 当前机台拥有币数
    /// </summary>
    public int CoinNumForMachine
    {
        get => this.coinNumForMachine;
    }
    int coinNumForMachine;

    /// <summary>
    /// 获取玩一局游戏需要币数
    /// </summary>
    public int CoinNumForOneGame
    {
        get => this.coinNumForOneGame;
    }
    /// <summary>
    /// 设置玩一局游戏需要币数
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool SetCoinNumForOneGame(int num)
    {
        if (num < 1)
            return false;

        this.coinNumForOneGame = num;
        PlayerPrefs.SetInt(SaveKey_CoinNumForOneGame, this.coinNumForOneGame);
        PlayerPrefs.Save();

        EventMgr.Instance.Emit(MachineDataMgr.DataUpdateEvent, null);
        return true;
    }
    int coinNumForOneGame;

    /// <summary>
    /// 获取显示控台数量
    /// </summary>
    public int PlayerShowCount { get; private set; }

    /// <summary>
    /// 设置显示控台数量
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool SetPlayerShowCount(int count)
    {
        if (count is <= 1 or > GameApp.MAX_PLAYER_COUNT)
            return false;

        this.PlayerShowCount = count;
        PlayerPrefs.SetInt(SaveKey_PlayerShowCount, this.PlayerShowCount);
        PlayerPrefs.Save();

        EventMgr.Instance.Emit(MachineDataMgr.DataUpdateEvent, null);
        return true;
    }

    /// <summary>
    /// 关卡时间
    /// </summary>
    Dictionary<int, int[]> levelTimes;
    /// <summary>
    /// 关卡击杀怪物目标数量（0为无限制）
    /// </summary>
    Dictionary<int, int[]> levelTargetMonsterCounts;
    /// <summary>
    /// 关卡目标积分（0为无限制）
    /// </summary>
    Dictionary<int, int[]> levelTargetScores;
    /// <summary>
    /// 获取关卡时间
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetLevelTime(int scene, int level)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return 0;

        if (this.levelTimes == null || !this.levelTimes.ContainsKey(scene) || this.levelTimes[scene].Length < level)
            return 0;

        return this.levelTimes[scene][level - 1];
    }
    /// <summary>
    /// 设置关卡时间
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool SetLevelTime(int scene, int level, int time)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return false;

        if (this.levelTimes == null || !this.levelTimes.ContainsKey(scene) || this.levelTimes[scene].Length < level)
            return false;

        this.levelTimes[scene][level - 1] = time;

        PlayerPrefs.SetInt($"{SaveKey_LevelTime}-{scene}-{level}", this.levelTimes[scene][level - 1]);
        PlayerPrefs.Save();

        return true;
    }
    /// <summary>
    /// 获取关卡击杀怪物数量(0为没有击杀怪物数量过关条件)
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetLevelTargetMonsterCount(int scene, int level)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return 0;

        if (this.levelTargetMonsterCounts == null ||
            !this.levelTargetMonsterCounts.ContainsKey(scene) ||
            this.levelTargetMonsterCounts[scene].Length < level)
            return 0;

        return this.levelTargetMonsterCounts[scene][level - 1];
    }
    /// <summary>
    /// 设置关卡击杀怪物数量
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool SetLevelTargetMonsterCount(int scene, int level, int count)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return false;

        if (this.levelTargetMonsterCounts == null ||
            !this.levelTargetMonsterCounts.ContainsKey(scene) ||
            this.levelTargetMonsterCounts[scene].Length < level)
            return false;

        this.levelTargetMonsterCounts[scene][level - 1] = count;

        PlayerPrefs.SetInt($"{SaveKey_LevelTargetMonsterCount}{scene}-{level}",
            this.levelTargetMonsterCounts[scene][level - 1]);
        PlayerPrefs.Save();

        return true;
    }
    /// <summary>
    /// 获取关卡目标积分（0为没有积分过关条件）
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetLevelTargetScore(int scene, int level)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return 0;

        if (this.levelTargetScores == null ||
            !this.levelTargetScores.ContainsKey(scene) ||
            this.levelTargetScores[scene].Length < level)
            return 0;

        return this.levelTargetScores[scene][level - 1];
    }
    /// <summary>
    /// 设置关卡目标积分
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="level"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public bool SetLevelTargetScore(int scene, int level, int score)
    {
        if (level is < 1 or > MachineDataMgr.MaxLevelCount)
            return false;

        if (this.levelTargetScores == null ||
            !this.levelTargetScores.ContainsKey(scene) ||
            this.levelTargetScores[scene].Length < level)
            return false;

        this.levelTargetScores[scene][level - 1] = score;
        PlayerPrefs.SetInt($"{SaveKey_LevelTargetScore}{scene}-{level}", this.levelTargetScores[scene][level - 1]);
        PlayerPrefs.Save();

        return true;
    }

    /// <summary>
    /// 玩家已玩币数
    /// </summary>
    int[] coinNumWithPlayers;
    /// <summary>
    /// 触发大转盘所需币数
    /// </summary>
    int coinNumForBigWheel;
    /// <summary>
    /// 获取触发大转盘所需洗码币数
    /// </summary>
    public int CoinNumForBigWheel
    {
        get => this.coinNumForBigWheel;
    }
    /// <summary>
    /// 设置触发大转盘所需洗码币数
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool SetCoinNumForBigWheel(int num)
    {
        if (num < 1)
            return false;

        this.coinNumForBigWheel = num;
        PlayerPrefs.SetInt(SaveKey_CoinNumForBigWheel, this.coinNumForBigWheel);
        PlayerPrefs.Save();

        return true;
    }
    int bigWheelPercent;
    /// <summary>
    /// 获取满足大转盘洗码币数后触发大转盘百分比
    /// </summary>
    public int BigWheelPercent
    {
        get => this.bigWheelPercent;
    }
    /// <summary>
    /// 设置满足大转盘洗码币数后触发大转盘百分比
    /// </summary>
    /// <param name="percent"></param>
    /// <returns></returns>
    public bool SetBigWheelPercent(int percent)
    {
        if (percent is < 1 or > 100)
            return false;

        this.bigWheelPercent = percent;
        PlayerPrefs.SetInt(SaveKey_BigWheelPercent, this.bigWheelPercent);
        PlayerPrefs.Save();

        return true;
    }
    /// <summary>
    /// 大转盘每个位置的权重
    /// </summary>
    int[] bigWheelWeights;
    /// <summary>
    /// 获取大转盘中奖位置权重
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetBigWheelWeight(int index)
    {
        if (index is < 0 or > BigWheelGiftCount)
            return 0;

        if (this.bigWheelWeights == null || this.bigWheelWeights.Length < index)
            return 0;

        return this.bigWheelWeights[index];
    }
    /// <summary>
    /// 设置大转盘中奖位置权重
    /// </summary>
    /// <param name="index"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public bool SetBigWheelWeight(int index, int weight)
    {
        if (index is < 0 or > BigWheelGiftCount)
            return false;

        if (this.bigWheelWeights == null || this.bigWheelWeights.Length < index)
            return false;

        this.bigWheelWeights[index] = weight;
        this.bigWheelTotalWeight = this.bigWheelWeights.Sum();

        PlayerPrefs.SetInt($"{SaveKey_BigWheelWeight}{index}", this.bigWheelWeights[index]);
        PlayerPrefs.Save();

        return true;
    }
    int bigWheelTotalWeight;

    /// <summary>
    /// 击杀怪物获得道具概率
    /// </summary>
    int dropItemProbability;
    /// <summary>
    /// 获取击杀怪物获得道具（血量补给、双倍积分、散弹枪、火箭炮）百分比
    /// </summary>
    public int GetDropItemProbability
    {
        get => this.dropItemProbability;
    }
    /// <summary>
    /// 设置击杀怪物获得道具（血量补给、双倍积分、散弹枪、火箭炮）百分比
    /// </summary>
    /// <param name="probability"></param>
    /// <returns></returns>
    public bool SetDropItemProbability(int probability)
    {
        if (probability is < 1 or > 100)
            return false;

        this.dropItemProbability = probability;
        PlayerPrefs.SetInt(SaveKey_DropItemProbability, this.dropItemProbability);
        PlayerPrefs.Save();

        return true;
    }
    /// <summary>
    /// 每种道具的权重
    /// </summary>
    int[] dropItemWeights;
    int dropItemTotalWeight;
    /// <summary>
    /// 获取道具权重
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetDropItemWeight(EItemType type)
    {
        if (type is < EItemType.HP_SUPPLY or > EItemType.BOLT_CANNON)
            return 0;

        if (this.dropItemWeights == null || this.dropItemWeights.Length < (int)type)
            return 0;

        return this.dropItemWeights[(int)type - 1];
    }
    /// <summary>
    /// 设置道具权重
    /// </summary>
    /// <param name="type"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public bool SetDropItemWeight(EItemType type, int weight)
    {
        if (type is < EItemType.HP_SUPPLY or > EItemType.BOLT_CANNON)
            return false;

        if (this.dropItemWeights == null || this.dropItemWeights.Length < (int)type)
            return false;

        this.dropItemWeights[(int)type - 1] = weight;
        this.dropItemTotalWeight = this.dropItemWeights.Sum();

        PlayerPrefs.SetInt($"{SaveKey_DropItemWeight}{(int)type}", this.dropItemWeights[(int)type - 1]);
        PlayerPrefs.Save();

        return true;
    }

    Dictionary<ERealUnitType, int> unitHPs;
    Dictionary<ERealUnitType, int> unitATTs;
    /// <summary>
    /// 获取怪物血量
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetUnitHP(ERealUnitType type)
    {
        return this.unitHPs?.GetValueOrDefault(type, 0) ?? 0;
    }
    /// <summary>
    /// 获取怪物攻击力
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetUnitATT(ERealUnitType type)
    {
        return this.unitATTs?.GetValueOrDefault(type, 0) ?? 0;
    }
    /// <summary>
    /// 设置怪物血量
    /// </summary>
    /// <param name="type"></param>
    /// <param name="hp"></param>
    /// <returns></returns>
    public bool SetUnitHP(ERealUnitType type, int hp)
    {
        this.unitHPs ??= new Dictionary<ERealUnitType, int>();

        this.unitHPs[type] = hp;
        PlayerPrefs.SetInt(SaveKey_MonsterHP + type.ToString(), hp);
        PlayerPrefs.Save();

        return true;
    }
    /// <summary>
    /// 设置怪物攻击力
    /// </summary>
    /// <param name="type"></param>
    /// <param name="att"></param>
    /// <returns></returns>
    public bool SetUnitATT(ERealUnitType type, int att)
    {
        this.unitATTs ??= new Dictionary<ERealUnitType, int>();

        this.unitATTs[type] = att;
        PlayerPrefs.SetInt(SaveKey_MonsterATT + type.ToString(), att);
        PlayerPrefs.Save();

        return true;
    }

    bool isChineseLanguageVersion;
    public bool IsChineseLanguageVersion
    {
        get => this.isChineseLanguageVersion;
        set
        {
            this.isChineseLanguageVersion = value;
            PlayerPrefs.SetInt(SaveKey_LanguageVersion, this.isChineseLanguageVersion ? 0 : 1);
            PlayerPrefs.Save();

            ReloadData();
        }
    }
    public void Init()
    {
        this.coinNumWithPlayers = new int[GameApp.MAX_PLAYER_COUNT];
        this.levelTimes = new Dictionary<int, int[]>();
        this.levelTargetMonsterCounts = new Dictionary<int, int[]>();
        this.levelTargetScores = new Dictionary<int, int[]>();
        this.bigWheelWeights = new int[BigWheelGiftCount];
        this.unitHPs = new Dictionary<ERealUnitType, int>();
        this.unitATTs = new Dictionary<ERealUnitType, int>();
        this.dropItemWeights = new int[(int)EItemType.BOLT_CANNON - (int)EItemType.HP_SUPPLY + 1];
        this.itemConfigData = ResMgr.Instance.LoadAssetSync<ItemConfigData>("Config/itemConfigData");
        this.weaponConfigData = ResMgr.Instance.LoadAssetSync<WeaponConfigData>("Config/WeaponConfigData");
        
        if(!SetDefaultData())
            ReloadData();
    }
    void ReloadData()
    {
        this.isChineseLanguageVersion = PlayerPrefs.GetInt(SaveKey_LanguageVersion, 0) == 0;
        this.coinNumForMachine = PlayerPrefs.GetInt(SaveKey_CoinNumForMachine, 0);
        this.coinNumForOneGame = PlayerPrefs.GetInt(SaveKey_CoinNumForOneGame, 5);
        
        this.levelTimes.Clear();
        this.levelTargetMonsterCounts.Clear();
        this.levelTargetScores.Clear();
        for (var i = 1; i < 11; i++)
        {
            this.levelTimes.Add(i, new int[MaxLevelCount]);
            for (var j = 1; j <= MaxLevelCount; j++)
            {
                this.levelTimes[i][j - 1] = PlayerPrefs.GetInt($"{SaveKey_LevelTime}{i}-{j}", 120);
            }
        }
        for (var i = 1; i < 11; i++)
        {
            this.levelTargetMonsterCounts.Add(i, new int[MaxLevelCount]);
            for (var j = 1; j <= MaxLevelCount; j++)
            {
                this.levelTargetMonsterCounts[i][j - 1] =
                    PlayerPrefs.GetInt($"{SaveKey_LevelTargetMonsterCount}{i}-{j}", 30);
            }
        }
        for (var i = 1; i < 11; i++)
        {
            this.levelTargetScores.Add(i, new int[MaxLevelCount]);
            for (var j = 1; j <= MaxLevelCount; j++)
            {
                this.levelTargetScores[i][j - 1] =
                    PlayerPrefs.GetInt($"{SaveKey_LevelTargetScore}{i}-{j}", 1000);
            }
        }
        this.PlayerShowCount = PlayerPrefs.GetInt(SaveKey_PlayerShowCount, 4);
        
        for (var i = 0; i < this.coinNumWithPlayers.Length; i++)
        {
            this.coinNumWithPlayers[i] = PlayerPrefs.GetInt($"{SaveKey_CoinNumWithPlayer}{i}", 0);
        }
        this.coinNumForBigWheel = PlayerPrefs.GetInt(SaveKey_CoinNumForBigWheel, 100);
        this.bigWheelPercent = PlayerPrefs.GetInt(SaveKey_BigWheelPercent, 20);
        for (var i = 0; i < this.bigWheelWeights.Length; i++)
        {
            this.bigWheelWeights[i] = PlayerPrefs.GetInt($"{SaveKey_BigWheelWeight}{i}", 1);
        }
        this.bigWheelTotalWeight = this.bigWheelWeights.Sum();
        
        this.unitHPs.Clear();
        this.unitATTs.Clear();
        for (var i = 1; i < (int)ERealUnitType.MAX; i++)
        {
            var type = (ERealUnitType)i;
            this.unitHPs.Add(type, PlayerPrefs.GetInt(SaveKey_MonsterHP + type.ToString(), 2));
            this.unitATTs.Add(type, PlayerPrefs.GetInt(SaveKey_MonsterATT + type.ToString(), 10));
        }
        this.dropItemProbability = PlayerPrefs.GetInt(SaveKey_DropItemProbability, 5);
        for (var i = 0; i < this.dropItemWeights.Length; i++)
        {
            this.dropItemWeights[i] = PlayerPrefs.GetInt($"{SaveKey_DropItemWeight}{i + 1}", 1);
        }
        this.dropItemTotalWeight = this.dropItemWeights.Sum();
        
        this.itemConfigData.hpConfig.itemParam = PlayerPrefs.GetInt(SaveKey_HP_SUPPLY, this.itemConfigData.hpConfig.itemParam);
        if (this.itemConfigData.hpConfig.itemParam < 1)
            this.itemConfigData.hpConfig.itemParam = 1;
        this.itemConfigData.doubleConfig.itemParam = PlayerPrefs.GetInt(SaveKey_SCORE_DOUBLE, this.itemConfigData.doubleConfig.itemParam);
        if (this.itemConfigData.doubleConfig.itemParam < 1)
            this.itemConfigData.doubleConfig.itemParam = 1;
        this.itemConfigData.bulletConfig.itemParam = PlayerPrefs.GetInt(SaveKey_SHELL_GUN, this.itemConfigData.bulletConfig.itemParam);
        if (this.itemConfigData.bulletConfig.itemParam < 1)
            this.itemConfigData.bulletConfig.itemParam = 1;
        this.itemConfigData.rocketConfig.itemParam = PlayerPrefs.GetInt(SaveKey_BOLT_CANNON, this.itemConfigData.rocketConfig.itemParam);
        if (this.itemConfigData.rocketConfig.itemParam < 1)
            this.itemConfigData.rocketConfig.itemParam = 1;
        
        EventMgr.Instance.Emit(MachineDataMgr.DataUpdateEvent, null);
    }

    public bool CheckCoinNumEnoughForStartGame(int player)
    {
        if (player < 0 || player >= MachineDataMgr.Instance.PlayerShowCount)
            return false;
        
        if (this.coinNumForMachine < this.coinNumForOneGame)
            return false;

        this.coinNumForMachine -= this.coinNumForOneGame;
        PlayerPrefs.SetInt(SaveKey_CoinNumForMachine, this.coinNumForMachine);

        this.coinNumWithPlayers[player] += this.coinNumForOneGame;
        PlayerPrefs.SetInt($"{SaveKey_CoinNumWithPlayer}{player}", this.coinNumWithPlayers[player]);

        PlayerPrefs.Save();

        return true;
    }
    public void CoinNumForMachineAdd(int addNum)
    {
        this.coinNumForMachine += addNum;
        PlayerPrefs.SetInt(SaveKey_CoinNumForMachine, this.coinNumForMachine);

        PlayerPrefs.Save();
    }

    public bool CheckForBigWheel(int player)
    {
        if (this.coinNumWithPlayers[player] < this.coinNumForBigWheel)
            return false;

        if (Random.Range(0, 100) >= this.bigWheelPercent)
            return false;

        this.coinNumWithPlayers[player] -= this.coinNumForBigWheel;
        PlayerPrefs.SetInt($"{SaveKey_CoinNumWithPlayer}{player}", this.coinNumWithPlayers[player]);

        PlayerPrefs.Save();
        return true;
    }
    /// <summary>
    /// 根据大转盘礼物权重计算结果（0~7对应8个奖励）
    /// </summary>
    /// <returns></returns>
    public int GetBigWheelResult()
    {
        int randValue = Random.Range(1, this.bigWheelTotalWeight + 1);
        for (var i = 0; i < this.bigWheelWeights.Length; i++)
        {
            randValue -= this.bigWheelWeights[i];
            if (randValue <= 0)
                return i;
        }
        return this.bigWheelWeights.Length - 1;
    }
    /// <summary>
    /// 根据道具掉落权重计算掉落的道具
    /// </summary>
    /// <returns></returns>
    public EItemType GetDropItem()
    {
        int randWeight = Random.Range(1, this.dropItemTotalWeight + 1);
        for (var i = 0; i < this.dropItemWeights.Length; i++)
        {
            randWeight -= this.dropItemWeights[i];
            if (randWeight <= 0)
                return (EItemType)(i + 1);
        }
        return EItemType.HP_SUPPLY;
    }


    /// <summary>
    /// 获取当前音乐是否静音
    /// </summary>
    public bool IsMusicMute
    {
        get => SoundMgr.Instance.IsMusicMute();
    }
    /// <summary>
    /// 获取当前音效是否静音
    /// </summary>
    public bool IsSoundMute
    {
        get => SoundMgr.Instance.IsSoundMute();
    }
    /// <summary>
    /// 设置当前音乐静音
    /// </summary>
    /// <param name="mute"></param>
    public void SetMusicMute(bool mute)
    {
        SoundMgr.Instance.SetMusicMute(mute);
    }
    /// <summary>
    /// 设置当前音效静音
    /// </summary>
    /// <param name="mute"></param>
    public void SetSoundMute(bool mute)
    {
        SoundMgr.Instance.SetSoundMute(mute);
    }
    /// <summary>
    /// 获取音乐音量
    /// </summary>
    public float MusicVolume
    {
        get => SoundMgr.Instance.GetMusicVolume();
    }
    /// <summary>
    /// 获取音效音量
    /// </summary>
    public float SoundVolume
    {
        get => SoundMgr.Instance.GetSoundVolume();
    }
    /// <summary>
    /// 设置音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        SoundMgr.Instance.SetMusicVolume(volume);
    }
    /// <summary>
    /// 设置音效音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetSoundVolume(float volume)
    {
        SoundMgr.Instance.SetSoundVolume(volume);
    }

    public int HpSupply
    {
        get => this.itemConfigData.hpConfig.itemParam;
        set
        {
            this.itemConfigData.hpConfig.itemParam = value;
            PlayerPrefs.SetInt(SaveKey_HP_SUPPLY, value);
            PlayerPrefs.Save();
        }
    }
    public int ScoreDouble
    {
        get => this.itemConfigData.doubleConfig.itemParam;
        set
        {
            this.itemConfigData.doubleConfig.itemParam = value;
            PlayerPrefs.SetInt(SaveKey_SCORE_DOUBLE, value);
            PlayerPrefs.Save();
        }
    }
    public int ShellGun
    {
        get => this.itemConfigData.bulletConfig.itemParam;
        set
        {
            this.itemConfigData.bulletConfig.itemParam = value;
            PlayerPrefs.SetInt(SaveKey_SHELL_GUN, value);
            PlayerPrefs.Save();
        }
    }
    public int BoltCannon
    {
        get => this.itemConfigData.rocketConfig.itemParam;
        set
        {
            this.itemConfigData.rocketConfig.itemParam = value;
            PlayerPrefs.SetInt(SaveKey_BOLT_CANNON, value);
            PlayerPrefs.Save();
        }
    }
    
    // 清空所有存储的数据
    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
    }
    public bool SetDefaultData()
    {
        // ClearAllData();
        
        // 初始化玩家的设置和游戏数据
        if (PlayerPrefs.HasKey(SaveKey_CoinNumForMachine))
            return false;

        // 设置初始的游戏参数，包括硬币数量、每局游戏需要的硬币数、和玩家显示的计数
        PlayerPrefs.SetInt(SaveKey_CoinNumForMachine, 0);
        PlayerPrefs.SetInt(SaveKey_CoinNumForOneGame, 5);
        PlayerPrefs.SetInt(SaveKey_PlayerShowCount, 4);
        // 为每个关卡设置时间限制
        for (var i = 1; i < 11; i++)
        {
            for (var j = 1; j <= MaxLevelCount; j++)
            {
                PlayerPrefs.SetInt($"{SaveKey_LevelTime}{i}-{j}", 120);
            }
        }
        // 设置各关卡的目标怪物数量和目标分数
        for (var i = 1; i < 11; i++)
        {
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetMonsterCount}{i}-{1}", 50);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetScore}{i}-{1}", 1000);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetMonsterCount}{i}-{2}", 70);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetScore}{i}-{2}", 1500);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetMonsterCount}{i}-{3}", 100);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetScore}{i}-{3}", 2000);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetMonsterCount}{i}-{4}", 20);
            PlayerPrefs.SetInt($"{SaveKey_LevelTargetScore}{i}-{4}", 0);
        }

        // 设置怪物的生命值和攻击力
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.EYu.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.EYu.ToString(), 1);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.FeiLong.ToString(), 3);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.FeiLong.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.ZongXiong.ToString(), 4);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.ZongXiong.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.HaiDao.ToString(), 5);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.HaiDao.ToString(), 3);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.Boss.ToString(), 100);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.Boss.ToString(), 40);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.BossHitPoint.ToString(), 8);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.BossHitPoint.ToString(), 0);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.Monster100.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.Monster100.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterHP + ERealUnitType.Monster101.ToString(), 2);
        PlayerPrefs.SetInt(SaveKey_MonsterATT + ERealUnitType.Monster101.ToString(), 2);

        // 设置每个玩家的初始硬币数量
        for (var i = 0; i < GameApp.MAX_PLAYER_COUNT; i++)
            PlayerPrefs.SetInt($"{SaveKey_CoinNumWithPlayer}{i}", 0);

        // 设置大转盘的初始设置
        PlayerPrefs.SetInt(SaveKey_CoinNumForBigWheel, 100);
        PlayerPrefs.SetInt(SaveKey_BigWheelPercent, 30);
        this.bigWheelWeights = new int[BigWheelGiftCount];
        for (var i = 0; i < BigWheelGiftCount; i++)
            PlayerPrefs.SetInt($"{SaveKey_BigWheelWeight}{i}", 1);

        // 设置掉落物品的概率和权重
        PlayerPrefs.SetInt(SaveKey_DropItemProbability, 10);
        PlayerPrefs.SetInt($"{SaveKey_DropItemWeight}{(int)EItemType.HP_SUPPLY}", 10);
        PlayerPrefs.SetInt($"{SaveKey_DropItemWeight}{(int)EItemType.SCORE_DOUBLE}", 5);
        PlayerPrefs.SetInt($"{SaveKey_DropItemWeight}{(int)EItemType.SHELL_GUN}", 3);
        PlayerPrefs.SetInt($"{SaveKey_DropItemWeight}{(int)EItemType.BOLT_CANNON}", 1);

        PlayerPrefs.GetInt(SaveKey_HP_SUPPLY, 100);
        PlayerPrefs.GetInt(SaveKey_SCORE_DOUBLE, 15);
        PlayerPrefs.GetInt(SaveKey_SHELL_GUN, 20);
        PlayerPrefs.GetInt(SaveKey_BOLT_CANNON, 2);

        // 设置音乐和音效的相关参数
        SetMusicMute(false);
        SetSoundMute(false);
        SetMusicVolume(1f);
        SetSoundVolume(1f);

        PlayerPrefs.Save();

        ReloadData();

        return true;
    }
}

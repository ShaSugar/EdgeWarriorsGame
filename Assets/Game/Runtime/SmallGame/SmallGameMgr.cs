using UnityEngine;
/// <summary>
/// 小游戏管理 目前只有转盘
/// </summary>
public class SmallGameMgr : UnitySingleton<SmallGameMgr>
{
    SmallGame_BigWheel_UICtrl smallGameBigWheelUICtrl;
    public void Init()
    {
        this.smallGameBigWheelUICtrl = UIMgr.Instance.ShowUIView("GUIPrefabs/SmallGame_BigWheel") as SmallGame_BigWheel_UICtrl;
    }
    /// <summary>
    /// 检测是否有大转盘
    /// </summary>
    /// <param name="player"></param>
    /// <param name="unitId"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool CheckForBigWheel(int player, int unitId, Vector3 pos)
    {
        return this.smallGameBigWheelUICtrl.CheckForActiveBigWheel(player, unitId, pos);
    }
    /// <summary>
    /// 检测玩家是否在大转盘
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPlayerPlayingBigWheel(int player)
    {
        return this.smallGameBigWheelUICtrl.IsPlayerPlayingBigWheel(player);
    }
    /// <summary>
    /// 更新玩家显示数量
    /// </summary>
    public void UpdatePlayerShowCount()
    {
        this.smallGameBigWheelUICtrl.UpdatePlayerShowCount();
    }
}

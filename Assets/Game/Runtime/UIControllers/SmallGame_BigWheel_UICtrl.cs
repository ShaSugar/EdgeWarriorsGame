using UnityEngine;

public class SmallGame_BigWheel_UICtrl : UICtrl
{
    SmallGameBigWheel smallGameBigWheel;
    Transform[] playerTrans;
    Transform testTran;
    bool playingFlag;
    int playingPlayer;
    void Start()
    {
        this.playingPlayer = -1;
        this.playingFlag = false;
        Transform turnInfos = this.transform.Find("TurnInfos");
        this.playerTrans = new Transform[GameApp.MAX_PLAYER_COUNT];
        for (var i = 0; i < this.playerTrans.Length; i++)
        {
            this.playerTrans[i] = turnInfos.Find($"Player{i}Pos");
        }
        smallGameBigWheel = new SmallGameBigWheel(turnInfos.Find($"PlayerEffect").gameObject,
            () => { this.playingFlag = false; });
        
    }
    /// <summary>
    /// 更新玩家数量
    /// </summary>
    public void UpdatePlayerShowCount()
    {
        int playerShowCount = MachineDataMgr.Instance.PlayerShowCount;
        if (playerShowCount <= 1)
        {
            Vector3 pos = this.playerTrans[0].localPosition;
            pos.x = 0;
            this.playerTrans[0].localPosition = pos;
        }
        else if (playerShowCount <= 2)
        {
            Vector3 pos = this.playerTrans[0].localPosition;
            pos.x = -480;
            this.playerTrans[0].localPosition = pos;
            pos.x = 480;
            this.playerTrans[1].localPosition = pos;
        }
        else if (playerShowCount <= 3)
        {
            Vector3 pos = this.playerTrans[0].localPosition;
            pos.x = -480;
            this.playerTrans[0].localPosition = pos;
            pos.x = 0;
            this.playerTrans[1].localPosition = pos;
            pos.x = -480;
            this.playerTrans[2].localPosition = pos;
        }
        else
        {
            Vector3 pos = this.playerTrans[0].localPosition;
            pos.x = -480;
            this.playerTrans[0].localPosition = pos;
            pos.x = -230;
            this.playerTrans[1].localPosition = pos;
            pos.x = 230;
            this.playerTrans[2].localPosition = pos;
            pos.x = 480;
            this.playerTrans[3].localPosition = pos;
        }
    }
    /// <summary>
    /// 显示大转盘
    /// </summary>
    /// <param name="player"></param>
    /// <param name="result"></param>
    /// <param name="startPos"></param>
    void ShowBigWheel(int player, int result, Vector3 startPos)
    {
        if (this.smallGameBigWheel.IsPlaying)
            return;

        this.smallGameBigWheel.Show(player, result % 8, startPos, this.playerTrans[player].position);
    }
    /// <summary>
    /// 检测玩家是否在玩大转盘
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPlayerPlayingBigWheel(int player)
    {
        if (!this.playingFlag)
            return false;

        return (this.playingPlayer == player);
    }
    /// <summary>
    /// 检测是否激活大转盘
    /// </summary>
    /// <param name="player"></param>
    /// <param name="unitId"></param>
    /// <param name="unitPos"></param>
    /// <returns></returns>
    public bool CheckForActiveBigWheel(int player, int unitId, Vector3 unitPos)
    {
        if (this.playingFlag)
            return false;

        if (!MachineDataMgr.Instance.CheckForBigWheel(player))
            return false;

        this.playingPlayer = player;
        this.playingFlag = true;

        Vector3 startPos = CameraController.Instance.MainCamera.WorldToScreenPoint(unitPos);
        startPos.z = 0;
        ShowBigWheel(player, MachineDataMgr.Instance.GetBigWheelResult(), startPos);
        return true;
    }
}

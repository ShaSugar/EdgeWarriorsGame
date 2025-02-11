using UnityEngine;

public class PGL_AniEventCamera : MonoBehaviour
{

    // 怪物走出
    private void MonsterRunOut(int monsterID)
    {
        Debug.Log($"ActiveMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterRunOutEvent, monsterID);
    }
    // 怪物组走出
    private void MonsterRunOutGroup(int monsterID)
    {
        Debug.Log($"ActiveMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterRunOutGroupEvent, monsterID);
    }

    // 产怪
    private void SpawnMonster(int monsterID)
    {
        Debug.Log($"SpawnMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.SpawnMonsterEvent, monsterID);
    }
    // 激活怪物
    private void ActiveMonster(int monsterID)
    {
        Debug.Log($"ActiveMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.ActiveMonsterEvent, monsterID);
    }
    // 产怪组
    private void SpawnMonsterGroup(int groupID)
    {
        Debug.Log($"SpawnMonsterGroup: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.SpawnMonsterGroupEvent, groupID);
    }
    // 激活怪物组
    private void ActiveMonsterGroup(int groupID)
    {
        Debug.Log($"ActiveMonsterGroup: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.ActiveMonsterGroupEvent, groupID);
    }
    // 显示怪物
    private void ShowMonster(int monsterID)
    {
        Debug.Log($"ShowMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.ShowMonsterEvent, monsterID);
    }
    // 隐藏怪物
    private void HideMonster(int monsterID)
    {
        Debug.Log($"HideMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.HideMonsterEvent, monsterID);
    }
    // 显示怪物组
    private void ShowMonsterGroup(int groupID)
    {
        Debug.Log($"ShowMonsterGroup: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.ShowMonsterGroupEvent, groupID);
    }
    // 隐藏怪物组
    private void HideMonsterGroup(int groupID)
    {
        Debug.Log($"HideMonsterGroup: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.HideMonsterGroupEvent, groupID);
    }
    // 销毁怪物
    private void DestroyMonster(int monsterID)
    {
        Debug.Log($"DestroyMonster: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.DestroyMonsterEvent, monsterID);
    }
    // 销毁怪物组
    private void DestroyMonsterGroup(int groupID)
    {
        Debug.Log($"DestroyMonsterGroup: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.DestroyMonsterGroupEvent, groupID);
    }
    // 怪物爆炸
    private void MonsterBoom(int monsterID)
    {
        Debug.Log($"MonsterBoom: {monsterID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterBoomEvent, monsterID);
    }
    // 怪物组爆炸
    private void MonsterGroupBoom(int groupID)
    {
        Debug.Log($"MonsterGroupBoom: {groupID}");
        EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterGroupBoomEvent, groupID);
    }
    
    // 显示剧情提示
    private void ShowStoryTip(string content)
    {
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.ShowPlotTipsEvent, content);
    }
    
    // 开启射击
    private void OpenShoot(int flag)
    {
        Debug.Log("OpenShoot: " + flag);
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, flag == 1);
    }
    // 进入循环检测
    private void EnterLoopAniEvent(int loopId)
    {
        Debug.Log("EnterLoopAniEvent: " + loopId);
        EventMgr.Instance.Emit(PGL_Main.EnterLoopAniEvent, loopId);
    }

    // 进入下一轮
    private void GoToNext()
    {
        Debug.Log("GoToNext");
        EventMgr.Instance.Emit(PGL_Main.LevelFinishedEvent, null);
    }
}

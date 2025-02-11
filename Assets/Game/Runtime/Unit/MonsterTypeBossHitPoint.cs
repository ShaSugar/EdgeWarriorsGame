using DG.Tweening;
using UnityEngine;

public class MonsterTypeBossHitPoint : MonsterBase
{

    // 5秒后开始从黄色变成红色
    const int CHANGE_DELAY_TIME = 5;
    const string UI_EFFECT_PREFAB_PATH = "Effects/BossHitPointUIEffect";
    GameObject uiEffect;
    Transform uiEffectTran;
    SpriteRenderer uiEffectSpr1;
    SpriteRenderer uiEffectSpr2;
    Vector3 uiEffectPos;

    float bossHitTime;
    public override void Init(GameObject obj, int unitId)
    {
        base.Init(obj, unitId);

        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(UI_EFFECT_PREFAB_PATH);
        this.uiEffect = Object.Instantiate(nodePrefab);
        this.uiEffectTran = this.uiEffect.transform;
        this.uiEffectTran.SetParent(CameraController.Instance.BulletCameraTran);
        this.uiEffectTran.localPosition = Vector3.zero;
        this.uiEffectTran.localEulerAngles = Vector3.zero;
        this.uiEffectTran.localScale = Vector3.one;
        this.uiEffect.SetActive(false);
        this.uiEffectSpr1 = this.uiEffectTran.Find("Sprite1").GetComponent<SpriteRenderer>();
        this.uiEffectSpr2 = this.uiEffectTran.Find("Sprite2").GetComponent<SpriteRenderer>();

        Recycle();
    }
    public void StartMoveIn(Vector3 pos, float hitTime)
    {
        Recycle();
        this.RecycleFlag = false;

        this.bossHitTime = hitTime;
        this.unitCollider.enabled = true;
        this.lessHP = this.Config.health;
        this.tran.localPosition = pos;
        this.tran.localScale = Vector3.one;
        this.obj.SetActive(true);

        this.uiEffectSpr1.DOKill();
        this.uiEffectSpr2.DOKill();
        Color color = this.uiEffectSpr1.color;
        color.a = 0f;
        this.uiEffectSpr1.color = color;
        color.a = 1f;
        this.uiEffectSpr2.color = color;
        if (this.timerId != -1)
        {
            TimerMgr.Instance.UnSchedule(this.timerId);
            this.timerId = -1;
        }
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            float disTime = this.bossHitTime - CHANGE_DELAY_TIME;
            this.uiEffectSpr1.DOFade(1f, disTime);
            this.uiEffectSpr2.DOFade(0f, disTime).OnComplete(MoveOutQuickly);
        }, CHANGE_DELAY_TIME);

        this.uiEffectTran.DOKill();
        Vector3 screenPos = CameraController.Instance.MainCamera.WorldToScreenPoint(this.tran.position);
        screenPos.z = 500f;
        Vector3 effectPos = CameraController.Instance.BulletCamera.ScreenToWorldPoint(screenPos);
        this.uiEffectPos = effectPos;
        this.uiEffectTran.position = this.uiEffectPos;
        this.uiEffectTran.localScale = Vector3.zero;
        this.uiEffect.SetActive(true);
        this.uiEffect.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            this.unitCollider.enabled = true;
        });
    }

    public override void MoveOutQuickly()
    {
        if (this.lessHP > 0)
            UnitEffectMgr.Instance.ShowDieEffect(this.Config.deathEffect, this.tran.position, this.tran.parent);

        Recycle();
    }

    public override void HitByPlayer(int player, int hp, int weapon)
    {
        if (this.lessHP <= 0)
            return;

        this.lessHP -= hp;
        if (this.lessHP <= 0)
        {
            this.lessHP = 0;
            this.unitCollider.enabled = false;
            // EventMgr.Instance.Emit(UnitMgr.UnitPathOverEvent, this.Config.unitId);
            PlayDieAction(player, weapon);
        }
        else
        {
            this.uiEffectTran.DOKill();
            this.uiEffectTran.position = this.uiEffectPos;
            this.uiEffectTran.DOShakePosition(0.05f, 0.2f, 5).OnComplete(() =>
            {
                this.uiEffectTran.DOShakePosition(0.05f, 0.2f, 5).OnComplete(() =>
                {
                    this.uiEffectTran.DOShakePosition(0.05f, 0.2f, 5);
                });
            });
        }

        EventMgr.Instance.Emit(GameLevel.BossHitPointDeductHPEvent, new[]
        {
            player,
            hp
        });

    }

    public void PlayDieAction(int player, int weapon)
    {
        if (this.RecycleFlag)
        {
            Recycle();
            return;
        }

        if (!string.IsNullOrEmpty(this.Config.deathEffect))
            UnitEffectMgr.Instance.ShowDieEffect(this.Config.deathEffect, this.tran.position, this.tran.parent);
        if (!string.IsNullOrEmpty(this.Config.deathSound))
            SoundMgr.Instance.PlayOneShot(this.Config.deathSound);
        base.PlayVoice();

        int score = PlayerInfo.GetRealKillScore(this.Config.score, GameApp.Instance.IsPlayerInTimeDouble(player));
        int health = this.Config.health;
        int isBossHitPoint = this.Config.unitType == EUnitType.unitBossHitPoint ? 1 : 0;
        EventMgr.Instance.Emit(GameLevel.MonsterKilledEvent, new[]
        {
            player,
            unitId,
            score,
            health,
            isBossHitPoint
        });


        UIEffectMgr.Instance.ShowKillScoreEffect(player, score, GetKillScorePos());

        Recycle();
    }

    public override void Recycle()
    {
        base.Recycle();

        this.uiEffectSpr1?.DOKill();
        this.uiEffectSpr2?.DOKill();
        this.uiEffectTran?.DOKill();
        this.uiEffect?.SetActive(false);
    }

    public override void Destroy()
    {
        base.Destroy();
        GameObject.DestroyImmediate(this.uiEffect);
        this.uiEffect = null;
    }
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PGL_MonsterBase : MonoBehaviour
{
    enum PGL_MonsterState
    {
        enterAction,
        idle,
        run,
        attack,
        die,
        runOut
    }

    [SerializeField] private PGL_MonsterType _monsterType;
    [SerializeField] private Animation _anim;
    [SerializeField] private GameObject _attackTimeline;
    [SerializeField] private Transform _dieEffectTran;
    [SerializeField] private PGL_AniEventMonster _aniEventMonster;

    private int _groupID;
    private Transform _tran;
    private Collider _collider;

    private PGL_MonsterConfigData.PGL_MonsterConfig _config;

    private Tween _curTween;
    private int _lessHP;
    private int _timerId, _hittedTimerId;

    private PGL_MonsterState _monsterState;

    private Dictionary<string, float> _animClipsLength = new Dictionary<string, float>();

    private bool _isHitting, _isActive;

    private void Awake()
    {
        _tran = transform;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;


        foreach (AnimationState clip in _anim)
        {
            _animClipsLength.Add(clip.name, clip.length);
        }
    }

    public void Init(PGL_MonsterConfigData.PGL_MonsterConfig config, int groupID)
    {
        _groupID = groupID;
        _config = config;
        _lessHP = _config.health;
        this.gameObject.name = $"{groupID}_{config.monsterID}";
        _aniEventMonster?.UpdateMonsterConfig(_config);

        _tran.position = _config.enterStartPosList[0];
        if (_config.enterStartPosList.Length < 2)
        {
            _tran.LookAt(CameraController.Instance.MainCameraTran);
            _curTween = null;
        }
        else
        {
            _curTween = DoPathBySpeed(_config.enterStartPosList, _config.enterPathSpeed, _config.ease, _config.isLockAngleZ, EnterAttackState);
            _curTween.timeScale = .001f;
        }

        _collider.enabled = false;
        _isHitting = false;
        _isActive = false;
        if (string.IsNullOrEmpty(_config.enterAction))
        {
            ChangeMonsterState(PGL_MonsterState.idle);
            if (_config.autoActive)
                StartRun();
        }
        else
        {
            ChangeMonsterState(PGL_MonsterState.enterAction);
            _timerId = TimerMgr.Instance.ScheduleOnce((_) =>
                {
                    ChangeMonsterState(PGL_MonsterState.idle);
                    if (_config.autoActive)
                        StartRun();
                },
                _animClipsLength[_config.enterAction]);
        }
    }

    public void StartRun()
    {
        if (_isActive == true || _monsterState == PGL_MonsterState.die || _lessHP <= 0)
            return;

        TimerMgr.Instance.UnSchedule(_timerId);

        _isActive = true;
        _collider.enabled = true;
        if (_curTween != null)
        {
            ChangeMonsterState(PGL_MonsterState.run);
            _curTween.timeScale = 1;
        }
    }

    public void Show(bool flag)
    {
        if (_lessHP <= 0)
            return;

        this.gameObject.SetActive(flag);
    }

    public void HitByPlayer(int player, int hp, int weapon)
    {
        if (_lessHP <= 0)
            return;

        _lessHP -= hp;
        if (_lessHP > 0)
        {
            if (_monsterState is PGL_MonsterState.idle or PGL_MonsterState.run)
            {
                if (!string.IsNullOrEmpty(_config.hitAction) && _isHitting == false)
                {
                    _isHitting = true;
                    if (_curTween != null)
                        _curTween.timeScale = .001f;
                    _anim.Play(_config.hitAction);
                    _hittedTimerId = TimerMgr.Instance.ScheduleOnce((_) =>
                        {
                            if (_monsterState == PGL_MonsterState.idle)
                            {
                                ChangeMonsterState(PGL_MonsterState.idle);
                            }
                            else if (_monsterState == PGL_MonsterState.run)
                            {
                                _curTween.timeScale = 1f;
                                ChangeMonsterState(PGL_MonsterState.run);
                            }
                        },
                        _animClipsLength[_config.hitAction]);
                }
            }

            return;
        }

        StopAllTimer();

        _lessHP = 0;
        _collider.enabled = false;
        PlayDieAction(player, weapon);
    }

    private void PlayDieAction(int player, int weapon)
    {
        this._attackTimeline?.SetActive(false);

        ShowDieEffect();
        if (!string.IsNullOrEmpty(_config.dieSound))
            SoundMgr.Instance.PlayOneShot(_config.dieSound);
        PlayVoice();

        int score = PlayerInfo.GetRealKillScore(_config.score, GameApp.Instance.IsPlayerInTimeDouble(player));
        int health = _config.health;
        int isBossHitPoint = 0;
        EventMgr.Instance.Emit(GameLevel.MonsterKilledEvent, new[]
        {
            player,
            _config.monsterID,
            score,
            health,
            isBossHitPoint
        });
        UIEffectMgr.Instance.ShowKillScoreEffect(player, score, _tran.position + _tran.up * 5);

        // 掉落道具或触发大转盘
        if (!SmallGameMgr.Instance.CheckForBigWheel(player, _config.monsterID, _tran.position + _tran.up * 3) &&
            !SmallGameMgr.Instance.IsPlayerPlayingBigWheel(player) &&
            weapon == 0)
        {
            if (Random.Range(0, 100) < MachineDataMgr.Instance.GetDropItemProbability)
                GameApp.Instance.CheckProp(player, _config.monsterID, _tran.position + _tran.up * 3);
        }

        ChangeMonsterState(PGL_MonsterState.die);
        _timerId = TimerMgr.Instance.ScheduleOnce(
            (_) =>
            {
                gameObject.SetActive(false);
                EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterDieEvent, _config.monsterID);
            },
            _animClipsLength[_config.dieAction]);
    }

    private void ChangeMonsterState(PGL_MonsterState state)
    {
        _isHitting = false;
        _monsterState = state;
        TimerMgr.Instance.UnSchedule(_hittedTimerId);
        switch (_monsterState)
        {
            case PGL_MonsterState.enterAction:
                _anim.Play(_config.enterAction);
                break;
            case PGL_MonsterState.idle:
                _anim.Play(_config.idleAction);
                break;
            case PGL_MonsterState.run:
                _anim.Play(_config.runAction);
                break;
            case PGL_MonsterState.attack:
                _anim.Play(_config.attackAction);
                break;
            case PGL_MonsterState.die:
                _anim.Play(_config.dieAction);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void StopAllTimer()
    {
        TimerMgr.Instance.UnSchedule(_timerId);
        if (_curTween != null)
        {
            _curTween.Kill();
            _curTween = null;
        }

        _tran.DOKill();
    }

    // 进入攻击状态
    private void EnterAttackState()
    {
        StopAllTimer();

        if (_config.monsterType == PGL_MonsterType.boom)
        {
            Boom();
            return;
        }
        
        ChangeMonsterState(PGL_MonsterState.idle);
        _tran.LookAt(CameraController.Instance.MainCameraTran);

        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (_lessHP <= 0)
            return;

        StopAllTimer();

        float nextAttackTime =
            Random.Range(_config.minAttackTime, _config.maxAttackTime);
        if (GameApp.Instance.IsAnyPlayerHaveHp() && Random.Range(0, 100) < _config.attackPercent)
        {
            SoundMgr.Instance.PlayOneShot(_config.attackSound);
            ChangeMonsterState(PGL_MonsterState.attack);
            _attackTimeline?.SetActive(false);
            _attackTimeline?.SetActive(true);

            _timerId = TimerMgr.Instance.ScheduleOnce(
                (_) =>
                {
                    if (_monsterState != PGL_MonsterState.idle)
                        ChangeMonsterState(PGL_MonsterState.idle);

                    _timerId = TimerMgr.Instance.ScheduleOnce(
                        (_) => { AttackPlayer(); },
                        nextAttackTime);
                },
                _animClipsLength[_config.attackAction]);
        }
        else
        {
            if (_monsterState != PGL_MonsterState.idle)
                ChangeMonsterState(PGL_MonsterState.idle);
            _timerId = TimerMgr.Instance.ScheduleOnce(
                (_) => { AttackPlayer(); },
                nextAttackTime);
        }
    }

    private void OnDestroy()
    {
        StopAllTimer();

        _config = null;
    }

    private Tween DoPathBySpeed(Vector3[] pathPosArray, float speed, Ease ease, bool isLockAngleZ, Action completeCallback)
    {
        if (isLockAngleZ)
        {
            return _tran.DOPath(pathPosArray, speed, PathType.CatmullRom)
                .SetEase(ease)
                .SetOptions(false)
                .SetLookAt(0, true)
                .SetSpeedBased()
                .OnUpdate(() =>
                {
                    Vector3 angle = this._tran.localEulerAngles;
                    angle.z = 0;
                    this._tran.localEulerAngles = angle;
                })
                .OnComplete(() => { completeCallback?.Invoke(); });
        }
        else
        {
            return _tran.DOPath(pathPosArray, speed, PathType.CatmullRom)
                .SetEase(ease)
                .SetOptions(false)
                .SetLookAt(0, true)
                .SetSpeedBased()
                .OnComplete(() => { completeCallback?.Invoke(); });
        }
    }

    private bool PlayVoice()
    {
        if (_config.dieVoicePercent <= 0)
            return false;

        if (Random.Range(0, 100) > _config.dieVoicePercent)
            return false;

        if (MachineDataMgr.Instance.IsChineseLanguageVersion)
        {
            if (_config.dieVoiceList.Length <= 0)
                return false;

            int index = Random.Range(0, _config.dieVoiceList.Length);
            SoundMgr.Instance.PlaySound(_config.dieVoiceList[index]);

            return true;
        }
        else
        {
            if (_config.dieVoiceListEN.Length <= 0)
                return false;

            int index = Random.Range(0, _config.dieVoiceListEN.Length);
            SoundMgr.Instance.PlaySound(_config.dieVoiceListEN[index]);

            return true;
        }
    }

    private void ShowDieEffect()
    {
        if (string.IsNullOrEmpty(_config.dieEffectPrefab) || _dieEffectTran == null)
            return;

        _tran.LookAt(CameraController.Instance.MainCameraTran);
        Vector3 pos = _dieEffectTran.position;

        UnitEffectMgr.Instance.ShowDieEffect(_config.dieEffectPrefab, pos, _tran.parent);
    }

    public float Distance(Vector3 pos)
    {
        if (_collider == null || _lessHP <= 0)
            return float.MaxValue;

        Vector3 unitPos = CameraController.Instance.MainCamera.WorldToScreenPoint(_collider.bounds.center);
        unitPos.z = 0;
        return Vector3.Distance(unitPos, pos);
    }

    public void Boom()
    {
        if (_lessHP <= 0)
            return;

        StopAllTimer();

        _lessHP = 0;
        _collider.enabled = false;
        this._attackTimeline?.SetActive(false);

        ShowDieEffect();
        if (!string.IsNullOrEmpty(_config.bombSound))
            SoundMgr.Instance.PlayOneShot(_config.bombSound);

        EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, _config.attack);

        gameObject.SetActive(false);
        EventMgr.Instance.Emit(PGL_MonsterMgr.MonsterDieEvent, _config.monsterID);
    }

    public void RunOut()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGL_CameraPathsController : MonoBehaviour
{
    [SerializeField] private CameraPathAnimator[] _cameraPathAnimators;

    private CameraPathAnimator _currentAnimator;

    public void GameStart(Transform _mainCameraRootTran)
    {
        if (_cameraPathAnimators == null || _cameraPathAnimators.Length == 0)
        {
            Debug.LogError("没有配置任何摄像机路径动画呢！！！！");
        }
        else
        {
            for (var i = 0; i < _cameraPathAnimators.Length; i++)
            {
                _cameraPathAnimators[i].playOnStart = false;
                _cameraPathAnimators[i].animationObject = _mainCameraRootTran;
                _cameraPathAnimators[i].Stop();
            }
        }

        PlayAnimator(0);
    }

    private void PlayAnimator(int index)
    {
        if (_cameraPathAnimators == null || _cameraPathAnimators.Length <= index)
        {
            Debug.LogError("没有配置摄像机路径动画：" + index);
            return;
        }

        if (_currentAnimator != null)
        {
            _currentAnimator.Stop();
            _currentAnimator.AnimationStartedEvent -= OnAnimationStarted;
            _currentAnimator.AnimationPausedEvent -= OnAnimationPaused;
            _currentAnimator.AnimationStoppedEvent -= OnAnimationStopped;
            _currentAnimator.AnimationFinishedEvent -= OnAnimationFinished;
            _currentAnimator.AnimationLoopedEvent -= OnAnimationLooped;
            _currentAnimator.AnimationPingPongEvent -= OnAnimationPingPonged;
            _currentAnimator.AnimationCustomEvent -= OnCustomEvent;

            _currentAnimator.AnimationPointReachedEvent -= OnPointReached;
            _currentAnimator.AnimationPointReachedWithNumberEvent -= OnPointReachedByNumber;
        }

        _currentAnimator = _cameraPathAnimators[index];
        _currentAnimator.AnimationStartedEvent += OnAnimationStarted;
        _currentAnimator.AnimationPausedEvent += OnAnimationPaused;
        _currentAnimator.AnimationStoppedEvent += OnAnimationStopped;
        _currentAnimator.AnimationFinishedEvent += OnAnimationFinished;
        _currentAnimator.AnimationLoopedEvent += OnAnimationLooped;
        _currentAnimator.AnimationPingPongEvent += OnAnimationPingPonged;
        _currentAnimator.AnimationCustomEvent += OnCustomEvent;

        _currentAnimator.AnimationPointReachedEvent += OnPointReached;
        _currentAnimator.AnimationPointReachedWithNumberEvent += OnPointReachedByNumber;
        _currentAnimator.Play();
    }


    public void CPECallReceiveMethod()
    {
        Debug.Log($"CPECallReceive Method - Void");
    }

    public void CPECallReceiveMethodString(string msg)
    {
        Debug.Log($"CPECallReceive Method - {msg}");
    }

    public void CPECallReceiveMethodFloat(float val)
    {
        Debug.Log($"CPECallReceive Method - {val}");
    }

    public void CPECallReceiveMethodInt(int val)
    {
        Debug.Log($"CPECallReceive Method - {val}");
        
        PlayAnimator(val);
    }


    private void OnCustomEvent(string eventname)
    {
        Debug.Log("Custom Camera Path event: " + eventname);
        // if (eventname.Equals("PauseCameraPath"))
        // {
        //     _currentAnimator?.Pause();
        // }
        // else if (eventname.Equals("StopCameraPath"))
        // {
        //     _currentAnimator?.Stop();
        // }
        // else if (eventname.Equals("PlayCameraPath"))
        // {
        //     _currentAnimator?.Play();
        // }
    }

    private void OnAnimationStarted()
    {
        Debug.Log("The animation has begun");
    }

    private void OnAnimationPaused()
    {
        Debug.Log("The animation has been paused");
    }

    private void OnAnimationStopped()
    {
        Debug.Log("The animation has been stopped");
    }

    private void OnAnimationFinished()
    {
        Debug.Log("The animation has finished");
    }

    private void OnAnimationLooped()
    {
        Debug.Log("The animation has looped back to the start");
    }

    private void OnAnimationPingPonged()
    {
        Debug.Log("The animation has ping ponged into the other direction");
    }

    private void OnPointReached()
    {
        Debug.Log("A point was reached");
    }

    private void OnPointReachedByNumber(int pointNumber)
    {
        Debug.Log("The point " + pointNumber + " was reached");
    }
}
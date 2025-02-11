using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 序列帧动画组件
/// </summary>
[RequireComponent(typeof(Image))]
public class UGUISpriteAnimation : MonoBehaviour
{
    private Image ImageSource;
    private int mCurFrame;
    private float mDelta;
    public float FPS = 5;
    public List<Sprite> SpriteFrames;
    public bool IsPlaying;
    public bool Forward = true;
    public bool AutoPlay;
    public bool Loop;
    public int FrameCount
    {
        get => SpriteFrames.Count;
    }

    void Awake()
    {
        ImageSource = GetComponent<Image>();
    }

    void Start()
    {
        if (AutoPlay)
        {
            Play();
        }
        else
        {
            IsPlaying = false;
        }
    }

    private void SetSprite(int idx)
    {
        ImageSource.sprite = SpriteFrames[idx];
        //该部分为设置成原始图片大小，如果只需要显示Image设定好的图片大小，注释掉该行即可。
        ImageSource.SetNativeSize();
    }

    public void Play()
    {
        IsPlaying = true;
        Forward = true;
    }

    public void PlayReverse()
    {
        IsPlaying = true;
        Forward = false;
    }

    void Update()
    {
        if (!IsPlaying || 0 == FrameCount)
        {
            return;
        }
        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Forward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }
            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    IsPlaying = false;
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    IsPlaying = false;
                    return;
                }
            }
            SetSprite(mCurFrame);
        }
    }

    public void Pause()
    {
        IsPlaying = false;
    }

    public void Resume()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
        }
    }

    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        IsPlaying = false;
    }

    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }
}

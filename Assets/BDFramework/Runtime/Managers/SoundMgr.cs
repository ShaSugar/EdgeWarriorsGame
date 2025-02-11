using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundMgr : UnitySingleton<SoundMgr>
{
    const int MAX_SOUNDS = 128;
    const string MusicMuteKey = "isMusicMute";
    const string SoundMuteKey = "isSoundMute";
    const string MusicVolumeKey = "MusicVolume";
    const string SoundVolumeKey = "SoundVolume";

    private AudioSource bossSoundSource;

    List<AudioSource> sounds;
    int curIndex;
    AudioSource musicSource;

    int isMusicMute;
    int isSoundMute;
    
    string curMusicName;
    
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public IEnumerator Preload()
    {
        for (var i = 0; i < GameLevelMgr.Instance.gameLevelConfigData.data.Count; i++)
        {
            GameLevelConfigData.GameLevelConfig levelConfig = GameLevelMgr.Instance.gameLevelConfigData.data[i];
            if (levelConfig.scene != GameSceneMgr.Instance.CurScene)
                continue;
            
            if(string.IsNullOrEmpty(levelConfig.bgmPath))
                continue;
            
            ResourceRequest request = ResMgr.Instance.LoadAssetASync<AudioClip>(levelConfig.bgmPath);
            yield return request;
            var clip = request.asset as AudioClip;
            if (clip == null)
                continue;
            
            _audioClips.Add(levelConfig.bgmPath, clip);
        }
        yield return null;
    }

    private AudioClip GetAudioClip(string path)
    {
        if (_audioClips.ContainsKey(path))
            return _audioClips[path];
        
        
        var clip = ResMgr.Instance.LoadAssetSync<AudioClip>(path);
        if (clip == null)
            return null;
        
        _audioClips.Add(path, clip);
        return clip;
    }

    public void PreloadClear()
    {
        _audioClips.Clear();
    }
    
    public void Init()
    {
        bossSoundSource = this.gameObject.AddComponent<AudioSource>();
        
        this.sounds = new List<AudioSource>();
        for (var i = 0; i < MAX_SOUNDS; i++)
        {
            var audioSource = this.gameObject.AddComponent<AudioSource>();
            this.sounds.Add(audioSource);
        }

        this.musicSource = this.gameObject.AddComponent<AudioSource>();
        this.curIndex = 0;

        this.isMusicMute = PlayerPrefs.GetInt(MusicMuteKey, 0);
        this.isSoundMute = PlayerPrefs.GetInt(SoundMuteKey, 0);

        this.curMusicName = string.Empty;

        this.SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f));
        this.SetSoundVolume(PlayerPrefs.GetFloat(SoundVolumeKey, 1.0f));
    }

    
    public void PlayBossSound(string soundName)
    {
        if (this.isSoundMute != 0)
            return;
        
        var clip = GetAudioClip(soundName);
        if (clip == null)
            return;
        
        bossSoundSource.clip = clip;
        bossSoundSource.loop = false;
        bossSoundSource.pitch = 1;
        bossSoundSource.Play();
    }
    public void SetBossSoundSpeed(float speed)
    {
        bossSoundSource.pitch = speed;
    }
    public void StopBossSound()
    {
        bossSoundSource.Stop();
    }
    
    public void PlayMusic(string musicName, bool loop = true)
    {
        if (this.curMusicName == musicName)
            return;

        this.curMusicName = musicName;
        
        // var clip = ResMgr.Instance.LoadAssetSync<AudioClip>(musicName);
        var clip = GetAudioClip(musicName);
        if (clip == null)
            return;

        this.musicSource.clip = clip;
        this.musicSource.loop = loop;

        if (this.isMusicMute != 0)
            return;

        this.musicSource.Play();
    }
    public void StopMusic()
    {
        this.musicSource.Stop();
    }
    public void PlaySound(string soundName, bool loop = false)
    {
        if (this.isSoundMute != 0)
            return;

        // var clip = ResMgr.Instance.LoadAssetSync<AudioClip>(soundName);
        var clip = GetAudioClip(soundName);
        if (clip == null)
            return;

        int soundId = this.curIndex;
        // int soundId = GetSoundId(this.curIndex);
        AudioSource audioSource = this.sounds[soundId];
        this.curIndex = soundId + 1;
        this.curIndex = (this.curIndex >= this.sounds.Count) ? 0 : this.curIndex;

        audioSource.clip = clip;
        audioSource.loop = loop;

        if (this.isSoundMute != 0)
            return;

        // audioSource.pitch = 1;
        audioSource.Play();
    }
    public void PlayOneShot(string soundName, bool loop = false)
    {
        if (this.isSoundMute != 0)
            return;

        // var clip = ResMgr.Instance.LoadAssetSync<AudioClip>(soundName);
        var clip = GetAudioClip(soundName);
        if (clip == null)
            return;

        int soundId = this.curIndex;
        // int soundId = GetSoundId(this.curIndex);
        AudioSource audioSource = this.sounds[soundId];
        this.curIndex = soundId + 1;
        this.curIndex = (this.curIndex >= this.sounds.Count) ? 0 : this.curIndex;

        audioSource.clip = clip;
        audioSource.loop = loop;

        if (this.isSoundMute != 0)
            return;

        // audioSource.pitch = 1;
        audioSource.PlayOneShot(clip);
    }
    public void StopAllSound()
    {
        for (var i = 0; i < this.sounds.Count; i++)
        {
            this.sounds[i].Stop();
        }
        
        bossSoundSource.Stop();
    }

    // int GetSoundId(int curIndex)
    // {
    //     for (int i = curIndex; i < this.sounds.Count; i++)
    //     {
    //         if (!this.sounds[i].isPlaying)
    //             return i;
    //     }
    //     for (var i = 0; i < curIndex; i++)
    //     {
    //         if (!this.sounds[i].isPlaying)
    //             return i;
    //     }
    //     
    //     return curIndex;
    // }

    public bool IsMusicMute()
    {
        return PlayerPrefs.GetInt(MusicMuteKey, 0) != 0;
    }
    public bool IsSoundMute()
    {
        return PlayerPrefs.GetInt(SoundMuteKey, 0) != 0;
    }
    public float GetMusicVolume()    { return PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f); }
    public float GetSoundVolume()    { return PlayerPrefs.GetFloat(SoundVolumeKey, 1.0f); }
    public void SetMusicMute(bool mute)
    {
        if (mute)
        {
            StopMusic();
        }
        else
        {
            PlayMusic(this.curMusicName);
        }
        this.isMusicMute = mute ? 1 : 0;
        PlayerPrefs.SetInt(MusicMuteKey, this.isMusicMute);
        PlayerPrefs.Save();
    }
    public void SetSoundMute(bool mute)
    {
        if (mute)
        {
            StopAllSound();
        }
        this.isSoundMute = mute ? 1 : 0;
        PlayerPrefs.SetInt(SoundMuteKey, this.isSoundMute);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float per)
    {
        per = Mathf.Clamp(per, 0, 1);
        this.musicSource.volume = per;

        PlayerPrefs.SetFloat(MusicVolumeKey, per);
        PlayerPrefs.Save();
    }
    public void SetSoundVolume(float per)
    {
        per = Mathf.Clamp(per, 0, 1);

        for (var i = 0; i < this.sounds.Count; i++)
        {
            this.sounds[i].volume = per;
        }

        PlayerPrefs.SetFloat(SoundVolumeKey, per);
        PlayerPrefs.Save();
    }
}

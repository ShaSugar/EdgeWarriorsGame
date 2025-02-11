using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : UnitySingleton<SceneMgr>
{
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public void Init()
    {
            
    }

        
    public void EnterScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator EnterSceneAsync(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
    
    public IEnumerator EnterSceneAsync(string sceneName, Action<float> processCallback)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        if (op == null)
        {
            Debug.LogError($"加载场景失败{sceneName}");
            yield break;
        }
        
        // 限制场景加载程度，不允许立马切换场景，op。progress也只会到达/停在0.899f处
        // op.allowSceneActivation = false;
        var lastProgress = 0f;
        
        // 根据场景加载程度，设置显示进度条，使进度条慢慢加载
        while (op.progress < 1f)
        {
            if (!lastProgress.Equals(op.progress))
            {
                lastProgress = op.progress;
                processCallback?.Invoke(lastProgress);
            }
            yield return waitForEndOfFrame;
        }
        
        processCallback?.Invoke(1f);

        // yield return new WaitForSeconds(5);
    }

}

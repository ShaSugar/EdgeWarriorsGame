using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEvent : MonoBehaviour
{
    public float delayHide;

    // Start is called before the first frame update
    void Start()
    {
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, GameLevelStartEvent);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveListener(GameLevel.GameLevelStartEvent, GameLevelStartEvent);
    }

    private void GameLevelStartEvent(string arg1, object arg2)
    {
        int[] a = (int[])arg2;
        if (a[0] == 1)
        {
            gameObject.SetActive(true);
            Invoke("Hide", delayHide);

        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

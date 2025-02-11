using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public GameObject ViewNode(string viewName)
    {
        Transform tf = this.transform.Find(viewName);
        return tf.gameObject;
    }

    public T View<T>(string viewName) where T : Component
    {
        Transform tf = this.transform.Find(viewName);
        return tf.GetComponent<T>();
    }

    public void AddButtonListener(string viewName,
        UnityAction onClick)
    {
        var bt = this.View<Button>(viewName);
        if (bt == null)
        {
            Debug.LogWarning("UI_manager add_button_listener:not Button Component!");
            return;
        }

        bt.onClick.AddListener(onClick);
    }

    public void AddEventTriggerPointDown(string viewName,
        UnityAction<BaseEventData> callback)
    {
        var trigger = this.View<EventTrigger>(viewName);

        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown,
            callback = new EventTrigger.TriggerEvent()
        };

        trigger.triggers.Add(entry);
        entry.callback.AddListener(callback);
    }

    public void AddEventTriggerDrag(string viewName,
        UnityAction<BaseEventData> callback)
    {
        var trigger = this.View<EventTrigger>(viewName);


        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Drag,
            callback = new EventTrigger.TriggerEvent()
        };

        trigger.triggers.Add(entry);
        entry.callback.AddListener((callback));
    }


    public void AddEventTriggerEndDrag(string viewName,
        UnityAction<BaseEventData> callback)
    {
        var trigger = this.View<EventTrigger>(viewName);


        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.EndDrag,
            callback = new EventTrigger.TriggerEvent()
        };

        trigger.triggers.Add(entry);
        entry.callback.AddListener((callback));
    }

    public void AddEventTriggerBeginDrag(string viewName,
        UnityAction<BaseEventData> callback)
    {
        var trigger = this.View<EventTrigger>(viewName);


        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.BeginDrag,
            callback = new EventTrigger.TriggerEvent()
        };

        trigger.triggers.Add(entry);
        entry.callback.AddListener((callback));
    }

    public void AddEventTriggerPointClick(string viewName,
        UnityAction<BaseEventData> callback)
    {
        var trigger = this.View<EventTrigger>(viewName);


        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick,
            callback = new EventTrigger.TriggerEvent()
        };

        trigger.triggers.Add(entry);
        entry.callback.AddListener((callback));
    }
}

public class UIMgr : UnitySingleton<UIMgr>
{
    public GameObject canvas;

    SpriteAtlas playerInfoAtlas;
    Dictionary<string, Sprite> cacheAtlasSprites = new Dictionary<string, Sprite>();
    public Sprite GetPlayerInfoSprite(string spriteName)
    {
        if (this.cacheAtlasSprites.TryGetValue(spriteName, out Sprite sprite))
            return sprite;

        Debug.LogError($"Textures/PlayerInfo 找不到 Sprite：{spriteName}");
        return null;

    }

    public void Init()
    {
        this.playerInfoAtlas = ResMgr.Instance.LoadAssetSync<SpriteAtlas>("Textures/PlayerInfo");
        if (this.playerInfoAtlas != null && this.playerInfoAtlas.spriteCount > 0)
        {
            var sps = new Sprite[this.playerInfoAtlas.spriteCount];
            this.playerInfoAtlas.GetSprites(sps);
            for (var i = 0; i < sps.Length; i++)
            {
                this.cacheAtlasSprites.Add(sps[i].name.Replace("(Clone)", ""), sps[i]);
            }
        }
        else
        {
            Debug.LogError("Textures/PlayerInfo 不存在任何 Sprite");
        }

        this.canvas = GameObject.Find("Canvas");
        if (this.canvas == null)
        {
            Debug.LogWarning("UI manager load  Canvas failed!!!!");
        }
        else
        {
            DontDestroyOnLoad(this.canvas);
        }

        GameObject ev = GameObject.Find("EventSystem");
        if (ev)
        {
            DontDestroyOnLoad(ev);
        }
    }

    public UICtrl ShowUIWindow(string uiWindowPath)
    {
        var uiPrefab = ResMgr.Instance.LoadAssetSync<GameObject>(uiWindowPath);
        GameObject uiView = GameObject.Instantiate(uiPrefab);
        uiView.name = uiPrefab.name;

        var type = Type.GetType($"{uiPrefab.name}_UICtrl");
        var ctrl = (UICtrl)uiView.AddComponent(type);

        return ctrl;
    }

    public void RemoveUIWindow(string uiWindowPath)
    {
        int lastIndex = uiWindowPath.LastIndexOf("/", StringComparison.Ordinal);
        if (lastIndex > 0)
        {
            uiWindowPath = uiWindowPath.Substring(lastIndex + 1);
        }

        GameObject uiWindow = GameObject.Find(uiWindowPath);
        GameObject.DestroyImmediate(uiWindow);
    }

    public UICtrl ShowUIView(string viewPath, GameObject parent = null)
    {
        if (parent == null)
        {
            parent = this.canvas;
        }
        var uiPrefab = ResMgr.Instance.LoadAssetSync<GameObject>(viewPath);
        GameObject uiView = GameObject.Instantiate(uiPrefab, parent.transform, false);
        uiView.name = uiPrefab.name;

        var type = Type.GetType(uiPrefab.name + "_UICtrl");
        var ctrl = (UICtrl)uiView.AddComponent(type);

        return ctrl;
    }

    public void RemoveUIView(string viewPath)
    {
        int lastIndex = viewPath.LastIndexOf("/", StringComparison.Ordinal);
        if (lastIndex > 0)
        {
            viewPath = viewPath.Substring(lastIndex + 1);
        }

        Transform view = this.canvas.transform.Find(viewPath);
        if (view)
        {
            GameObject.DestroyImmediate(view.gameObject);
        }
    }

    public void RemoveAllViews()
    {
        UnityUtils.DestroyAllChildren(this.canvas.transform);
    }
}

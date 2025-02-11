using System;
using System.Collections.Generic;


public class EventMgr : UnitySingleton<EventMgr>
{
    Dictionary<string, Action<string, object>> eventActions;
    public void Init()
    {
        this.eventActions = new Dictionary<string, Action<string, object>>();
    }

    public void AddListener(string eventName, Action<string, object> onEvent)
    {
        if (this.eventActions.TryAdd(eventName, onEvent))
            return;

        this.eventActions[eventName] += onEvent;
    }
    public void RemoveListener(string eventName, Action<string, object> onEvent)
    {
        if (this.eventActions == null || !this.eventActions.ContainsKey(eventName))
            return;

        this.eventActions[eventName] -= onEvent;
    }

    public void Emit(string eventName, object data)
    {
        if (!this.eventActions.TryGetValue(eventName, out Action<string, object> action))
            return;

        action?.Invoke(eventName, data);
    }
}

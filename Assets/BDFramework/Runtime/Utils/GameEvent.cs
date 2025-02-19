using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(object data)
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(data);
    }
    public void Raise()
    {
        Raise(null);
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

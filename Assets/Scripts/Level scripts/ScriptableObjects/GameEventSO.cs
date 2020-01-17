using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName ="Scriptable objects/Game Event")]
public class GameEventSO : ScriptableObject
{
    private List<EventListener> listeners = new List<EventListener>();

    public void Subscribe(EventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void Unsubscribe(EventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    public void Raise()
    {
        for (int i = listeners.Count - 1; i > -1; i--)
        {
            listeners[i].OnEventRaised();
        }
    }
}

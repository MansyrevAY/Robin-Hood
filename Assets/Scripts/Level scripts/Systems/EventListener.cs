using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public UnityEvent responce;
    public GameEventSO gameEvent;

    private void OnEnable()
    {
        gameEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        gameEvent.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        responce.Invoke();
    }
}

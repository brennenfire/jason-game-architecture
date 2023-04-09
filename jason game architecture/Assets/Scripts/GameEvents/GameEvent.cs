using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]

public class GameEvent : ScriptableObject
{
    HashSet<GameEventListener> gameEventListeners = new HashSet<GameEventListener>();

    public void Register(GameEventListener gameEventListener) 
    { 
        gameEventListeners.Add(gameEventListener);
    }
    public void Deregister(GameEventListener gameEventListener)
    {
        gameEventListeners.Remove(gameEventListener);
    }


    [ContextMenu("Invoke")]
    public void Invoke()
    {
        foreach(var gameEventListener in gameEventListeners) 
        {
            gameEventListener.RaiseEvent();
        }
    }
}

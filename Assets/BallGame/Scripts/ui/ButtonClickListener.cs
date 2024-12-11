using System;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClickListener : MonoBehaviour
{
    [SerializeField] GameEventListenere[] _responce;
    // Start is called before the first frame update
 

    private void OnEnable()
    {
        foreach (GameEventListenere e in _responce)
        {
            e.listenerEvent.AddListener(e.responceEvent.Invoke);
        }
    }

    private void OnDisable()
    {
        foreach (GameEventListenere e in _responce)
        {
            e.listenerEvent.RemoveListener(e.responceEvent.Invoke);
        }
    }

}

[Serializable]
public class GameEventListenere
{
    public string buttonName;
    public GameEvent listenerEvent;
    public UnityEvent responceEvent;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name + " (Singleton)");
                    instance = gameObject.AddComponent<T>();
                    DontDestroyOnLoad(gameObject); // Make persistent across scenes
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject); // Ensure persistence
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void OnApplicationQuit()
    {
        // Cleanup instance on application exit
        instance = null;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // Clean up the static reference
        }
    }
}

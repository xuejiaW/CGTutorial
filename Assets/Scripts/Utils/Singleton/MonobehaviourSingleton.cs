using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonobehaviorSingleton<T> : MonoBehaviour where T : MonobehaviorSingleton<T>
{
    private static object locker = new object();
    protected static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                lock (locker)
                {
                    if (_Instance == null)
                    {
                        _Instance = FindObjectOfType<T>();
                        if (_Instance != null && !_Instance.InitDone)
                            _Instance.Init();
                    }
                }
            }
            return _Instance;
        }
    }

    protected bool InitDone = false;
    protected virtual void Init()
    {
        InitDone = true;
    }

    protected virtual void Start()
    {
        if (!InitDone)
            Init();
    }
}

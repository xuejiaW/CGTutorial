using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static object locker = new object();
    private static T _Instance = null;
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
                        _Instance = new T();
                        _Instance.Init();
                        Debug.Log(_Instance.GetType() + "has created");
                    }
                }
            }
            return _Instance;
        }
    }

    protected virtual void Init() { }
}

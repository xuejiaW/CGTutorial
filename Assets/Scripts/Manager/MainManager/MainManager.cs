using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonobehaviorSingleton<MainManager>, IMainUpdateSubject
{
    private List<IMainUpdateObserver> updateObserversList = null;
    public Camera viewCamera { get; set; }
    public Camera worldCamera { get; set; }

    protected override void Init()
    {
        base.Init();
        updateObserversList = new List<IMainUpdateObserver>();
        viewCamera = GameObject.Find("CameraGroup/View").GetComponent<Camera>();
        worldCamera = GameObject.Find("CameraGroup/World").GetComponent<Camera>();
    }

    public void RegisterObserver(IMainUpdateObserver observer)
    {
        updateObserversList.Add(observer);
    }

    public void UnregisterObserver(IMainUpdateObserver observer)
    {
        updateObserversList.Remove(observer);
    }

    protected override void Start()
    {
        base.Start();
        // Inorder to create interactiveManager
        InteractiveManager.Instance.GetType();
    }

    public void Update()
    {
        for (int i = 0; i != updateObserversList.Count; ++i)
        {
            updateObserversList[i].Update();
        }
    }
}

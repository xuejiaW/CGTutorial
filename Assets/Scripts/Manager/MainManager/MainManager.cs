using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonobehaviorSingleton<MainManager>, IMainUpdateSubject
{
    private List<IMainUpdateObserver> updateObserversList = null;
    public Camera viewCamera { get; set; }

    protected override void Init()
    {
        base.Init();
        updateObserversList = new List<IMainUpdateObserver>();
        viewCamera = Camera.main;
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
        InteractiveManager.Instance.Init();
    }

    public void Update()
    {
        updateObserversList.ForEach(observer => observer.Update());
    }
}

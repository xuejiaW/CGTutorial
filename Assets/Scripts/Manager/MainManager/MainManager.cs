using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonobehaviorSingleton<MainManager>, IMainUpdateSubject
{
    private List<IMainUpdateObserver> updateObserversList = null;
    public GameObject cubePrefab = null;
    public Camera viewCamera = null;

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
        GameResourceManager.Instance.InstantiateInteractive(cubePrefab);
        InteractiveManager.Instance.SetTransformState(InteractiveState.MOVING);//Default transform state should be moving.
    }

    public void Update()
    {
        updateObserversList.ForEach(observer => observer.Update());
    }
}

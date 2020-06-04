using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonobehaviorSingleton<GameManager>
{
    private List<IUpdateObserver> updateObserversList = null;
    public GameObject cubePrefab = null;

    protected override void Init()
    {
        base.Init();
        updateObserversList = new List<IUpdateObserver>();
    }

    public void RegisterObserver(IUpdateObserver observer)
    {
        updateObserversList.Add(observer);
    }

    public void UnRegisterObserver(IUpdateObserver observer)
    {
        updateObserversList.Remove(observer);
    }

    protected override void Start()
    {
        base.Start();
        GameResourceManager.Instance.InstantiateInteractive(cubePrefab);
        InteractiveManager.Instance.SetTransformState(InteractiveState.MOVING);//Default transform state should be moving.
    }

    private void Update()
    {
        updateObserversList.ForEach(observer => observer.Update());
    }
}

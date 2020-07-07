using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUIManager : MonobehaviorSingleton<ComponentUIManager>
{
    private TransformView transformView = null;
    protected override void Init()
    {
        base.Init();
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;

        transformView = GetComponentInChildren<TransformView>();
        transformView.gameObject.SetActive(false);
    }
    private void OnSelectedGoUpdated(InteractiveGameObjectView oldGbj, InteractiveGameObjectView newGbj)
    {
        Debug.Log("old game obj" + oldGbj?.gameObject?.name);
        Debug.Log("new game obj" + newGbj?.gameObject?.name);
        transformView.gameObject.SetActive(newGbj);
        //TODO: adapt
        // transformView.targetGameObjectView = newGbj;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<DisplayableEntityModel> interactiveGo = null;
    public DisplayableEntityModel holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<DisplayableEntityModel, DisplayableEntityModel> OnHoldingInteractiveGOUpdated = null;
    public event System.Action<DisplayableEntityModel> OnCreateInteractiveGo = null;

    protected override void InitProcess()
    {
        base.InitProcess();
        interactiveGo = new List<DisplayableEntityModel>();
        holdingInteractiveGo = null;
    }

    public void AddInteractiveGo(DisplayableEntityModel model)
    {
        interactiveGo.Add(model);
        OnCreateInteractiveGo?.Invoke(model);
        Debug.Log("interactiveGO name is " + interactiveGo.Count);
    }

    public void OnSelectGameObject(DisplayableEntityModel GO)
    {
        if (holdingInteractiveGo == GO)
            return;
        DisplayableEntityModel old = holdingInteractiveGo;
        holdingInteractiveGo = GO;
        OnHoldingInteractiveGOUpdated?.Invoke(old, holdingInteractiveGo);
    }

    public void OnSelectGameObject(GameObject GO)
    {
        Debug.Log("On Select GO");
        OnSelectGameObject(GO?.GetComponent<InteractiveGameObjectView>().model);
    }
}

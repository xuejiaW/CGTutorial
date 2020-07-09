using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<DisplayableEntityModel> interactiveGo = null;
    public DisplayableEntityModel holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<DisplayableEntityModel, DisplayableEntityModel> OnHoldingInteractiveGOUpdated = null;
    public event System.Action<DisplayableEntityModel> OnCreateInteractiveGo = null;

    public override void Init()
    {
        base.Init();
        interactiveGo = new List<DisplayableEntityModel>();
    }

    public void AddInteractiveGo(DisplayableEntityModel model)
    {
        interactiveGo.Add(model);
        OnCreateInteractiveGo?.Invoke(model);
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
        OnSelectGameObject(GO?.GetComponent<InteractiveGameObjectView>().model);
    }
}

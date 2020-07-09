using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<DisplayableEntityModel> interactiveGo = null;
    public DisplayableEntityModel holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<DisplayableEntityModel, DisplayableEntityModel> OnHoldingInteractiveGOUpdated = null;
    public event System.Action<DisplayableEntityModel> OnCreateNewInteractiveGo = null;

    public override void Init()
    {
        base.Init();
        interactiveGo = new List<DisplayableEntityModel>();
    }

    public void AddInteractiveGo(DisplayableEntityModel model)
    {
        interactiveGo.Add(model);
        OnCreateNewInteractiveGo?.Invoke(model);
    }

    public void OnClickGameObject(GameObject GO)
    {
        Debug.Log("On click gameobject" + GO?.name);
        DisplayableEntityModel result = GO?.GetComponent<InteractiveGameObjectView>().model;
        OnHoldingInteractiveGOUpdated?.Invoke(holdingInteractiveGo, result);
        holdingInteractiveGo = result;
    }
}

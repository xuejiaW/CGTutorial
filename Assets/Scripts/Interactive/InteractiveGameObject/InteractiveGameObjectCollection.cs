using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<InteractiveGameObjectView> interactiveGo = null;
    public InteractiveGameObjectView holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<InteractiveGameObjectView, InteractiveGameObjectView> OnHoldingInteractiveGOUpdated = null;

    public override void Init()
    {
        base.Init();

        interactiveGo = new List<InteractiveGameObjectView>();
    }

    public void AddInteractiveGo(InteractiveGameObjectView view)
    {
        interactiveGo.Add(view);
    }

    public void OnClickGameObject(GameObject GO)
    {
        Debug.Log("On click gameobject" + GO?.name);
        InteractiveGameObjectView result = GO?.GetComponent<InteractiveGameObjectView>();
        OnHoldingInteractiveGOUpdated?.Invoke(holdingInteractiveGo, result);
        holdingInteractiveGo = result;
    }
}

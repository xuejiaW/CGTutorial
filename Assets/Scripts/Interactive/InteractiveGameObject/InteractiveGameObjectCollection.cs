using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<InteractiveGameObject> interactiveGo = null;
    public InteractiveGameObject holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<InteractiveGameObject, InteractiveGameObject> OnHoldingInteractiveGOUpdated = null;

    public override void Init()
    {
        base.Init();

        interactiveGo = new List<InteractiveGameObject>();
    }

    public void AddInteractiveGo(GameObject gameObject)
    {
        interactiveGo.Add(gameObject.GetComponent_AutoAdd<InteractiveGameObject>());
    }

    public void OnClickGameObject(GameObject GO)
    {
        Debug.Log("Onclick gameobject");
        InteractiveGameObject result = GO?.GetComponent<InteractiveGameObject>();
        OnHoldingInteractiveGOUpdated?.Invoke(holdingInteractiveGo, result);
        holdingInteractiveGo = result;
    }
}

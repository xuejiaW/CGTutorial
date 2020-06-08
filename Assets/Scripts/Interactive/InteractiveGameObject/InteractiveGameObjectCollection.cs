using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<InteractiveGameObject> interactiveGo = null;
    public InteractiveGameObject holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<InteractiveGameObject, InteractiveGameObject> OnHoldingInteractiveGOUpdated = null;

    protected override void Init()
    {
        base.Init();

        interactiveGo = new List<InteractiveGameObject>();

        // Handle for layer "InteractiveGo" and empty GO
        MouseInputManager.Instance.RegisterLeftClickMessageHandle(OnClickInteractiveGameObject, LayerMask.GetMask("InteractiveGO"), -1);
    }

    public void AddInteractiveGo(GameObject gameObject)
    {
        interactiveGo.Add(gameObject.GetComponent_AutoAdd<InteractiveGameObject>());
    }

    private void OnClickInteractiveGameObject(GameObject GO)
    {
        InteractiveGameObject result = GO?.GetComponent<InteractiveGameObject>();
        OnHoldingInteractiveGOUpdated?.Invoke(holdingInteractiveGo, result);
        holdingInteractiveGo = result;
    }
}

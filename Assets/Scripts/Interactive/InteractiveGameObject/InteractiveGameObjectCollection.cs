using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>, IUpdateObserver
{
    private Camera viewCamera = null;
    private List<InteractiveGameObject> interactiveGo = null;
    public InteractiveGameObject holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<InteractiveGameObject, InteractiveGameObject> OnInteractiveGOUpdated = null;

    private int interactiveGoLayer = 1 << 9;

    protected override void Init()
    {
        base.Init();
        GameManager.Instance.RegisterObserver(this);

        interactiveGo = new List<InteractiveGameObject>();
        viewCamera = Camera.main;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractiveGameObject result = null;
            if (Physics.Raycast(viewCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, interactiveGoLayer))
                result = hit.transform.GetComponent<InteractiveGameObject>();

            OnInteractiveGOUpdated?.Invoke(holdingInteractiveGo, result);
            holdingInteractiveGo = result;
        }
    }

    public void AddInteractiveGo(GameObject gameObject)
    {
        interactiveGo.Add(gameObject.GetComponent_AutoAdd<InteractiveGameObject>());
    }
}

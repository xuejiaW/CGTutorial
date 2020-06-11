using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicator : MonoBehaviour
{
    private Transform thisTransform = null;
    private GameObject thisGameObject = null;
    public GameObject attachedGameObject = null;

    private IIndicatorHandle indicatorHandle = null;

    //Awake will be called after gameobject is instantiated while Start will not
    public void Awake()
    {
        thisTransform = transform;
        thisGameObject = gameObject;
        thisGameObject.SetActive(false);

        // indicatorHandle = new IndicatorMovingHandle();
        // indicatorHandle.SetIndicator(this);
    }

    public InteractiveIndicator SetHandle(IIndicatorHandle handle)
    {
        indicatorHandle = handle;
        indicatorHandle.SetIndicator(this);
        return this;
    }

    public void SetParent(InteractiveGameObject interactiveGO)
    {
        thisTransform.SetParent(interactiveGO?.transform);
        thisTransform.localPosition = Vector3.zero;
        thisGameObject.SetActive(interactiveGO != null);
        attachedGameObject = interactiveGO?.gameObject;
    }

    public void ClickIndicatorAxis(string axisGame)
    {
        indicatorHandle.SetIndicatorAxis(axisGame);
    }

    public void DragIndicatorAxis(Vector3 deltaPos)
    {
        indicatorHandle.DragIndicatorAxis(deltaPos);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicator : MonoBehaviour
{
    private Transform thisTransform = null;
    private GameObject thisGameObject = null;
    private GameObject attachedGameObject = null;

    private Vector3 currentHandleAxis = Vector3.zero;

    //Awake will be called after gameobject is instantiated while Start will not
    public void Awake()
    {
        thisTransform = transform;
        thisGameObject = gameObject;
        thisGameObject.SetActive(false);
    }

    public void SetParent(InteractiveGameObject interactiveGO)
    {
        thisTransform.SetParent(interactiveGO?.transform);
        thisGameObject.SetActive(interactiveGO != null);
        attachedGameObject = interactiveGO?.gameObject;
    }

    public void ClickIndicatorAxis(string axisGame)
    {
        currentHandleAxis = -thisTransform.Find(axisGame).forward;
    }

    public void DragIndicatorAxis(Vector3 deltaPos)
    {
        //TODO: handle for other axis
        float result = Vector3.Dot(deltaPos, currentHandleAxis) / 30.0f;
        Debug.Log("result is " + result);
        Vector3 localPos = attachedGameObject.transform.localPosition;
        localPos.x += result;
        attachedGameObject.transform.localPosition = localPos;
    }

}

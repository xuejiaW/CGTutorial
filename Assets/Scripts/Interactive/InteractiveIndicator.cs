using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicator : MonoBehaviour
{
    private Transform thisTransform = null;
    private GameObject thisGameObject = null;

    //Awake will be called after gameobject is instantiated while Start will not
    public void Awake()
    {
        thisTransform = transform;
        thisGameObject = gameObject;
        thisGameObject.SetActive(false);
    }

    public void SetParent(InteractiveGameObject interactiveGO)
    {
        if (thisTransform == null)
            Debug.Log("this transform is null");

        thisTransform.SetParent(interactiveGO?.transform);
        thisGameObject.SetActive(interactiveGO != null);
    }
}

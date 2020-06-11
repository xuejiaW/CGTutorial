using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObject : MonoBehaviour
{
    public new GameObject gameObject { get; private set; }
    public new Transform transform { get; private set; }

    private void Start()
    {
        gameObject = base.gameObject;
        transform = base.transform;
    }
}

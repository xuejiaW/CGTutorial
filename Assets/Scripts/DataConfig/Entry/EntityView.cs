using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityView : MonoBehaviour
{
    protected Transform thisTransform = null;
    protected GameObject thisGameObject = null;

    [System.NonSerialized]
    public new Transform transform = null;
    [System.NonSerialized]
    public new GameObject gameObject = null;

    public DisplayableEntityModel entityModel { get; protected set; }

    public void BindEntityModel(DisplayableEntityModel model)
    {
        entityModel = model;
    }

    protected virtual void Awake()
    {
        transform = base.transform;
        gameObject = base.gameObject;
        thisTransform = transform;
        thisGameObject = gameObject;
    }
}

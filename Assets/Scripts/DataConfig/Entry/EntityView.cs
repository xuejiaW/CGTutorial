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

    public EntityModel model { get; protected set; }
    public EntityController controller { get; protected set; }

    public virtual void BindEntityModel(EntityModel model)
    {
        this.model = model;
    }

    public virtual void BindEntityController(EntityController controller)
    {
        this.controller = controller;
    }

    protected virtual void Awake()
    {
        transform = base.transform;
        gameObject = base.gameObject;
        thisTransform = transform;
        thisGameObject = gameObject;
    }
}

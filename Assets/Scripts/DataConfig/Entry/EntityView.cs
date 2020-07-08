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

    public DisplayableEntityModel model { get; protected set; }

    public virtual void BindEntityModel(DisplayableEntityModel model)
    {
        this.model = model;

        model.OnLocalPositionUpdated += SetLocalPosition;
        model.OnLocalRotationUpdated += SetLocalRotation;
        model.OnLocalScaleUpdated += SetLocalScale;

        model.OnParentUpdated += SetParent;
        model.OnActiveUpdated += SetActive;
    }

    protected virtual void Awake()
    {
        transform = base.transform;
        gameObject = base.gameObject;
        thisTransform = transform;
        thisGameObject = gameObject;
    }

    protected virtual void OnDestroy()
    {
        if (model == null)
            return;

        model.OnLocalPositionUpdated -= SetLocalPosition;
        model.OnParentUpdated -= SetParent;
        model.OnActiveUpdated -= SetActive;
        model.OnLocalScaleUpdated -= SetLocalScale;
    }

    protected virtual void SetActive(bool active) { thisGameObject.SetActive(active); }
    protected virtual void SetParent(DisplayableEntityModel trans)
    {
        // all the local transform properties should be handled by DisplayableModel rather than Unity Engine
        thisTransform.SetParent(trans?.view?.transform, false);
    }
    protected virtual void SetLocalPosition(Vector3 localPosition) { thisTransform.localPosition = localPosition; }
    protected virtual void SetLocalRotation(Quaternion localRotation) { thisTransform.localRotation = localRotation; }
    protected virtual void SetLocalScale(Vector3 localScale) { thisTransform.localScale = localScale; }

}
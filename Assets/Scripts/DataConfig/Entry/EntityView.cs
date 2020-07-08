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
    public EntityController controller { get; protected set; }

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

    private void SetActive(bool active) { thisGameObject.SetActive(active); }
    private void SetParent(Transform trans)
    {
        thisTransform.parent = trans;
        // Because the local position has been changed by Unity engine, so need to be manually set
        // TODO: moving these code to displayableModel, these properties should be handled in model
        model.localPosition = thisTransform.localPosition;
        model.localRotation = thisTransform.localRotation;
        model.localScale = thisTransform.localScale;
    }
    private void SetLocalPosition(Vector3 localPosition) { thisTransform.localPosition = localPosition; }
    private void SetLocalRotation(Quaternion localRotation) { thisTransform.localRotation = localRotation; }
    private void SetLocalScale(Vector3 localScale) { thisTransform.localScale = localScale; }

}
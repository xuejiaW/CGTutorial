using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayableEntityModel : EntityModel
{
    public string prefabPath = "";

    public EntityView view;
    public void BindEntityView(EntityView view) { this.view = view; }

    public new DisplayableEntityController controller;
    public override void BindEntityController(EntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = controller as DisplayableEntityController;
    }

    [System.NonSerialized] // avoid serialized loop
    private List<DisplayableEntityModel> childrenList = null;

    public DisplayableEntityModel() : base()
    {
        childrenList = new List<DisplayableEntityModel>();
    }

    #region Unity Transform Data
    public event System.Action<DisplayableEntityModel> OnParentUpdated = null;
    public event System.Action<bool> OnActiveUpdated = null;
    public event System.Action<Vector3> OnLocalPositionUpdated = null;
    public event System.Action<Quaternion> OnLocalRotationUpdated = null;
    public event System.Action<Vector3> OnLocalScaleUpdated = null;
    public event System.Action<Vector3> OnPositionUpdated = null;

    private bool m_active = true;
    public virtual bool active
    {
        get { return m_active; }
        set { m_active = value; OnActiveUpdated?.Invoke(value); }
    }

    private DisplayableEntityModel m_parent = null;
    public DisplayableEntityModel parent
    {
        get { return m_parent; }
        set { SetParent(value); }
    }

    public void SetParent(DisplayableEntityModel model, bool stayWorldPosition = true)
    {
        if (stayWorldPosition)
        {
            Vector3 deltaPos = this.position - (model != null ? model.position : Vector3.zero);
            this.localPosition = deltaPos;

            Quaternion deltaRot = (model != null ? Quaternion.Inverse(model.rotation) : Quaternion.identity) * this.rotation;
            this.localRotation = deltaRot;

            Vector3 deltaScale = this.lossyScale.Divide(model != null ? model.lossyScale : Vector3.one);
            this.localScale = deltaScale;
        }

        m_parent = model;
        model?.childrenList.Add(this);

        OnParentUpdated?.Invoke(model);
    }

    private Vector3 m_localPosition = Vector3.zero;
    public Vector3 localPosition
    {
        get { return m_localPosition; }
        set { m_localPosition = value; OnLocalPositionUpdated?.Invoke(value); }
    }

    public Vector3 position
    {
        get { return localPosition + (parent == null ? Vector3.zero : parent.position); }
        set { localPosition += (value - position); OnPositionUpdated?.Invoke(value); }
    }

    private Quaternion m_localRotation = Quaternion.identity;
    public Quaternion localRotation
    {
        get { return m_localRotation; }
        set { m_localRotation = value; OnLocalRotationUpdated?.Invoke(value); }
    }

    public Quaternion rotation
    {
        get { return (parent == null ? Quaternion.identity : parent.rotation) * localRotation; }
        set { localRotation = Quaternion.Inverse(rotation) * value; }
    }

    public Vector3 m_localScale = Vector3.one;
    public Vector3 localScale
    {
        get { return m_localScale; }
        set { m_localScale = value; OnLocalScaleUpdated?.Invoke(value); }
    }
    public Vector3 lossyScale
    {
        get { return localScale.Times(parent == null ? Vector3.one : parent.lossyScale); }
    }

    #endregion
}

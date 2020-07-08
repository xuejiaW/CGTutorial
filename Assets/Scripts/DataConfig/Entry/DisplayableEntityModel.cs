using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayableEntityModel : EntityModel
{
    public string prefabPath = "";

    public EntityView view;
    public void BindEntityView(EntityView view) { this.view = view; }


    #region Unity Transform Data
    public event System.Action<Transform> OnParentUpdated = null;
    public event System.Action<bool> OnActiveUpdated = null;
    public event System.Action<Vector3> OnLocalPositionUpdated = null;
    public event System.Action<Quaternion> OnLocalRotationUpdated = null;
    public event System.Action<Vector3> OnLocalScaleUpdated = null;

    public bool m_active = true;
    public bool active
    {
        get { return m_active; }
        set { m_active = value; OnActiveUpdated?.Invoke(value); }
    }

    private Transform m_parent = null;// TODO: use EntityView to instead of the Transform?
    public Transform parent
    {
        get { return m_parent; }
        set { m_parent = value; OnParentUpdated?.Invoke(value); }
    }


    private Vector3 m_localPosition = Vector3.zero;
    public Vector3 localPosition
    {
        get { return m_localPosition; }
        set { SetLocalPosition(value); }
    }

    public void SetLocalPosition(Vector3 pos)
    {
        m_localPosition = pos;
        OnLocalPositionUpdated?.Invoke(pos);
    }

    private Quaternion m_localRotation = Quaternion.identity;
    public Quaternion localRotation
    {
        get { return m_localRotation; }
        set { SetLocalRotation(value); }
    }
    public void SetLocalRotation(Quaternion rot)
    {
        m_localRotation = rot;
        OnLocalRotationUpdated?.Invoke(rot);
    }

    public Vector3 m_localScale = Vector3.one;
    public Vector3 localScale
    {
        get { return m_localScale; }
        set { SetLocalScale(value); }
    }

    public void SetLocalScale(Vector3 scale)
    {
        m_localScale = scale;
        OnLocalScaleUpdated?.Invoke(scale);
    }

    #endregion
}

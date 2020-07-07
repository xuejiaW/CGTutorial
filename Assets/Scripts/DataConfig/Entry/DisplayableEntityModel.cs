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
    public event System.Action<Vector3> OnPositionUpdated = null;
    public event System.Action<Quaternion> OnLocalRotationUpdated = null;
    public event System.Action<Quaternion> OnRotationUpdated = null;
    public event System.Action<Vector3> OnLocalScaleUpdated = null;

    public bool m_active = true;
    public bool active
    {
        get { return m_active; }
        set { m_active = value; OnActiveUpdated?.Invoke(value); }
    }

    public Transform m_parent = null;// TODO: use EntityView to instead of the Transform?
    public Transform parent
    {
        get { return m_parent; }
        set { m_parent = value; OnParentUpdated?.Invoke(value); }
    }

    public Vector3 m_position = Vector3.zero;
    public Vector3 position
    {
        get { return m_position; }
        set { m_position = value; OnPositionUpdated?.Invoke(value); }
    }

    public Vector3 m_localPosition = Vector3.zero;
    public Vector3 localPosition
    {
        get { return m_localPosition; }
        set { m_localPosition = value; OnLocalPositionUpdated?.Invoke(value); }
    }

    public Quaternion m_localRotation = Quaternion.identity;
    public Quaternion localRotation
    {
        get { return m_localRotation; }
        set { m_localRotation = value; OnLocalRotationUpdated?.Invoke(value); }
    }

    public Quaternion m_Rotation = Quaternion.identity;
    public Quaternion Rotation
    {
        get { return m_Rotation; }
        set { m_Rotation = value; OnRotationUpdated?.Invoke(value); }
    }

    public Vector3 m_localScale = Vector3.one;
    public Vector3 localScale
    {
        get { return m_localScale; }
        set { m_localScale = value; OnLocalScaleUpdated?.Invoke(value); }
    }

    public Vector3 m_Scale = Vector3.one;
    public Vector3 Scale
    {
        get { return m_Scale; }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractiveGameObjectModel : DisplayableEntityModel
{
    public override System.Type GetViewType()
    {
        return typeof(InteractiveGameObjectView);
    }

    public event System.Action<Vector3> OnLocalPositionUpdated = null;
    public event System.Action<Vector3> OnLocalRotationUpdated = null;
    public event System.Action<Vector3> OnLocalSizeUpdated = null;
    public event System.Action<Vector3> OnPositionUpdated = null;
    public event System.Action<Vector3> OnRotationUpdated = null;
    public event System.Action<Vector3> OnSizeUpdated = null;

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
}

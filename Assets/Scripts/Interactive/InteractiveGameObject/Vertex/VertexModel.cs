using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(VertexView); }
    public override System.Type GetControllerType() { return typeof(VertexController); }
    public event System.Action<Vector2> OnTexcoordUpdated = null;
    
    private Vector2 _texcoord = Vector2.zero;
    public Vector2 texcoord
    {
        get { return _texcoord; }
        set
        {
            _texcoord = value;
            OnTexcoordUpdated?.Invoke(value);
        }
    }
}

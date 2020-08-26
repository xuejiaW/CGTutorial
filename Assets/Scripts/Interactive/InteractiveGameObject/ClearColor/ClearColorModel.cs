using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(ClearColorView); }
    public override System.Type GetControllerType() { return typeof(ClearColorController); }

    public event System.Action<Color> OnClearColorChanged = null;

    private Color _clearColor = Color.black;

    public Color clearColor
    {
        get { return _clearColor; }
        set
        {
            _clearColor = value;
            OnClearColorChanged?.Invoke(value);
        }
    }
}

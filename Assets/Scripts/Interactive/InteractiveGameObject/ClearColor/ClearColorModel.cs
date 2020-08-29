using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(ClearColorView); }
    public override System.Type GetControllerType() { return typeof(ClearColorController); }

    public event System.Action<Color> OnColorChanged = null;

    private Color _color = Color.black;

    public Color color
    {
        get { return _color; }
        set
        {
            _color = value;
            OnColorChanged?.Invoke(value);
        }
    }
}

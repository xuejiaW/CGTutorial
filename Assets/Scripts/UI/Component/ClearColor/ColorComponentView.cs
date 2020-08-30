using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorComponentView : ComponentView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new ColorViewUpdater();
    }
}

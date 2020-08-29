using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : ComponentView
{
    public override IUpdateView GetViewUpdater()
    {
        return new ColorViewUpdater();
    }

}

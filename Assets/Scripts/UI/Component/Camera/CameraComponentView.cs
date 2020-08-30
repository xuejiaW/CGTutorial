using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraComponentView : ComponentView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new CameraViewUpdater();
    }
}

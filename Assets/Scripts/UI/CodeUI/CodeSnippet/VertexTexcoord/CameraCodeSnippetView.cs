using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCodeSnippetView : CodeSnippetView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new CameraViewUpdater();
    }
}

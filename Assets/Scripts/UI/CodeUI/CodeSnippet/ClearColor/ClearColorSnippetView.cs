using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetView : CodeSnippetView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new ColorViewUpdater();
    }
}

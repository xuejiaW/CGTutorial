using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCodeSnippetView : CodeSnippetView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new ColorViewUpdater();
    }
}

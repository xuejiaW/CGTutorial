using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetView : CodeSnippetView
{
    public override IUpdateView GetViewUpdater()
    {
        return new ColorViewUpdater();
    }
}

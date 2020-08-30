using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexcoordCodeSnippetView : CodeSnippetView
{
    public override UpdateViewBase GetViewUpdater()
    {
        return new TexcoordViewUpdater();
    }
}

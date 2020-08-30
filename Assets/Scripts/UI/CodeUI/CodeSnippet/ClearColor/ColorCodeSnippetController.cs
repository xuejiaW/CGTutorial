using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCodeSnippetController : CodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}

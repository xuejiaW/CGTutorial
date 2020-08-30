using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetController : CodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCodeSnippetController : TransformCodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalPositionModelUpdater();
    }
}

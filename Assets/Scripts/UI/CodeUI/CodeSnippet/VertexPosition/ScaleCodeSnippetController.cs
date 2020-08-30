using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCodeSnippetController : TransformCodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalScaleModelUpdater();
    }
}

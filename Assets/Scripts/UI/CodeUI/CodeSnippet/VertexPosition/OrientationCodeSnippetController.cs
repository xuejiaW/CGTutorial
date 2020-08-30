using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCodeSnippetController : TransformCodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new OrientationModelUpdater();
    }
}

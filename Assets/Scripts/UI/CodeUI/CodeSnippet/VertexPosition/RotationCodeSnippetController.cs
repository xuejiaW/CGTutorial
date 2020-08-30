using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCodeSnippetController : TransformCodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalRotationModelUpdater();
    }
}

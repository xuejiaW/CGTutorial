using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionSnippetController : TransformCodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalPositionModelUpdater();
    }
}

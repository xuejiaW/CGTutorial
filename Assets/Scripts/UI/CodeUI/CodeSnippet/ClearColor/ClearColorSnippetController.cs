using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetController : CodeSnippetController
{
    public override IUpdateModelProperty GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}

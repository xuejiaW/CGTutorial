using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCodeSnippetController : CodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new CameraModelUpdater();
    }
}

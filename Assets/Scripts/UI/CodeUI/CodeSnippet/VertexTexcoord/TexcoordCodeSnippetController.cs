using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexcoordCodeSnippetController : CodeSnippetController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new TexcoordModelUpdater();
    }
}

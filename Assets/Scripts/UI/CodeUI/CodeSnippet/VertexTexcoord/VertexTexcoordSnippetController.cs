using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTexcoordSnippetController : CodeSnippetController
{
    private VertexModel targetModel = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();
        targetModel = model.targetGameObject as VertexModel;
        Debug.Log("enter texcoord controller");
    }

    public void SetTargetGOTexcoord(int axis, string val)
    {
        float.TryParse(val, out float value);
        Vector2 texcoord = targetModel.texcoord;
        texcoord[axis] = value;
        targetModel.texcoord = texcoord;
    }
}

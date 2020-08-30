using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexcoordComponentController : ComponentController
{
    private VertexModel targetGO = null;

    public override void InitComponent()
    {
        base.InitComponent();
        targetGO = model.targetGameObject as VertexModel;
    }

    public void SetTexcoord(int axis, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);
        Vector2 texcoord = targetGO.texcoord;
        texcoord[axis] = val;
        targetGO.texcoord = texcoord;
    }
}

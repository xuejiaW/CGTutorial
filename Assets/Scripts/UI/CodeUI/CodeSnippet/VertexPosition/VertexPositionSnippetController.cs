using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionSnippetController : CodeSnippetController
{
    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();
        Debug.Log("Enter vertex pos controller");
    }

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public void SetTargetGOPosition(int axis, string value)
    {
        if (!model.isOn) return;

        float.TryParse(value, out float val);

        Vector3 currLocalPos = targetGameObject.localPosition;
        currLocalPos[axis] = val;
        targetGameObject.localPosition = currLocalPos;
    }

    public void SetModelPosition(int axis, string value)
    {
        if (!model.isOn) return;

        float.TryParse(value, out float val);

        Vector3 currLocalPos = model.targetGameObject.localPosition;
        currLocalPos[axis] = val;
        model.targetGameObject.localPosition = currLocalPos;
    }
}

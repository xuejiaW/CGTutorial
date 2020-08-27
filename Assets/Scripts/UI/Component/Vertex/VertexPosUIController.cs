using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIController : ComponentController
{
    private new VertexPosUIModel model = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as VertexPosUIModel;
    }

    public void SetPosition(int axis, string value)
    {
        if (!model.active) return;

        float.TryParse(value, out float val);

        Vector3 currLocalPos = targetGameObject.localPosition;
        currLocalPos[axis] = val;
        targetGameObject.localPosition = currLocalPos;
    }
}

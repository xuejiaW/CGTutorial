using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIController : ComponentController
{
    private new TransformUIModel model = null;
    // private CodeSnippetInputAdaptor adaptor = null;

    // The actual object that Transform component controlls is the current active Indicator
    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TransformUIModel;
    }


    #region Function which modify target
    public void SetPosition(int axis, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);

        Vector3 currLocalPos = targetGameObject.localPosition;

        if (axis == 0)
            targetGameObject.localPosition = currLocalPos.SetX(val);
        else if (axis == 1)
            targetGameObject.localPosition = currLocalPos.SetY(val);
        else if (axis == 2)
            targetGameObject.localPosition = currLocalPos.SetZ(val);
    }

    public void SetRotation(int axis, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);
        val *= -1;

        Vector3 currLocalRotEuler = targetGameObject.localRotation.eulerAngles;

        if (axis == 0)
            targetGameObject.localRotation = Quaternion.Euler(val, currLocalRotEuler.y, currLocalRotEuler.z);
        else if (axis == 1)
            targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, val, currLocalRotEuler.z);
        else if (axis == 2)
            targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, currLocalRotEuler.y, val);
    }

    public void SetScaling(int axis, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);

        // the scale value displayed on the transformUI is GO's localScale * indicator's localScale
        Vector3 currLocalScale = targetGameObject.localScale;
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;

        currLocalScale = currLocalScale.Times(holdingGOScale);

        if (axis == 0)
            currLocalScale.SetX(val);
        else if (axis == 1)
            currLocalScale.SetY(val);
        else if (axis == 2)
            currLocalScale.SetZ(val);

        targetGameObject.localScale = currLocalScale.Divide(holdingGOScale);
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationSnippetController : CodeSnippetController
{
    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitCodeSnippet()
    {
        Debug.Log("Enter transformation controller");
    }

    public void SetTargetGOPosition(int axis, string value)
    {
        if (!model.isOn) return;
        float.TryParse(value, out float val);

        Vector3 currLocalPos = targetGameObject.localPosition;

        if (axis == 0)
            targetGameObject.localPosition = currLocalPos.SetX(val);
        else if (axis == 1)
            targetGameObject.localPosition = currLocalPos.SetY(val);
        else if (axis == 2)
            targetGameObject.localPosition = currLocalPos.SetZ(val);
    }

    public void SetTargetGORotation(int axis, string value)
    {
        if (!model.isOn) return;
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

    public void SetTargetGOScaling(int axis, string value)
    {
        if (!model.isOn) return;
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

    public void SetTargetPosition(int axis, string value)
    {
        if (!model.isOn) return;
        float.TryParse(value, out float val);

        Vector3 currLocalPos = model.targetGameObject.localPosition;

        if (axis == 0)
            model.targetGameObject.localPosition = currLocalPos.SetX(val);
        else if (axis == 1)
            model.targetGameObject.localPosition = currLocalPos.SetY(val);
        else if (axis == 2)
            model.targetGameObject.localPosition = currLocalPos.SetZ(val);
    }

    public void SetTargetRotation(int axis, string value)
    {
        if (!model.isOn) return;
        float.TryParse(value, out float val);
        val *= -1;

        Vector3 currLocalRotEuler = model.targetGameObject.localRotation.eulerAngles;

        if (axis == 0)
            model.targetGameObject.localRotation = Quaternion.Euler(val, currLocalRotEuler.y, currLocalRotEuler.z);
        else if (axis == 1)
            model.targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, val, currLocalRotEuler.z);
        else if (axis == 2)
            model.targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, currLocalRotEuler.y, val);
    }

    public void SetTargetScaling(int axis, string value)
    {
        if (!model.isOn) return;
        float.TryParse(value, out float val);

        // the scale value displayed on the transformUI is GO's localScale * indicator's localScale
        Vector3 currLocalScale = model.targetGameObject.localScale;
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;

        currLocalScale = currLocalScale.Times(holdingGOScale);

        if (axis == 0)
            currLocalScale.SetX(val);
        else if (axis == 1)
            currLocalScale.SetY(val);
        else if (axis == 2)
            currLocalScale.SetZ(val);

        model.targetGameObject.localScale = currLocalScale.Divide(holdingGOScale);
    }
}

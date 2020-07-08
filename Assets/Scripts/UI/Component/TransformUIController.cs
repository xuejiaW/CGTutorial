using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUIController : DisplayableEntityController
{
    private new TransformUIModel model = null;
    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TransformUIModel;
    }

    public void SetPosition(int axis, string value)
    {
        float.TryParse(value, out float val);

        Vector3 currLocalPos = model.targetGameObject.localPosition;

        if (axis == 0)
            model.targetGameObject.localPosition = currLocalPos.SetX(val);
        else if (axis == 1)
            model.targetGameObject.localPosition = currLocalPos.SetY(val);
        else if (axis == 2)
            model.targetGameObject.localPosition = currLocalPos.SetZ(val);
    }

    public void SetRotation(int axis, string value)
    {
        float.TryParse(value, out float val);

        Vector3 currLocalRotEuler = model.targetGameObject.localRotation.eulerAngles;

        if (axis == 0)
            model.targetGameObject.localRotation = Quaternion.Euler(val, currLocalRotEuler.y, currLocalRotEuler.z);
        else if (axis == 1)
            model.targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, val, currLocalRotEuler.z);
        else if (axis == 2)
            model.targetGameObject.localRotation = Quaternion.Euler(currLocalRotEuler.x, currLocalRotEuler.y, val);
    }

    public void SetScaling(int axis, string value)
    {
        float.TryParse(value, out float val);

        Vector3 currLocalScale = model.targetGameObject.localScale;

        if (axis == 0)
            model.targetGameObject.localScale = currLocalScale.SetX(val);
        else if (axis == 1)
            model.targetGameObject.localScale = currLocalScale.SetY(val);
        else if (axis == 2)
            model.targetGameObject.localScale = currLocalScale.SetZ(val);
    }

    public void UpdateUIPositionData(Vector3 pos)
    {
        model.inputFields[0].text = pos.x.ToString("f2");
        model.inputFields[1].text = pos.y.ToString("f2");
        model.inputFields[2].text = pos.z.ToString("f2");
    }

    public void UpdateUIRotationData(Quaternion rot)
    {
        //TODO: Clamp
        Vector3 euler = rot.eulerAngles;
        model.inputFields[3].text = euler.x.ToString("f2");
        model.inputFields[4].text = euler.y.ToString("f2");
        model.inputFields[5].text = euler.z.ToString("f2");
    }

    public void UpdateUIScaleData(Vector3 scale)
    {
        model.inputFields[6].text = scale.x.ToString("f2");
        model.inputFields[7].text = scale.y.ToString("f2");
        model.inputFields[8].text = scale.z.ToString("f2");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIView : ComponentView
{
    private new TransformUIModel model = null;
    private new TransformUIController controller = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TransformUIModel;
    }

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as TransformUIController;

        for (int i = 0; i != inputFields.Length; ++i)
        {
            int index = i, axis = i % 3;
            if (index <= 2)
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetPosition(axis, val));
            else if (index <= 5)
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetRotation(axis, val));
            else if (index <= 8)
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetScaling(axis, val));
        }
    }

    public override void InitComponent()
    {
        base.InitComponent();
        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    private void onModelActiveUpdated(bool active)
    {

        if (active)
        {
            RegisterControllerHandle(targetGameObject);
            UpdateUIRotationData(targetGameObject.localRotation);
            UpdateUIPositionData(targetGameObject.localPosition);
            UpdateUIScaleData(targetGameObject.localScale);
        }
        else
        {
            UnRegisterControllerHandle(targetGameObject);
            RefreshUIData();
        }
    }

    private void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // modify the callback when indicator changed
        if (oldIndicator != null)
            UnRegisterControllerHandle(oldIndicator.model);
        if (newIndicator != null)
            RegisterControllerHandle(newIndicator.model);
    }

    private void RegisterControllerHandle(DisplayableEntityModel targetModel)
    {
        targetModel.OnLocalPositionUpdated += UpdateUIPositionData;
        targetModel.OnLocalRotationUpdated += UpdateUIRotationData;
        targetModel.OnLocalScaleUpdated += UpdateUIScaleData;
    }

    private void UnRegisterControllerHandle(DisplayableEntityModel targetModel)
    {
        targetModel.OnLocalPositionUpdated -= UpdateUIPositionData;
        targetModel.OnLocalRotationUpdated -= UpdateUIRotationData;
        targetModel.OnLocalScaleUpdated -= UpdateUIScaleData;
    }


    public void UpdateUIPositionData(Vector3 pos)
    {
        if (!model.active) return;

        inputFields[0].text = pos.x.ToString("f2");
        inputFields[1].text = pos.y.ToString("f2");
        inputFields[2].text = pos.z.ToString("f2");
    }

    public void UpdateUIRotationData(Quaternion rot)
    {
        if (!model.active) return;

        Vector3 euler = rot.eulerAngles;
        inputFields[3].text = (rotateValueClamp(euler.x) * -1.0f).ToString("f2");
        inputFields[4].text = (rotateValueClamp(euler.y) * -1.0f).ToString("f2");
        inputFields[5].text = (rotateValueClamp(euler.z) * -1.0f).ToString("f2");
    }

    public void UpdateUIScaleData(Vector3 scale)
    {
        if (!model.active) return;

        // the scale value displayed on the transformUI is GO's localScale * indicator's localScale
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;
        scale = scale.Times(holdingGOScale);
        inputFields[6].text = scale.x.ToString("f2");
        inputFields[7].text = scale.y.ToString("f2");
        inputFields[8].text = scale.z.ToString("f2");
    }

    private void RefreshUIData()
    {
        Quaternion rotation = model.targetGameObject.localRotation;
        Vector3 position = model.targetGameObject.localPosition;
        Vector3 scale = model.targetGameObject.localScale;
        for (int i = 0; i != 3; ++i)
            inputFields[i].text = position[i].ToString("f2");
        for (int i = 0; i != 3; ++i)
            inputFields[i + 3].text = rotation[i].ToString("f2");
        for (int i = 0; i != 3; ++i)
            inputFields[i + 6].text = scale[i].ToString("f2");
    }

    private float rotateValueClamp(float value)
    {
        float result = value - 360.0f * (((int)value + 180) / 360);
        return result;
    }

    ~TransformUIView()
    {
        model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}

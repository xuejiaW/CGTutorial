using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIModel : DisplayableEntityModel
{
    private new TransformUIController controller = null;
    public override Type GetControllerType()
    {
        return typeof(TransformUIController);
    }

    public override Type GetViewType()
    {
        return typeof(TransformUIView);
    }

    // 0-2 position | 3-5 rotation | 6-8 scaling
    public InputField[] inputFields = null;

    public DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityController(EntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as TransformUIController;

        for (int i = 0; i != inputFields.Length; ++i)
        {
            int axis = i % 3;
            if (i <= 2)
                inputFields[i].onValueChanged.AddListener((val) => this.controller.SetPosition(axis, val));
            else if (i <= 5)
                inputFields[i].onValueChanged.AddListener((val) => this.controller.SetRotation(axis, val));
            else if (i <= 8)
                inputFields[i].onValueChanged.AddListener((val) => this.controller.SetScaling(axis, val));
        }

        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    public override bool active
    {
        get { return base.active; }
        set
        {
            base.active = value;
            if (targetGameObject == null)
                return;

            if (value)
                RegisterControllerHandle(targetGameObject);
            else
                UnRegisterControllerHandle(targetGameObject);

            controller.UpdateUIRotationData(targetGameObject.localRotation);
            controller.UpdateUIPositionData(targetGameObject.localPosition);
            controller.UpdateUIScaleData(targetGameObject.localScale);
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

    private void RegisterControllerHandle(DisplayableEntityModel model)
    {
        model.OnLocalPositionUpdated += controller.UpdateUIPositionData;
        model.OnLocalRotationUpdated += controller.UpdateUIRotationData;
        model.OnLocalScaleUpdated += controller.UpdateUIScaleData;
    }

    private void UnRegisterControllerHandle(DisplayableEntityModel model)
    {
        model.OnLocalPositionUpdated -= controller.UpdateUIPositionData;
        model.OnLocalRotationUpdated -= controller.UpdateUIRotationData;
        model.OnLocalScaleUpdated -= controller.UpdateUIScaleData;
    }

    ~TransformUIModel()
    {
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}

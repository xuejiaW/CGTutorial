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

    public override void Init()
    {
        base.Init();

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int axis = i % 3;
            if (i <= 2)
                this.model.inputFields[i].onValueChanged.AddListener((val) => SetPosition(axis, val));
            else if (i <= 5)
                this.model.inputFields[i].onValueChanged.AddListener((val) => SetRotation(axis, val));
            else if (i <= 8)
                this.model.inputFields[i].onValueChanged.AddListener((val) => SetScaling(axis, val));
        }

        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;

        SwitchPositionField(CoursesAdaptor.Instance.currentCourse.allowTranslate);
        SwitchRotationField(CoursesAdaptor.Instance.currentCourse.allowRotate);
        SwitchScalingField(CoursesAdaptor.Instance.currentCourse.allowScale);
    }


    public void SwitchPositionField(bool on)
    {
        for (int i = 0; i != 3; ++i)
            model.inputFields[i].interactable = on;
    }

    public void SwitchRotationField(bool on)
    {
        for (int i = 3; i != 6; ++i)
            model.inputFields[i].interactable = on;
    }

    public void SwitchScalingField(bool on)
    {
        for (int i = 6; i != 9; ++i)
            model.inputFields[i].interactable = on;
    }


    private void onModelActiveUpdated(bool active)
    {
        DisplayableEntityModel target = model.targetGameObject;

        if (target == null)
            return;

        if (active)
            RegisterControllerHandle(target);
        else
            UnRegisterControllerHandle(target);

        UpdateUIRotationData(target.localRotation);
        UpdateUIPositionData(target.localPosition);
        UpdateUIScaleData(target.localScale);
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

    #region Functions which interact with UI data
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
        // the scale value displayed on the transformUI is GO's localScale * indicator's localScale
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;
        scale = scale.Times(holdingGOScale);
        model.inputFields[6].text = scale.x.ToString("f2");
        model.inputFields[7].text = scale.y.ToString("f2");
        model.inputFields[8].text = scale.z.ToString("f2");
    }
    #endregion
    ~TransformUIController()
    {
        this.model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}

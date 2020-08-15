using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIController : ComponentController
{
    private new TransformUIModel model = null;
    private CodeSnippetInputAdaptor adaptor = null;

    private bool allowTranslate = false;
    private bool allowRotate = false;
    private bool allowScale = false;

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

    public override void Init()
    {
        base.Init();

        // Modify the code snippet can lead to change of code and component ui
        adaptor = new CodeSnippetInputAdaptor();
        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int axis = i % 3;
            if (i <= 2)
                adaptor.BindValueChangedEvent((val => SetPosition(axis, val)));
            else if (i <= 5 && allowRotate)
                adaptor.BindValueChangedEvent((val => SetRotation(axis, val)));
            else if (i <= 8 && allowScale)
                adaptor.BindValueChangedEvent((val => SetScaling(axis, val)));
        }
        CodeSnippetManager.Instance.BindSnippetAdaptor(adaptor);

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            InputField inputField = model.inputFields[i];

            int index = i, axis = i % 3;
            if (index <= 2)
                inputField.onEndEdit.AddListener((val) => SetPosition(axis, val));
            else if (index <= 5)
                inputField.onEndEdit.AddListener((val) => SetRotation(axis, val));
            else if (index <= 8)
                inputField.onEndEdit.AddListener((val) => SetScaling(axis, val));

            // Change the code snippet after ui data changed
            if (index < adaptor.dataCount)
            {
                // For manually edit input field
                inputField.onEndEdit.AddListener(val => adaptor.editableParts[index].text = val);

                // For inputField modified which caused by the interaction with the interactive GO
                inputField.onValueChanged.AddListener((val =>
                { if (!inputField.isFocused) adaptor.editableParts[index].text = val; }));
            }
        }


        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;

        // According to the course setting, disable some input field
        SwitchPositionField(CoursesAdaptor.Instance.currentCourse.allowTranslate);
        SwitchRotationField(CoursesAdaptor.Instance.currentCourse.allowRotate);
        SwitchScalingField(CoursesAdaptor.Instance.currentCourse.allowScale);
    }

    public override void InitComponent()
    {
        base.InitComponent();
        Debug.Log("enter init component");
        UpdateTargetAccording2Code(model.targetGameObject);
    }

    public void UpdateTargetAccording2Code(DisplayableEntityModel targetGO)
    {
        if (adaptor.dataCount == 3)
        {
            float.TryParse(adaptor.editableParts[0].text, out float x);
            float.TryParse(adaptor.editableParts[1].text, out float y);
            float.TryParse(adaptor.editableParts[2].text, out float z);
            targetGO.localPosition = new Vector3(x, y, z);
        }
    }

    public void SwitchPositionField(bool on)
    {
        allowTranslate = on;
        for (int i = 0; i != 3; ++i)
            model.inputFields[i].interactable = on;
    }

    public void SwitchRotationField(bool on)
    {
        allowRotate = on;
        for (int i = 3; i != 6; ++i)
            model.inputFields[i].interactable = on;
    }

    public void SwitchScalingField(bool on)
    {
        allowScale = on;
        for (int i = 6; i != 9; ++i)
            model.inputFields[i].interactable = on;
    }


    private void onModelActiveUpdated(bool active)
    {
        // Only make the code snippet that related to the target interactable
        // This is not only for the display effect, but also for avoiding bug that all Transform component only
        // care current indicator which means all code snippet will modify the selected target no matter it is related
        // to the target or not
        adaptor.editableParts.ForEach(inputField => inputField.interactable = active);

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

    #region Function which modify target
    public void SetPosition(int axis, string value)
    {
        if (!allowTranslate || !model.active) return;
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
        if (!allowRotate || !model.active) return;
        float.TryParse(value, out float val);

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
        if (!allowScale || !model.active) return;
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
    #region Functions which interact with UI data
    public void UpdateUIPositionData(Vector3 pos)
    {
        if (!allowTranslate || !model.active) return;

        model.inputFields[0].text = pos.x.ToString("f2");
        model.inputFields[1].text = pos.y.ToString("f2");
        model.inputFields[2].text = pos.z.ToString("f2");
    }

    public void UpdateUIRotationData(Quaternion rot)
    {
        if (!allowRotate || !model.active) return;

        //TODO: Clamp
        Vector3 euler = rot.eulerAngles;
        model.inputFields[3].text = euler.x.ToString("f2");
        model.inputFields[4].text = euler.y.ToString("f2");
        model.inputFields[5].text = euler.z.ToString("f2");
    }

    public void UpdateUIScaleData(Vector3 scale)
    {
        if (!allowScale || !model.active) return;

        // the scale value displayed on the transformUI is GO's localScale * indicator's localScale
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;
        scale = scale.Times(holdingGOScale);
        model.inputFields[6].text = scale.x.ToString("f2");
        model.inputFields[7].text = scale.y.ToString("f2");
        model.inputFields[8].text = scale.z.ToString("f2");
    }

    private void RefreshUIData()
    {
        Quaternion rotation = model.targetGameObject.localRotation;
        Vector3 position = model.targetGameObject.localPosition;
        Vector3 scale = model.targetGameObject.localScale;
        for (int i = 0; i != 3; ++i)
            model.inputFields[i].text = position[i].ToString("f2");
        if (allowRotate)
        {
            for (int i = 0; i != 3; ++i)
                model.inputFields[i + 3].text = rotation[i].ToString("f2");
        }
        if (allowScale)
        {
            for (int i = 0; i != 3; ++i)
                model.inputFields[i + 6].text = scale[i].ToString("f2");
        }
    }

    #endregion
    ~TransformUIController()
    {
        this.model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}

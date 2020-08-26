using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIController : ComponentController
{
    private new TransformUIModel model = null;
    private CodeSnippetInputAdaptor adaptor = null;

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
            else if (i <= 5)
                adaptor.BindValueChangedEvent((val => SetRotation(axis, val)));
            else if (i <= 8)
                adaptor.BindValueChangedEvent((val => SetScaling(axis, val)));
        }
        CodeBlockManager.Instance.BindSnippetAdaptor(adaptor);

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            InputField inputField = model.inputFields[i];

            int index = i, axis = i % 3;
            if (index <= 2)
            {
                inputField.onEndEdit.AddListener((val) => SetPosition(axis, val));
            }
            else if (index <= 5)
            {
                inputField.onEndEdit.AddListener((val) => SetRotation(axis, val));
            }
            else if (index <= 8)
            {
                inputField.onEndEdit.AddListener((val) => SetScaling(axis, val));
            }

            inputField.onEndEdit.AddListener(val => adaptor.editableParts[index].text = val);
            inputField.onValueChanged.AddListener((val =>
            { if (!inputField.isFocused) adaptor.editableParts[index].text = val; }));
        }

        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;

    }

    public override void InitComponent()
    {
        base.InitComponent();
        UpdateTargetAccording2Code(model.targetGameObject);
    }

    public void UpdateTargetAccording2Code(DisplayableEntityModel targetGO)
    {
        Vector3 position = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        Vector3 scale = Vector3.zero;

        for (int i = 0; i != 3; ++i)
        {
            float.TryParse(adaptor.editableParts[i].text, out float posVal);
            position[i] = posVal;

            float.TryParse(adaptor.editableParts[i + 3].text, out float rotVal);
            rotation[i] = -rotVal;

            float.TryParse(adaptor.editableParts[i + 6].text, out float scaleVal);
            scale[i] = scaleVal;
        }

        targetGO.localPosition = position;
        targetGO.localRotation = Quaternion.Euler(rotation);
        targetGO.localScale = scale;
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
    #region Functions which interact with UI data
    public void UpdateUIPositionData(Vector3 pos)
    {
        if (!model.active) return;

        model.inputFields[0].text = pos.x.ToString("f2");
        model.inputFields[1].text = pos.y.ToString("f2");
        model.inputFields[2].text = pos.z.ToString("f2");
    }

    public void UpdateUIRotationData(Quaternion rot)
    {
        if (!model.active) return;

        Vector3 euler = rot.eulerAngles;
        model.inputFields[3].text = (rotateValueClamp(euler.x) * -1.0f).ToString("f2");
        model.inputFields[4].text = (rotateValueClamp(euler.y) * -1.0f).ToString("f2");
        model.inputFields[5].text = (rotateValueClamp(euler.z) * -1.0f).ToString("f2");
    }

    public void UpdateUIScaleData(Vector3 scale)
    {
        if (!model.active) return;

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
        for (int i = 0; i != 3; ++i)
            model.inputFields[i + 3].text = rotation[i].ToString("f2");
        for (int i = 0; i != 3; ++i)
            model.inputFields[i + 6].text = scale[i].ToString("f2");
    }

    private float rotateValueClamp(float value)
    {
        float result = value - 360.0f * (((int)value + 180) / 360);
        return result;
    }
    #endregion
    ~TransformUIController()
    {
        this.model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}

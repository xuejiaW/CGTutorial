using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationSnippetView : CodeSnippetView
{
    public new TransformationSnippetController controller = null;

    private DisplayableEntityModel targetGO
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();

        controller = base.controller as TransformationSnippetController;
        Debug.Log("snippet count is " + inputFields.Count);
        for (int i = 0; i != inputFields.Count; ++i)
        {
            int index = i, axis = i % 3;
            if (index <= 2)
            {
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetTargetGOPosition(axis, val));
                this.controller.SetTargetPosition(axis, inputFields[i].text);
            }
            else if (index <= 5)
            {
                if (inputFields[i] == null)
                    Debug.Log(" list is null");
                if (this.controller == null)
                    Debug.Log("controller is null");
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetTargetGORotation(axis, val));
                this.controller.SetTargetRotation(axis, inputFields[i].text);
            }
            else if (index <= 8)
            {
                inputFields[i].onEndEdit.AddListener((val) => this.controller.SetTargetGOScaling(axis, val));
                this.controller.SetTargetScaling(axis, inputFields[i].text);
            }
        }
        Debug.Log("Enter transformation view");

        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    private void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // modify the callback when indicator changed
        if (oldIndicator != null)
            UnRegisterControllerHandle(oldIndicator.model);
        if (newIndicator != null)
            RegisterControllerHandle(newIndicator.model);
    }

    public override void Switch(bool on)
    {
        base.Switch(on);
        if (on)
        {
            RegisterControllerHandle(targetGO);
            UpdateUIRotationData(targetGO.localRotation);
            UpdateUIPositionData(targetGO.localPosition);
            UpdateUIScaleData(targetGO.localScale);
        }
        else
        {
            UnRegisterControllerHandle(targetGO);
            RefreshUIData();
        }
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
        if (!model.isOn) return;

        inputFields[0].text = pos.x.ToString("f2");
        inputFields[1].text = pos.y.ToString("f2");
        inputFields[2].text = pos.z.ToString("f2");
    }

    public void UpdateUIRotationData(Quaternion rot)
    {
        if (!model.isOn) return;

        Vector3 euler = rot.eulerAngles;
        inputFields[3].text = (rotateValueClamp(euler.x) * -1.0f).ToString("f2");
        inputFields[4].text = (rotateValueClamp(euler.y) * -1.0f).ToString("f2");
        inputFields[5].text = (rotateValueClamp(euler.z) * -1.0f).ToString("f2");
    }

    public void UpdateUIScaleData(Vector3 scale)
    {
        if (!model.isOn) return;

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
        Vector3 rotation = model.targetGameObject.localRotation.eulerAngles;
        Vector3 position = model.targetGameObject.localPosition;
        Vector3 scale = model.targetGameObject.localScale;
        for (int i = 0; i != 3; ++i)
            inputFields[i].text = position[i].ToString("f2");
        for (int i = 0; i != 3; ++i)
            inputFields[i + 3].text = (rotateValueClamp(rotation[i]) * -1.0f).ToString("f2");
        for (int i = 0; i != 3; ++i)
            inputFields[i + 6].text = scale[i].ToString("f2");
    }

    private float rotateValueClamp(float value)
    {
        float result = value - 360.0f * (((int)value + 180) / 360);
        return result;
    }

}

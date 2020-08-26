using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIController : ComponentController
{
    private new VertexPosUIModel model = null;
    private CodeSnippetInputAdaptor adaptor = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as VertexPosUIModel;
    }

    public override void Init()
    {
        base.Init();

        adaptor = new CodeSnippetInputAdaptor();
        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int axis = i;
            adaptor.BindValueChangedEvent((val => SetPosition(axis, val)));
        }
        CodeBlockManager.Instance.BindSnippetAdaptor(adaptor);

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int index = i, axis = i % 3;
            InputField inputField = model.inputFields[i];

            inputField.onEndEdit.AddListener((val) => SetPosition(axis, val));
            inputField.onEndEdit.AddListener(val => adaptor.editableParts[index].text = val);
            inputField.onValueChanged.AddListener(val =>
            {
                if (!inputField.isFocused)
                    adaptor.editableParts[index].text = val;
            });
        }


        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    public override void InitComponent()
    {
        base.InitComponent();
        float.TryParse(adaptor.editableParts[0].text, out float x);
        float.TryParse(adaptor.editableParts[1].text, out float y);
        float.TryParse(adaptor.editableParts[2].text, out float z);
        model.targetGameObject.localPosition = new Vector3(x, y, z);
    }

    private void SetPosition(int axis, string value)
    {
        if (!model.active) return;

        float.TryParse(value, out float val);

        Vector3 currLocalPos = targetGameObject.localPosition;
        currLocalPos[axis] = val;
        targetGameObject.localPosition = currLocalPos;
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
            targetGameObject.OnLocalPositionUpdated += UpdateUIPositionData;
            UpdateUIPositionData(targetGameObject.localPosition);
        }
        else
        {
            targetGameObject.OnLocalPositionUpdated -= UpdateUIPositionData;
            Vector3 position = model.targetGameObject.localPosition;
            for (int i = 0; i != 3; ++i)
                model.inputFields[i].text = position[i].ToString("f2");
        }
    }

    private void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // modify the callback when indicator changed
        if (oldIndicator != null)
            oldIndicator.model.OnLocalPositionUpdated -= UpdateUIPositionData;
        if (newIndicator != null)
            newIndicator.model.OnLocalPositionUpdated += UpdateUIPositionData;
    }


    private void UpdateUIPositionData(Vector3 pos)
    {
        if (!model.active) return;
        for (int i = 0; i != 3; ++i)
            model.inputFields[i].text = pos[i].ToString("f2");
    }
}

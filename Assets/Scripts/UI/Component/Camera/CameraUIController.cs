using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIController : ComponentController
{
    private new CameraUIModel model = null;
    private CodeSnippetInputAdaptor adaptor = null;
    private CameraModel targetGO = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as CameraUIModel;
    }

    public override void Init()
    {
        base.Init();

        // Set binding code snippet 
        adaptor = new CodeSnippetInputAdaptor();
        if (model == null)
            Debug.Log("camera model is null");
        if (model.inputFields == null)
            Debug.Log("camera input fields is null");
        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int channel = i;
            adaptor.BindValueChangedEvent((val) => SetCamera(channel, val));
        }
        CodeSnippetManager.Instance.BindSnippetAdaptor(adaptor);

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int channel = i; // to fix the c# closure problem
            InputField inputField = model.inputFields[i];
            this.model.inputFields[i].onEndEdit.AddListener((val) => SetCamera(channel, val));
            this.model.inputFields[i].onEndEdit.AddListener(val => adaptor.editableParts[channel].text = val);
        }

        model.OnActiveUpdated += (active => adaptor.editableParts.ForEach(inputField => inputField.interactable = active));
    }

    public override void InitComponent()
    {
        base.InitComponent();
        targetGO = model.targetGameObject as CameraModel;

        float.TryParse(adaptor.editableParts[0].text, out float fov);
        float.TryParse(adaptor.editableParts[1].text, out float near);
        float.TryParse(adaptor.editableParts[2].text, out float far);

        targetGO.fov = fov;
        targetGO.nearClipping = near;
        targetGO.farClipPlane = far;

        model.inputFields[0].text = fov.ToString("f2");
        model.inputFields[1].text = near.ToString("f2");
        model.inputFields[2].text = far.ToString("f2");
    }

    // 0 -> fov, 1 -> near, 2 -> far
    private void SetCamera(int channel, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);
        if (channel == 0)
            targetGO.fov = val;
        else if (channel == 1)
            targetGO.nearClipping = val;
        else if (channel == 1)
            targetGO.farClipPlane = val;
    }

}

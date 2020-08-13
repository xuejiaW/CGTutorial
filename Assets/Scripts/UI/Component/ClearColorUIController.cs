using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorUIController : DisplayableEntityController
{
    private new ClearColorModel model = null;
    private CodeEditablePartAdaptor adaptor = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ClearColorModel;
    }

    public override void Init()
    {
        base.Init();

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int channel = i; // to fix the c# closure problem
            this.model.inputFields[i].onValueChanged.AddListener((val) => UpdateCameraClearColor(channel, val));
            this.model.inputFields[i].onValueChanged.AddListener((val) => UpdateCodeSnippet(channel, val));
        }

        // Code -> component
        adaptor = new CodeEditablePartAdaptor();
        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int channel = i;
            adaptor.BindValueChangedEvent((val) => UpdateClearColorUIComponent(channel, val));
        }
        CodeSnippetManager.Instance.BindSnippetAdaptor(adaptor);
    }

    private void UpdateCameraClearColor(int channel, string value)
    {
        float.TryParse(value, out float val);

        Color backgroundColor = MainManager.Instance.viewCamera.backgroundColor;
        backgroundColor[channel] = val / 255.0f;
        MainManager.Instance.viewCamera.backgroundColor = backgroundColor;
    }

    private void UpdateClearColorUIComponent(int channel, string value)
    {
        float.TryParse(value, out float val);
        model.inputFields[channel].text = (val * 255.0f).ToString();
    }

    public void UpdateCodeSnippet(int channel, string value)
    {
        float.TryParse(value, out float val);
        adaptor.editableParts[channel].text = (val / 255.0f).ToString();
    }
}

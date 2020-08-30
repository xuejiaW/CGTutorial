using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeSnippetView
{
    public CodeSnippetModel model = null;
    public CodeSnippetController controller = null;

    public List<InputField> inputFields = null;

    public virtual void Switch(bool on)
    {
        inputFields.ForEach(inputField => inputField.interactable = on);
        model.isOn = on;
        if (on)
            viewUpdater?.RegisterEvent();
        else
            viewUpdater?.UnRegisterEvent();
    }

    public virtual void InitCodeSnippet()
    {
        viewUpdater?.SetTargetView(this);
        viewUpdater?.SetTargetModel(model.targetGameObject);
        viewUpdater?.RegisterEvent();

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i;
            inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateModelProperty(channel, val));
            this.controller.UpdateModelProperty(channel, inputFields[i].text); // using the code to init
        }
    }

    private UpdateViewBase _viewUpdater = null;
    protected UpdateViewBase viewUpdater
    {
        get
        {
            if (_viewUpdater == null)
                _viewUpdater = GetViewUpdater();
            return _viewUpdater;
        }
    }

    public virtual UpdateViewBase GetViewUpdater()
    {
        return null;
    }
}

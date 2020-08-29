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
    }

    public virtual void InitCodeSnippet()
    {
        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i;
            inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateModelProperty(channel, val));
            this.controller.UpdateModelProperty(channel, inputFields[i].text); // using the code to init
        }

        viewUpdater?.SetTargetView(this);
    }

    private IUpdateView _viewUpdater = null;
    private IUpdateView viewUpdater
    {
        get
        {
            if (_viewUpdater == null)
                _viewUpdater = GetViewUpdater();
            return _viewUpdater;
        }
    }

    public virtual IUpdateView GetViewUpdater()
    {
        return null;
    }
}

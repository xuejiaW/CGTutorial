using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeSnippetView
{
    public CodeSnippetModel model = null;
    public CodeSnippetController controller = null;

    public List<InputField> snippetInputsList = null;

    public virtual void Switch(bool on)
    {
        snippetInputsList.ForEach(inputField => inputField.interactable = on);
        model.isOn = on;
    }

    public virtual void InitCodeSnippet()
    {
        for (int i = 0; i != snippetInputsList.Count; ++i)
        {
            int channel = i;
            snippetInputsList[i].onEndEdit.AddListener((val) => this.controller.UpdateModelProperty(channel, val));
            this.controller.UpdateModelProperty(channel, snippetInputsList[i].text); // using the code to init
        }
    }
}

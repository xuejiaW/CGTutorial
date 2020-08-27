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

    public virtual void InitCodeSnippet() { }
}

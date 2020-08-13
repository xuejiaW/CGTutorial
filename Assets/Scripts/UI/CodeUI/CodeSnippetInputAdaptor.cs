using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeSnippetInputAdaptor
{
    public int dataCount { get; private set; }

    [System.NonSerialized]
    public List<System.Action<string>> inputValChangedEvenetList = new List<System.Action<string>>();
    [System.NonSerialized]
    public List<InputField> editableParts = new List<InputField>();

    // code value changed -> trigger event
    public void BindValueChangedEvent(params Action<string>[] eventList)
    {
        inputValChangedEvenetList.AddRange(eventList);
        dataCount = inputValChangedEvenetList.Count;
    }

    // other component value changed -> update code
    public void BindSnippetEditableFields(List<InputField> inputFields)
    {
        editableParts = inputFields;
        for (int i = 0; i != editableParts.Count; ++i)
        {
            int index = i;
            editableParts[i].onEndEdit.AddListener((val => inputValChangedEvenetList[index](val)));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSnippetController
{
    public CodeSnippetModel model = null;
    public virtual void InitCodeSnippet()
    {
        modelUpdater.SetTargetModel(model.targetGameObject);
    }

    private UpdateModelPropertyBase _modelUpdater;
    public UpdateModelPropertyBase modelUpdater
    {
        get
        {
            if (_modelUpdater == null)
                _modelUpdater = GetModelUpdater();
            return _modelUpdater;
        }
    }
    public virtual UpdateModelPropertyBase GetModelUpdater() { return null; }

    public virtual void UpdateModelProperty(int channel, string val)
    {
        modelUpdater.UpdateModelProperty(channel, val);
    }

}

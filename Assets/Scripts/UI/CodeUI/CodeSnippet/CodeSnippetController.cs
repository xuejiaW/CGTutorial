using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSnippetController
{
    public CodeSnippetModel model = null;
    public virtual void InitCodeSnippet() { }

    private IUpdateModelProperty _modelUpdater;
    public IUpdateModelProperty modelUpdater
    {
        get
        {
            if (_modelUpdater == null)
                _modelUpdater = GetModelUpdater();
            return _modelUpdater;
        }
    }
    public virtual IUpdateModelProperty GetModelUpdater() { return null; }

    public virtual void UpdateModelProperty(int channel, string val)
    {
        modelUpdater.UpdateModelProperty(model.targetGameObject, channel, val);
    }

}

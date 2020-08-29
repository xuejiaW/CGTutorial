using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentView : EntityView
{
    public List<InputField> inputFields = null;

    public new ComponentModel model = null;
    public new ComponentController controller = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ComponentModel;
        inputFields = GetComponentsInChildren<InputField>().ToList();
    }

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as ComponentController;

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i; // to fix the c# closure problem
            this.inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateModelProperty(channel, val));
        }
    }

    public virtual void InitComponent()
    {
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

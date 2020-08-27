using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : ComponentView
{
    public new ClearColorUIController controller = null;
    private ClearColorModel targetModel = null;
    public IUpdateComponent<Color> componentUpdater = null;

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as ClearColorUIController;

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i; // to fix the c# closure problem
            this.inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateClearColor(channel, val));
        }
    }

    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as ClearColorModel;
        componentUpdater = new ColorComponentUpdater();
        componentUpdater.SetTargetInputFields(inputFields);
        targetModel.OnClearColorChanged += (color => componentUpdater.UpdateComponent(color));
    }

}

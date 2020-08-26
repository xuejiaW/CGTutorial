using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : ComponentView
{
    public new ClearColorUIController controller = null;
    private ClearColorModel targetModel = null;

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as ClearColorUIController;

        for (int i = 0; i != inputFields.Length; ++i)
        {
            int channel = i; // to fix the c# closure problem
            this.inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateCameraClearColor(channel, val));
        }
    }

    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as ClearColorModel;
        targetModel.OnClearColorChanged += UpdateClearColorUIComponent;
    }

    private void UpdateClearColorUIComponent(Color color)
    {
        for (int i = 0; i != 3; ++i)
        {
            inputFields[i].text = (color[i]).ToString();
        }
    }
}

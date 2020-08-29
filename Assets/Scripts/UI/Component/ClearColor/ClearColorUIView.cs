using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : ComponentView
{
    private ClearColorModel targetModel = null;
    public IUpdateComponent<Color> componentUpdater = null;

    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as ClearColorModel;
        componentUpdater = new ColorComponentUpdater();
        componentUpdater.SetTargetInputFields(inputFields);
        targetModel.OnClearColorChanged += (color => componentUpdater.UpdateComponent(color));
    }
}

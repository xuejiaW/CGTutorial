using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransformUIModel : ComponentModel
{
    private new CameraTransformUIController controller = null;

    public InputField[] inputFields = null;

    public override void BindEntityController(EntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as CameraTransformUIController;
    }
}

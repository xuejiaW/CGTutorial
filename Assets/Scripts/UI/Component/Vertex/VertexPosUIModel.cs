using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIModel : ComponentModel
{
    private new VertexPosUIController controller = null;

    public InputField[] inputFields = null;


    public override void BindEntityController(EntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as VertexPosUIController;
    }
}

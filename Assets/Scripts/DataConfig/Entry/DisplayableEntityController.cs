using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayableEntityController : EntityController
{
    public new DisplayableEntityModel model;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as DisplayableEntityModel;
    }
}

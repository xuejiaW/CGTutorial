using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController
{
    public EntityModel model { get; protected set; }
    public virtual void BindEntityModel(EntityModel model) { this.model = model; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityModel
{
    public string assetID;

    public virtual System.Type GetViewType() { return typeof(EntityView); }
    public virtual System.Type GetControllerType() { return typeof(EntityController); }

    public EntityController controller { get; protected set; }
    public virtual void BindEntityController(EntityController controller) { this.controller = controller; }

    public virtual void Init() { }
}
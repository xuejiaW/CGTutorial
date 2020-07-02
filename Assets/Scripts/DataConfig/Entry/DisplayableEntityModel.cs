using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayableEntityModel : EntityModel
{
    public virtual System.Type GetViewType() { return typeof(EntityView); }

    public string prefabPath;

    public EntityView view { get; protected set; }
    public virtual void BindEntityView(EntityView view) { this.view = view; }
}

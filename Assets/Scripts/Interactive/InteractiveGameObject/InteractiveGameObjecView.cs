using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectView : EntityView
{
    protected override void Awake()
    {
        base.Awake();
        InteractiveGameObjectCollection.Instance.AddInteractiveGo(this);
    }

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
    }


}

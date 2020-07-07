using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectView : EntityView
{
    private InteractiveGameObjectModel goModel;
    protected override void Awake()
    {
        base.Awake();
        InteractiveGameObjectCollection.Instance.AddInteractiveGo(this);
    }

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        goModel = model as InteractiveGameObjectModel;
        goModel.OnLocalPositionUpdated += (localPos) => transform.localPosition = localPos;
        goModel.OnPositionUpdated += (pos) => transform.position = pos;
    }


}

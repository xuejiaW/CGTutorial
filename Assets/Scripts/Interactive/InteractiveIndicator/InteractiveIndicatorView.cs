using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorView : EntityView
{
    //Awake will be called after gameobject is instantiated while Start will not
    protected override void Awake()
    {
        base.Awake();
        thisGameObject.SetActive(false);
    }
}

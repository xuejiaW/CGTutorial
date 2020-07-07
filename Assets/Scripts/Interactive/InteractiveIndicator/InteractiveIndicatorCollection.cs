﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    // private Dictionary<InteractiveMethod, string> stateIndicatorPrefabDict = null;

    private InteractiveIndicatorController[] indicatorArray = null;
    public InteractiveIndicatorController this[InteractiveMethod state]
    {
        get
        {
            int index = (int)state;
            return indicatorArray[index];
        }
    }

    public InteractiveIndicatorController currentIndicator { get; private set; }

    public override void Init()
    {
        base.Init();

        indicatorArray = new InteractiveIndicatorController[3]
        {
            (GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_moving")
                                as InteractiveIndicatorController).SetHandle(new IndicatorMovingHandle()),
            (GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_rotating")
                                as InteractiveIndicatorController).SetHandle(new IndicatorRotatingHandle()),
            (GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_scaling")
                                as InteractiveIndicatorController).SetHandle(new IndicatorScalingHandle()),
        };

        InteractiveManager.Instance.OnInteractMethodUpdated += OnInteractiveStateUpdated;
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnHoldingInteractiveGOUpdated;
    }

    private void OnHoldingInteractiveGOUpdated(InteractiveGameObjectView oldInteractiveGo, InteractiveGameObjectView newInteractiveGO)
    {
        currentIndicator = this[InteractiveManager.Instance.interactMethod];

        currentIndicator.RemoveChild(oldInteractiveGo);
        currentIndicator.AddChild(newInteractiveGO);
    }

    public void OnInteractiveStateUpdated(InteractiveMethod state)
    {
        //old indicator
        currentIndicator?.RemoveChild(InteractiveGameObjectCollection.Instance.holdingInteractiveGo);
        currentIndicator?.AddChild(null);

        //new indicator
        currentIndicator = this[state];
        currentIndicator.AddChild(InteractiveGameObjectCollection.Instance.holdingInteractiveGo);
    }

    public void OnClickIndicator(GameObject Go)
    {
        string goName = Go.name;

        if (goName.IndexOf("Axis") != -1)
            currentIndicator.ClickIndicatorAxis(goName);
    }

    public void OnDragDeltaIndicator(Vector3 deltaPos)
    {
        currentIndicator.DragDeltaIndicatorAxis(deltaPos);
    }
}

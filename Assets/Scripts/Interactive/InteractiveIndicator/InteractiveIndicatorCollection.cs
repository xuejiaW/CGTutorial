using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    // private Dictionary<InteractiveMethod, string> stateIndicatorPrefabDict = null;

    private InteractiveIndicatorView[] indicatorArray = null;
    public InteractiveIndicatorView this[InteractiveMethod state]
    {
        get
        {
            int index = (int)state;
            return indicatorArray[index];
        }
    }

    private InteractiveIndicatorView currentIndicator = null;

    public override void Init()
    {
        base.Init();

        indicatorArray = new InteractiveIndicatorView[3]
        {
            (GameResourceManager.Instance.CreateEntityView<InteractiveIndicatorModel>("indicator_moving") as InteractiveIndicatorView).SetHandle(new IndicatorMovingHandle()),
            (GameResourceManager.Instance.CreateEntityView<InteractiveIndicatorModel>("indicator_rotating") as InteractiveIndicatorView).SetHandle(new IndicatorRotatingHandle()),
            (GameResourceManager.Instance.CreateEntityView<InteractiveIndicatorModel>("indicator_scaling") as InteractiveIndicatorView).SetHandle(new IndicatorScalingHandle()),
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

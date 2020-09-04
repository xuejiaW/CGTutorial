using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    private List<InteractiveIndicatorController> indicatorArray = null;
    public InteractiveIndicatorController this[InteractiveMethod state]
    {
        get
        {
            int index = (int)state;
            return indicatorArray[index];
        }
    }

    private InteractiveIndicatorController _currentIndicator = null;
    public InteractiveIndicatorController currentIndicator
    {
        get { return _currentIndicator; }
        private set
        {
            if (_currentIndicator != value)
                OnIndicatorChanged?.Invoke(_currentIndicator, value);
            _currentIndicator = value;
        }
    }
    public event System.Action<InteractiveIndicatorController, InteractiveIndicatorController> OnIndicatorChanged = null;

    public float indicatorSize { get; private set; }
    public float indicatorSensitive { get; private set; }

    public override void Init()
    {
        base.Init();

        InteractiveManager.Instance.OnInteractMethodUpdated += OnInteractiveStateUpdated;
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGOUpdated;
    }

    protected override void InitProcess()
    {
        base.InitProcess();
        indicatorArray = new List<InteractiveIndicatorController>();
        indicatorArray.Add((GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_moving")
                                as InteractiveIndicatorController).SetHandle(new IndicatorMovingHandle()));
        indicatorArray.Add((GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_rotating")
                                as InteractiveIndicatorController).SetHandle(new IndicatorRotatingHandle()));
        indicatorArray.Add((GameResourceManager.Instance.CreateEntityController<InteractiveIndicatorModel>("indicator_scaling")
                                as InteractiveIndicatorController).SetHandle(new IndicatorScalingHandle()));

        currentIndicator = this[InteractiveMethod.MOVING];
    }

    private void OnSelectedGOUpdated(DisplayableEntityModel oldInteractiveGo, DisplayableEntityModel newInteractiveGO)
    {
        currentIndicator = this[InteractiveManager.Instance.interactMethod];

        currentIndicator.RemoveChild(oldInteractiveGo);
        currentIndicator.AddChild(newInteractiveGO);

        UpdateIndicatorSize();

    }

    private void OnInteractiveStateUpdated(InteractiveMethod state)
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

    public void UpdateIndicatorSize()
    {
        if (InteractiveGameObjectCollection.Instance.holdingInteractiveGo == null)
            return;
        DisplayableEntityModel holdingGO = InteractiveGameObjectCollection.Instance.holdingInteractiveGo;

        Camera worldCamera = MainManager.Instance.worldCamera;
        float distance = Vector3.Distance(worldCamera.transform.position, holdingGO.position);
        float screenWidth_WorldSpace = Vector3.Distance(worldCamera.ViewportToWorldPoint(new Vector3(0.05f, 0, distance)), worldCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)));
        float size = screenWidth_WorldSpace;

        indicatorSize = size;

        indicatorArray.ForEach((indicator) =>
        {
            Transform[] verticesArr = indicator.model.view.GetComponentsInChildren<Transform>();
            for (int i = 0; i != verticesArr.Length; ++i)
            {
                if (verticesArr[i].name != "XAxis" && verticesArr[i].name != "YAxis"
                    && verticesArr[i].name != "ZAxis" && verticesArr[i].name != "Center")
                    continue;

                verticesArr[i].localScale = new Vector3(size, size, size);
            }

        });
    }

    public void SetIndicatorSensitive(float sensitive)
    {
        indicatorSensitive = sensitive;
    }

    ~InteractiveIndicatorCollection()
    {
        InteractiveManager.Instance.OnInteractMethodUpdated -= OnInteractiveStateUpdated;
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated -= OnSelectedGOUpdated;
    }
}

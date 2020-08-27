using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesAdaptor : MonobehaviorSingleton<CoursesAdaptor>
{

    private Transform creatorParent = null;
    public CoursesModel currentCourse { get; private set; }
    protected override void Init()
    {
        base.Init();
        creatorParent = GameObject.Find("UI/CreateGOUI").transform;
        currentCourse = new CoursesModel() { assetID = CoursesManager.Instance.currentCourse };
        GameResourceManager.Instance.LoadConfigData(currentCourse);

        SetCourse(currentCourse);
    }

    private void SetCourse(CoursesModel courseModel)
    {
        // InteractiveGameObjectInstantiator.Instance.InstantiateGameObject(currentCourse);
        StartCoroutine(waitAFrame2Instantiate());

        //Flythrough setting
        if (courseModel.allowCameraFlythrough)
            InteractiveManager.Instance.RegisterFlythroughMode();
        else
            InteractiveManager.Instance.UnRegisterFlythroughMode();

        if (courseModel.allowMultiInteractiveMethod)
            InteractiveManager.Instance.RegisterInteractiveMethodSwitch();
        else
            InteractiveManager.Instance.UnRegisterInteractiveMethodSwitch();

        InteractiveIndicatorCollection.Instance.SetIndicatorSize(courseModel.indicatorSize);
        InteractiveIndicatorCollection.Instance.SetIndicatorSensitive(courseModel.indicatorSensitive);

        Camera worldCamera = MainManager.Instance.worldCamera;
        Camera screenCamera = MainManager.Instance.screenCamera;

        worldCamera.orthographic = courseModel.cameraOrthographic;
        screenCamera.orthographic = courseModel.cameraOrthographic;
        if (worldCamera.orthographic)
        {
            worldCamera.orthographicSize = 1.0f;
            screenCamera.orthographicSize = 1.0f;
        }
    }

    private IEnumerator waitAFrame2Instantiate()
    {
        yield return null;
        InteractiveGameObjectInstantiator.Instance.InstantiateGameObject(currentCourse);
        currentCourse.createComponentAssetID.ForEach((component) => ComponentUIManager.Instance.CreateComponent(component, null, false));
    }
}

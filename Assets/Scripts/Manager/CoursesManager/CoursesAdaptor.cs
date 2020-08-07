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
        DisplayableEntityController courseRelatedCreator = GameResourceManager.Instance.
                            CreateEntityController<DisplayableEntityModel>(courseModel.createUIAssertID) as DisplayableEntityController;
        courseRelatedCreator.model.view.transform.SetParent(creatorParent, false);

        //Flythrough setting
        if (courseModel.allowCameraFlythrough)
            InteractiveManager.Instance.RegisterFlythroughMode();
        else
            InteractiveManager.Instance.UnRegisterFlythroughMode();

        // interactive method switch
        if (!courseModel.allowTranslate)
            InteractiveManager.Instance.RegisterFlythroughMode();
        else
            InteractiveManager.Instance.UnRegisterFlythroughMode();

        InteractiveIndicatorCollection.Instance.SetIndicatorSize(courseModel.indicatorSize);
        InteractiveIndicatorCollection.Instance.SetIndicatorSensitive(courseModel.indicatorSensitive);

        Camera targetCamera = MainManager.Instance.viewCamera;
        targetCamera.orthographic = courseModel.cameraOrthographic;
        if (targetCamera.orthographic)
        {
            targetCamera.orthographicSize = 1.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesAdaptor : MonobehaviorSingleton<CoursesAdaptor>
{
    private Transform creatorParent = null;
    protected override void Init()
    {
        base.Init();
        creatorParent = GameObject.Find("UI/CreateGOUI").transform;
        CoursesModel courseModel = new CoursesModel() { assetID = CoursesManager.Instance.currentCourse };
        GameResourceManager.Instance.LoadConfigData(courseModel);

        SetCourse(courseModel);
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
        if (!courseModel.onlySupportTranlate)
            InteractiveManager.Instance.RegisterFlythroughMode();
        else
            InteractiveManager.Instance.UnRegisterFlythroughMode();


        Camera targetCamera = MainManager.Instance.viewCamera;
        targetCamera.orthographic = courseModel.cameraOrthographic;
        if (targetCamera.orthographic)
            targetCamera.orthographicSize = 1.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformView : EntityView
{
    private EntityView m_targetGameObjectView = null;
    public EntityView targetGameObjectView
    {
        get
        {
            // while GO is the child of indicator, moving indicator
            if (InteractiveIndicatorCollection.Instance.currentIndicator != null)
                return InteractiveIndicatorCollection.Instance.currentIndicator;
            else
                return m_targetGameObjectView;
        }
        set
        {
            m_targetGameObjectView = value;
            UpdateTransformData();
        }
    }


    // 0-2 position | 3-5 rotation | 6-8 scaling
    private InputField[] inputFields = null;
    private void Start()
    {
        inputFields = GetComponentsInChildren<InputField>();
        for (int i = 0; i != inputFields.Length; ++i)
        {
            int axis = i % 3;
            if (i <= 2)
                inputFields[i].onValueChanged.AddListener((val) => SetPosition(axis, val));
            else if (i <= 5)
                inputFields[i].onValueChanged.AddListener((val) => SetRotation(axis, val));
            else if (i <= 8)
                inputFields[i].onValueChanged.AddListener((val) => SetScaling(axis, val));
        }
        Debug.Log("input fields is " + inputFields.Length);
    }

    public void SetPosition(int axis, string value)
    {
        Debug.Log("enter set position " + value);
        Debug.Assert(targetGameObjectView, "Error: Target View is empty");

        float.TryParse(value, out float val);
        Debug.Log("value is " + val);
        if (axis == 0)
            targetGameObjectView.transform.SetPositionX(val);
        else if (axis == 1)
            targetGameObjectView.transform.SetPositionY(val);
        else if (axis == 2)
            targetGameObjectView.transform.SetPositionZ(val);

    }

    public void SetRotation(int axis, string value)
    {
        Debug.Log("Rotation Axis is " + axis + " value is " + value);
    }

    public void SetScaling(int axis, string value)
    {
        Debug.Log("Scaling Axis is " + axis + " value is " + value);
    }

    private void UpdateTransformData()
    {
        Vector3 pos = targetGameObjectView.transform.localPosition;
        Vector3 eulerAngle = targetGameObjectView.transform.localEulerAngles;
        Vector3 size = targetGameObjectView.transform.localScale;
        inputFields[0].text = pos.x.ToString("f2");
        inputFields[1].text = pos.y.ToString("f2");
        inputFields[2].text = pos.z.ToString("f2");
    }
}

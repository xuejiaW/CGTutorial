using System.Collections.Generic;
using System;
using UnityEngine;

public partial class KeyboardInputManager : Singleton<KeyboardInputManager>, IMainUpdateObserver
{
    private float lastTime = 0.0f;
    private float currentTime = 0.0f;

    public void Update()
    {
        Debug.Log("enter update");
        currentTime = Time.time;
        foreach (KeyCode key in trackedKeyList)
        {
            if (Input.GetKeyDown(key) && keyDownHandlesDict.TryGetValue(key, out Action<KeyCode> downHandles))
            {
                lastTime = Time.time;
                downHandles.Invoke(key);
            }

            if (Input.GetKey(key) && keyHandlesDict.TryGetValue(key, out Action<float> handles))
            {
                handles.Invoke(currentTime - lastTime);
                lastTime = currentTime;
            }

            if (Input.GetKeyUp(key) && keyUpHandlesDict.TryGetValue(key, out Action<KeyCode> upHandles))
            {
                upHandles.Invoke(key);
            }
        }
    }
}
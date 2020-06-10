using System.Collections.Generic;
using System;
using UnityEngine;

public partial class KeyboardInputManager : Singleton<KeyboardInputManager>, IMainUpdateObserver
{
    private Dictionary<KeyCode, int> trackedKeyDownList = null;
    private Dictionary<KeyCode, int> trackedKeyList = null;
    private Dictionary<KeyCode, int> trackedKeyUpList = null;

    private float lastTime = 0.0f;
    private float currentTime = 0.0f;
    private float deltaTime = 0.0f;

    public void Update()
    {
        currentTime = Time.time;

        foreach (KeyValuePair<KeyCode, int> pair in trackedKeyDownList)
        {
            KeyCode key = pair.Key;
            if (Input.GetKeyDown(key) && keyDownHandlesDict.TryGetValue(key, out Action<KeyCode> downHandles))
            {
                downHandles.Invoke(key);
            }
        }

        foreach (KeyValuePair<KeyCode, int> pair in trackedKeyDownList)
        {
            KeyCode key = pair.Key;
            if (Input.GetKey(key) && keyHandlesDict.TryGetValue(key, out Action<float> handles))
            {
                deltaTime = currentTime - lastTime;
                lastTime = currentTime;

                if (deltaTime > 0.05f) return;  //Discard long interval data

                handles.Invoke(deltaTime);
            }
        }

        foreach (KeyValuePair<KeyCode, int> pair in trackedKeyDownList)
        {
            KeyCode key = pair.Key;
            if (Input.GetKeyUp(key) && keyUpHandlesDict.TryGetValue(key, out Action<KeyCode> upHandles))
            {
                upHandles.Invoke(key);
            }
        }
    }
}
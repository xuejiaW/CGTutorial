using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class KeyboardInputManager : Singleton<KeyboardInputManager>, IMainUpdateObserver, IInputManager
{
    private Dictionary<KeyCode, Action<KeyCode>> keyDownHandlesDict = null;
    private Dictionary<KeyCode, Action<float>> keyPressHandlesDict = null;
    private Dictionary<KeyCode, Action<KeyCode>> keyUpHandlesDict = null;


    protected override void InitProcess()
    {
        base.InitProcess();
        MainManager.Instance.RegisterObserver(this);

        keyDownHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();
        keyPressHandlesDict = new Dictionary<KeyCode, Action<float>>();
        keyUpHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();

        trackedKeyDownList = new Dictionary<KeyCode, int>();
        trackedKeyList = new Dictionary<KeyCode, int>();
        trackedKeyUpList = new Dictionary<KeyCode, int>();
    }

    public void RegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyDownHandlesDict, trackedKeyDownList, handle, key);
    }

    public void RegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyPressHandlesDict, trackedKeyList, handle, key);
    }

    public void RegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyUpHandlesDict, trackedKeyUpList, handle, key);
    }

    public void UnRegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyDownHandlesDict, trackedKeyDownList, handle, key);
    }

    public void UnRegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyPressHandlesDict, trackedKeyList, handle, key);
    }

    public void UnRegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyUpHandlesDict, trackedKeyUpList, handle, key);
    }
}
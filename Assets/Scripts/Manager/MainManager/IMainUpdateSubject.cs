using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainUpdateSubject
{
    void RegisterObserver(IMainUpdateObserver observer);
    void UnregisterObserver(IMainUpdateObserver observer);
    void Update();
}
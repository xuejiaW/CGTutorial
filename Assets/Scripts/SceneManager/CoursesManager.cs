using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: make async load scene
public class CoursesManager : MonoBehaviour
{
    public void OnClickCourseTriangle()
    {
        SceneManager.LoadScene("MainScene");
    }
}

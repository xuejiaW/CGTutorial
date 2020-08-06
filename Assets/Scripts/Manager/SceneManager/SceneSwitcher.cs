using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: make async load scene
public class SceneSwitcher : MonoBehaviour
{
    public void OnClickCourseTriangle(string course)
    {
        SceneManager.LoadScene("MainScene");
        CoursesManager.Instance.currentCourse = course;
    }
}

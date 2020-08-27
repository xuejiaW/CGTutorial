using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void OnClickCourseTriangle(string course)
    {
        SceneManager.LoadScene("MainScene");
        CoursesManager.Instance.currentCourse = course;
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene("CoursesScene");
    }
}

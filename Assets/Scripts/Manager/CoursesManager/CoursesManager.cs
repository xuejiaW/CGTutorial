using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesManager : Singleton<CoursesManager>
{
    // private string _currentCourse = "course_window";
    // private string _currentCourse = "course_triangles";
    // private string _currentCourse = "course_texture";
    // private string _currentCourse = "course_transformation";
    private string _currentCourse = "course_camera";
    public string currentCourse
    {
        get { return _currentCourse; }
        set { _currentCourse = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesManager : Singleton<CoursesManager>
{
    // private string _currentCourse = "course_window";
    private string _currentCourse = "course_triangles";
    // private string _currentCourse = "course_texture";
    public string currentCourse
    {
        get { return _currentCourse; }
        set { _currentCourse = value; }
    }
}

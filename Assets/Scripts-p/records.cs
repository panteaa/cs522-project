using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class records : MonoBehaviour
{

    //  Material mat1,mat2,mat3,mat4,mat5,mat6,mat7;


    void Start()
    {
        //  mat1 = Resources.Load("1", typeof(Material)) as Material;
        //   mat2 = Resources.Load("2", typeof(Material)) as Material;
        //   mat3 = Resources.Load("3", typeof(Material)) as Material;
        //  mat4 = Resources.Load("4", typeof(Material)) as Material;
        //   mat5 = Resources.Load("5", typeof(Material)) as Material;
        //   mat6 = Resources.Load("6", typeof(Material)) as Material;
        //   mat7 = Resources.Load("7", typeof(Material)) as Material;

    }

    void Update()
    {
    }

    public void OnSelectRight(Zinnia.Pointer.ObjectPointer.EventData data)
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        GameObject g;
        String n;
        if (data != null)
        {
            g = data.CollisionData.transform.gameObject;
            n = g.name;
            if (String.Equals(n, "Menu"))
            {
                SceneManager.LoadScene("1-menuitems");
            }
            if (String.Equals(n, "calendar"))
            {
                SceneManager.LoadScene("records1");
            }
            if (String.Equals(n, "total"))
            {
                SceneManager.LoadScene("records2");

            }
            if (String.Equals(n, "arms"))
            {
                SceneManager.LoadScene("records3");

            }
            if (String.Equals(n, "legs"))
            {
                SceneManager.LoadScene("records4");

            }

        }
    }

    public void quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

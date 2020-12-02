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

public class legs : MonoBehaviour
{
    public static bool startLegStopwatch;

    void Start()
    {
    }

    void Update()
    {
    }

    public void OnSelectRight(Zinnia.Pointer.ObjectPointer.EventData data)
    {

        GameObject g;
        String n;
        if (data != null)
        {
            g = data.CollisionData.transform.gameObject;
            n = g.name;
            if (String.Equals(n, "Easy"))
            {
                SceneManager.LoadScene("easyLeg");
            }
            else if (String.Equals(n, "Hard"))
            {
                startLegStopwatch = true;
                SceneManager.LoadScene("hardLeg");
            }
            else if (String.Equals(n, "Back"))
            {
                SceneManager.LoadScene("2-bodypartsMenu");
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

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

public class bodyParts : MonoBehaviour
{

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
            if (String.Equals(n, "Arms"))
            {
                SceneManager.LoadScene("3-armsMenu");
            }
            else if (String.Equals(n, "Legs"))
            {
                SceneManager.LoadScene("4-legsMenu");
            }
            else if (String.Equals(n, "Both"))
            {
                SceneManager.LoadScene("5-bothMenu");
            }
            else if (String.Equals(n, "Back"))
            {
                SceneManager.LoadScene("1-menuitems");
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

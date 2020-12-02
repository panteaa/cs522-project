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

public class menu : MonoBehaviour
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
            if (String.Equals(n, "NewSession"))
            {
                SceneManager.LoadScene("2-bodypartsMenu");
            }
            if (String.Equals(n, "Tutorial"))
            {
                SceneManager.LoadScene("tutorial1");
            }
            if (String.Equals(n, "Records"))
            {
                SceneManager.LoadScene("records1");
            }
            if (String.Equals(n, "Setting"))
            {
                SceneManager.LoadScene("setting");
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

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

public class hardLeg : MonoBehaviour
{
    float timeLeft;
    int[,] densityWidth;
    int densityIndex;
    int widthIndex;
    int numPresent;
    System.Random rand;
    int[] widths;
    int[] densities;
    Vector3 rb;
    Vector3 rt;
    Vector3 lt;
    Vector3 lb;

    GameObject cube;
    List<Vector3> targets;

    AudioSource correctAudioData;

    AudioSource errorAudioData;


    //file related
    StreamWriter outStream;
    string filePath;
    StringBuilder sb;
    string localDateTime;
    Stopwatch stopwatch;
    Stopwatch legStopwatch;


    int setCompleted;
    int currentDensity;
    int currentWidth;
    float completionTime;
    int errorCount;

    string objectSideStr;
    string handUsedStr;
    public static bool startLegStopwatch;
    public bool started;
    public bool showInfo;


    void Start()
    {
        started = false;
        showInfo = false;
        correctAudioData = GameObject.Find("correctAudio").GetComponent<AudioSource>();

        errorAudioData = GameObject.Find("errorAudio").GetComponent<AudioSource>();

        // file
        localDateTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
        sb = new StringBuilder();
        completionTime = 0;
        errorCount = 0;
        stopwatch = new Stopwatch();

        legStopwatch = new Stopwatch();
        Directory.CreateDirectory("./file");
        filePath = "./file/" + "actualtWalkTwo" + localDateTime + ".csv";
        outStream = System.IO.File.CreateText(filePath);


        sb.AppendLine("condition" + ',' + "objectSide" + ',' + "handUsed" + ',' + "blockNumber" + ',' + "density" + ',' + "width(cm)" + ',' + "completionTime(ms)" + ',' + "errorCount");
        print(sb.ToString());
        outStream.WriteLine(sb);
        sb.Length = 0;


        setCompleted = 0;

        densityWidth = new int[2, 3];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                densityWidth[i, j] = 1;
            }
        }


        cube = GameObject.Find("object-sample");
        targets = new List<Vector3>();
        densities = new int[2] { 10, 20 };
        densities = new int[2] { 20, 30 };
        widths = new int[3] { 1, 2, 3 };

        //rb = new Vector3(1, 0, (float)-0);
        //rt = new Vector3(1, 0, (float)6);
        //lt = new Vector3(-1, 0, (float)6);
        //lb = new Vector3(-1, 0, (float)-0);


        rb = new Vector3(1, 0, (float)-0);
        rt = new Vector3(1, 0, (float)12);
        lt = new Vector3(-1, 0, (float)12);
        lb = new Vector3(-1, 0, (float)-0);

        rand = new System.Random();

       // generateRandom();
    }


    void Update()
    {
        var input = Input.inputString;
        switch (input)
        {
            case "c":
                GameObject.Find("InfoParent").transform.GetChild(0).gameObject.SetActive(true);
                GameObject g = GameObject.Find("Info");
                Vector3 pos = GameObject.Find("HeadsetAlias").GetComponent<Transform>().position;
                g.transform.position = new Vector3(pos.x, pos.y, pos.z + 5);
                showInfo = true;
                break;
            case "v":
                GameObject.Find("InfoParent").transform.GetChild(0).gameObject.SetActive(false);
                showInfo = false;
                break;
            case "x":
                SceneManager.LoadScene("1-menuitems");
                break;
        }

        if (numPresent == 0)
        {
            setCompleted++;
            completionTime = stopwatch.ElapsedMilliseconds;
            file();
   //         generateRandom();
       //     legStopwatch.Stop();
        }

        startLegStopwatch = legs.startLegStopwatch;

        if (startLegStopwatch)
        {
            legStopwatch.Start();
            legs.startLegStopwatch = false;
            started = true;
        }

        if (showInfo && started)
        {
            //GameObject.Find("InfoText").GetComponent<TextMesh>().text = "x";
            float cPos = GameObject.Find("HeadsetAlias").GetComponent<Transform>().position.z;
            //  GameObject.Find("InfoText").GetComponent<TextMesh>().text = " Time passed: " + (totalStopwatch.ElapsedMilliseconds / 1000).ToString() + "\n Number of objects to be selected: " + (numPresent).ToString() + "\n Number of selected objects: " + (currentDensity - numPresent).ToString() + "\n Distance Left to walk: " + (20 - cPos).ToString() + "\n Distance travelled: " + (cPos).ToString();
            GameObject.Find("InfoText").GetComponent<TextMesh>().text = " Time passed: " + (legStopwatch.ElapsedMilliseconds / 1000).ToString() + "\n Distance Left to walk: " + (30 - cPos).ToString() + "\n Distance travelled: " + (cPos).ToString();
        }
    }


    public void file()
    {

        sb.AppendLine("actualWalkTwo" + ',' + objectSideStr + ',' + handUsedStr + ',' + setCompleted + ',' + currentDensity + ',' + currentWidth * 10 + ',' + completionTime + ',' + errorCount);

        outStream.WriteLine(sb);
        sb.Length = 0;

    }

    public void OnSelectRight(Zinnia.Pointer.ObjectPointer.EventData data)
    {

        GameObject g;
        if (data != null)
        {
            g = data.CollisionData.transform.gameObject;

            handUsedStr += "right;";
            if (g.transform.position.x > 0.5)
            {
                objectSideStr += "right;";
            }
            else
            {
                objectSideStr += "left;";
            }

            g.SetActive(false);
            numPresent = numPresent - 1;
            playCorrectAudio();

        }
        else
        {
            errorCount++;
            playErrorAudio();
        }
    }


    public void OnSelectLeft(Zinnia.Pointer.ObjectPointer.EventData data)
    {

        GameObject g;
        if (data != null)
        {
            g = data.CollisionData.transform.gameObject;

            handUsedStr += "left;";
            if (g.transform.position.x > 0.5)
            {
                objectSideStr += "right;";
            }
            else
            {
                objectSideStr += "left;";
            }

            g.SetActive(false);
            numPresent = numPresent - 1;
            playCorrectAudio();

        }
        else
        {
            errorCount++;
            playErrorAudio();
        }
    }

    void playCorrectAudio()
    {
        correctAudioData.Play(0);
    }

    void playErrorAudio()
    {
        errorAudioData.Play(0);
    }




    public void generateRandom()
    {

        print(setCompleted);

        if (setCompleted >= 6)
        {
            print("finished");
            outStream.Close();
            quit();
        }
        else
        {

            while (densityWidth[densityIndex, widthIndex] == 0)
            {
                densityIndex = UnityEngine.Random.Range(0, 2);
                widthIndex = UnityEngine.Random.Range(0, 3);
            }

            densityWidth[densityIndex, widthIndex] = 0;

            generateCubes(densityIndex, widthIndex);

        }

    }


    public void generateCubes(int i, int j)
    {
        objectSideStr = "";
        handUsedStr = "";

        for (int num = 0; num < densities[i] / 2; num++)
        {
            bool found = false;
            Vector3 newCube = new Vector3();
            while (!found)
            {
                double x = rand.NextDouble() * (3 - rb.x) + (rb.x);
                //double y = rand.NextDouble() * (2) + 0.5; 
                double y = rand.NextDouble() * (1) + 0.5;
                double z = rand.NextDouble() * (rt.z - rb.z) + (rb.z);

                newCube = new Vector3((float)x, (float)y, (float)z);

                found = !collisionDetector(newCube, (float)(widths[j] * 0.05));
            }

            GameObject go = Instantiate(cube, newCube, Quaternion.identity);
            go.transform.localScale = new Vector3((float)(widths[j] * 0.1), (float)(widths[j] * 0.1), (float)(widths[j] * 0.1));
            go.GetComponent<Renderer>().material.color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
            targets.Add(newCube);

        }

        for (int num = 0; num < densities[i] / 2; num++)
        {
            bool found = false;
            Vector3 newCube = new Vector3();
            while (!found)
            {
                double x = rand.NextDouble() * (lb.x - (-3)) + (-3);
                double y = rand.NextDouble() * (1) + 0.5;
                double z = rand.NextDouble() * (lt.z - lb.z) + (lb.z);

                newCube = new Vector3((float)x, (float)y, (float)z);

                found = !collisionDetector(newCube, (float)(widths[j] * 0.05));
            }

            GameObject go = Instantiate(cube, newCube, Quaternion.identity);
            go.transform.localScale = new Vector3((float)(widths[j] * 0.1), (float)(widths[j] * 0.1), (float)(widths[j] * 0.1));
            go.GetComponent<Renderer>().material.color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
            targets.Add(newCube);
        }


        numPresent = densities[i];
        currentDensity = densities[i];
        currentWidth = widths[j];
        completionTime = 0;
        errorCount = 0;
        stopwatch.Reset();
        stopwatch.Start();

    }

    public bool collisionDetector(Vector3 newTarget, float realWidth)
    {

        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(targets[i], newTarget) <= realWidth)
                return true;
        }
        return false;
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

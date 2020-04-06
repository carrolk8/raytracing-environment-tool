using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RouteLoader : MonoBehaviour
{
    public List<Vector3> loadRoute(string path)
    {
        var routeData = File.ReadAllLines(path);
        var routeDataList = new List<Vector3>();

        for(int i = 1; i < routeData.Length; i++)
        {
            var lineOfText = routeData[i].Split('\t');
            routeDataList.Add(new Vector3(Int32.Parse(lineOfText[1]), 0, Int32.Parse(lineOfText[2])));
        }

        return routeDataList;
    }
}

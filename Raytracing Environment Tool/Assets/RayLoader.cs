using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RayLoader : MonoBehaviour
{
    public List<RayGroup> LoadRays(string path)
    {
        var rayData = File.ReadAllLines(path);
        var rayDataList = new List<RayGroup>();
        
        for (int i = 0; i < rayData.Length; i++)
        {
            RayGroup rg = new RayGroup();
            var lineOfText = rayData[i].Split(' ');
            rg.Index = Int32.Parse(lineOfText[0]);
            rg.GroupSize = Int32.Parse(lineOfText[1]);

            for(int j = 1; j <= rg.GroupSize; j++)
            {
                var nextLine = rayData[j + i].Split(' ');
                var rt = new Raytrace(Int32.Parse(nextLine[0]),
                    new Vector3(float.Parse(nextLine[1]), 0, float.Parse(nextLine[2])),
                    new Vector3(float.Parse(nextLine[3]), 0, float.Parse(nextLine[4])),
                    new Vector3(float.Parse(nextLine[7]), 5, float.Parse(nextLine[8])),
                    new System.Numerics.Complex(Double.Parse(nextLine[9]), Double.Parse(nextLine[10])),
                    new System.Numerics.Complex(Double.Parse(nextLine[11]), Double.Parse(nextLine[12])),
                    new System.Numerics.Complex(Double.Parse(nextLine[13]), Double.Parse(nextLine[14])));

                rg.Rays.Add(rt);
            }
            i = i + rg.GroupSize;
            rayDataList.Add(rg);
        }

        GameObject transmitter = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        transmitter.transform.localScale = new Vector3(3f, 250f, 3f);
        transmitter.transform.position = new Vector3(rayDataList[0].Rays[0].StartLocation.x, 250, rayDataList[0].Rays[0].StartLocation.z);
        transmitter.GetComponent<Renderer>().material.color = Color.yellow;

        return rayDataList;
    }
}

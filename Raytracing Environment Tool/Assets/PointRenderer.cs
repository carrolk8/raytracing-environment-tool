using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRenderer : MonoBehaviour
{
    public List<GameObject> RenderPoints(RayGroup rg, List<GameObject> objs)
    {
        foreach(GameObject go in objs)
        {
            Destroy(go);
        }

        foreach(Raytrace r in rg.Rays)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = r.ReflectionPoint;
            obj.transform.localScale = new Vector3(3f, 3f, 3f);
            obj.GetComponent<Renderer>().material.color = new Color((float)r.XVector.Magnitude * 100000 ,
                 (float)r.YVector.Magnitude * 100000, 
                 (float)r.ZVector.Magnitude * 100000);
            obj.name = "RP" + objs.Count;

            objs.Add(obj);
        }
        return objs;
    }
}

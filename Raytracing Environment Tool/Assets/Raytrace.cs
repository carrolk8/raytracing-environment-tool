using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raytrace
{
    public int Type { get; set; }
    public Vector3 StartLocation { get; set; }
    public Vector3 EndLocation { get; set; }
    public Vector3 ReflectionPoint { get; set; }
    public System.Numerics.Complex XVector { get; set; }
    public System.Numerics.Complex YVector { get; set; }
    public System.Numerics.Complex ZVector { get; set; }

    public Raytrace(int type, Vector3 startLocation, Vector3 endLocation, Vector3 reflectionPoint, System.Numerics.Complex xVector, System.Numerics.Complex yVector, System.Numerics.Complex zVector)
    {
        Type = type;
        StartLocation = startLocation;
        EndLocation = endLocation;
        ReflectionPoint = reflectionPoint;
        XVector = xVector;
        YVector = yVector;
        ZVector = zVector;
    }
}

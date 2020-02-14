using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public int BuildingNumber {get; set;}
    public int BuildingHeight {get; set;}
    public List<Vector3> Vertices {get; set;}

    public Building()
    {
        this.Vertices = new List<Vector3>();
    }
}

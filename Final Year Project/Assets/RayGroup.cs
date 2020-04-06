using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGroup
{
    public int Index { get; set; }
    public int GroupSize { get; set; }
    public List<Raytrace> Rays { get; set; }

    public RayGroup()
    {
        this.Rays = new List<Raytrace>();
    }
}

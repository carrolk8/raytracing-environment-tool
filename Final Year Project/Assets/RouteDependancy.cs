using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteDependancy : MonoBehaviour
{
    public static RouteDependancy routeDependancy;
    private void Awake()
    {
        routeDependancy = this;
    }
}

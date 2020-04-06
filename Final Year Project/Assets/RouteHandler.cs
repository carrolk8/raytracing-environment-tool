using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteHandler : MonoBehaviour
{
    private static RouteHandler routeHandler;
    private List<Vector3> _route;
    private GameObject obj;
    private Vector3 target;
    private int currentElement = 1;


    private void Start()
    {
        routeHandler = this;
    }

    public GameObject StartMoving(List<Vector3> route)
    {
        int i = 1;
        float moveSpeed = 4.0f;
        obj = GameObject.Find("FirstPersonCharacter");
        //obj.GetComponent("Camera").transform.parent = obj.transform;
     
        _route = route;
        obj.transform.position = _route[0];
        //obj.AddComponent<RouteHandler>().StartCoroutine(TraverseRoute(route, moveSpeed, obj, i));

        return obj;
    }

    public void NextElement()
    {
        target = _route[currentElement + 1];
        currentElement++;
    }



    //private IEnumerator TraverseRoute(List<Vector3> route, float moveSpeed, GameObject obj, int i)
    //{
    //    while (true)
    //    {


    //        //Quaternion _lookRotation = Quaternion.LookRotation((route[i] - obj.transform.position).normalized);
    //        //obj.transform.position += _lookRotation * Vector3.forward * Time.deltaTime * 4.0f;


    //       // obj.transform.position = route[i];
    //      //  i++;
    //       // yield return new WaitForSeconds(1.5f);
    //    }
    //}

}

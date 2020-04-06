using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScript : MonoBehaviour
{
    private RouteLoader routeLoader = new RouteLoader();
    private RayLoader rayLoader = new RayLoader();
    private PointRenderer pointRenderer = new PointRenderer();
    private GameObject obj;
    private GameObject nestedObj;
    private List<GameObject> listOfPoints = new List<GameObject>();
    private List<RayGroup> rays = new List<RayGroup>();
    private List<Vector3> route;
    private Vector3 target;
    private int currentElement = 1;
    private bool mapExtendedFeatures = false;

    void Start()
    {
        //Path to map data
        string path = "Assets/MapData/building.txt";
        List<Building> listOfBuildings = new List<Building>();

        //Read file
        var buildingsData = File.ReadAllLines(path);

        if(buildingsData[0] == "1") //Determining if a map will have associated data
        {
            mapExtendedFeatures = true;
        }

        for (int i = 1; i < buildingsData.Length; i++)
        {
            Building building = new Building();
            int extraLines = 0;

            //Split line from map file into individual numbers
            var lineOfText = buildingsData[i].Split(' ');

            //Add Building Height and Number to Building object
            building.BuildingHeight = float.Parse(lineOfText[4]);
            building.BuildingNumber = Int32.Parse(lineOfText[5]);

            //Add in first point and it's counterpart at building height
            building.Vertices.Add(new Vector3(float.Parse(lineOfText[0]), 0, float.Parse(lineOfText[1])));

            //Add in second point and it's counterpart at building height
            building.Vertices.Add(new Vector3(float.Parse(lineOfText[2]), 0, float.Parse(lineOfText[3])));

            //Checking if next lines are part of same building, if they are, adding to building object.
            for (int j = i; j < buildingsData.Length; j++)
            {
                var nextLine = buildingsData[j].Split(' ');

                //Checking if points already exists in building object
                if (nextLine[5] == building.BuildingNumber.ToString())
                {
                    //Adding points if they're a part of the building
                    if (!building.Vertices.Contains(new Vector3(float.Parse(nextLine[0]), 0, float.Parse(nextLine[1]))))
                    {
                        building.Vertices.Add(new Vector3(float.Parse(nextLine[0]), 0, float.Parse(nextLine[1])));
                    }

                    if (!building.Vertices.Contains(new Vector3(float.Parse(nextLine[2]), 0, float.Parse(nextLine[3]))))
                    {
                        building.Vertices.Add(new Vector3(float.Parse(nextLine[2]), 0, float.Parse(nextLine[3])));
                    }

                    //Account for extra lines read
                    extraLines++;
                }
                else
                {
                    //Exit loop early if next line is not part of the building
                    break;
                }


            }
            listOfBuildings.Add(building);
            i = i + (extraLines - 1);
        }

        var material = new Material(Shader.Find("Standard"));
        foreach (var building in listOfBuildings)
        {
            for (int i = 0; i <= building.Vertices.Count - 1; i++)
            {
                GameObject obj = new GameObject("buildingB" + building.BuildingNumber + "F" + i);
                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                meshRenderer.sharedMaterial = material;

                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();

                Vector3[] vertices = new Vector3[4];

                if (i == (building.Vertices.Count - 1))
                {
                    vertices[0] = new Vector3(building.Vertices[i].x, building.Vertices[i].y, building.Vertices[i].z);
                    vertices[1] = new Vector3(building.Vertices[i].x, building.BuildingHeight, building.Vertices[i].z);
                    vertices[2] = new Vector3(building.Vertices[0].x, building.Vertices[0].y, building.Vertices[0].z);
                    vertices[3] = new Vector3(building.Vertices[0].x, building.BuildingHeight, building.Vertices[0].z);
                }
                else
                {
                    vertices[0] = new Vector3(building.Vertices[i].x, building.Vertices[i].y, building.Vertices[i].z);
                    vertices[1] = new Vector3(building.Vertices[i].x, building.BuildingHeight, building.Vertices[i].z);
                    vertices[2] = new Vector3(building.Vertices[i + 1].x, building.Vertices[i + 1].y, building.Vertices[i + 1].z);
                    vertices[3] = new Vector3(building.Vertices[i + 1].x, building.BuildingHeight, building.Vertices[i + 1].z);
                }
                mesh.vertices = vertices;

                int[] triangles = new int[6]
                {
                        1,3,0,
                        0,3,2
                };
                mesh.triangles = triangles;

                Vector3[] normals = new Vector3[4]
                {
                        -Vector3.forward,
                        -Vector3.forward,
                        -Vector3.forward,
                        -Vector3.forward
                };
                mesh.normals = normals;

                Vector2[] uv = new Vector2[4]
                {
                        new Vector2(0, 0),
                        new Vector2(0, 1),
                        new Vector2(1, 0),
                        new Vector2(1, 1)
                };
                mesh.uv = uv;

                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                meshFilter.mesh = mesh;
                meshCollider.sharedMesh = mesh;
            }
            renderRoof(building);
        }

        if (mapExtendedFeatures)
        {
            string pathForRoute = "Assets/MapData/route.txt";
            route = routeLoader.loadRoute(pathForRoute);

            string pathForRays = "Assets/MapData/rays.txt";
            rays = rayLoader.loadRays(pathForRays);



            obj = GameObject.Find("FPSController");
            nestedObj = GameObject.Find("FirstPersonCharacter");
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && mapExtendedFeatures)
        {
            obj.GetComponent<CharacterController>().enabled = false;

            obj.transform.position = target;
            nestedObj.transform.position = target;

            obj.GetComponent<CharacterController>().enabled = true;

            pointRenderer.RenderPoints(rays[currentElement], listOfPoints);
            NextElement();
        }
    }

    public void NextElement()
    {
        target = route[currentElement + 1];
        currentElement++;
    }

    void renderRoof(Building building)
    {
        var material = new Material(Shader.Find("Standard"));
        GameObject obj = new GameObject("buildingB" + building.BuildingNumber + "Roof");
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
        meshRenderer.sharedMaterial = material;

        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        List<Vector2> vertices2D = new List<Vector2>();
        foreach(var vert in building.Vertices)
        {
            vertices2D.Add(new Vector2(vert.x, vert.z));
        }

        Vector3[] vertices = new Vector3[vertices2D.Count];
        for(int i = 0; i < vertices2D.Count; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, building.BuildingHeight, vertices2D[i].y);
        }

        mesh.vertices = vertices;

        Triangulator triangulator = new Triangulator(vertices2D.ToArray());
        int[] triangles = triangulator.Triangulate();
        mesh.triangles = triangles;

        Vector3[] normals = new Vector3[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            normals[i] = -Vector3.forward;
        }
        mesh.normals = normals;


        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}

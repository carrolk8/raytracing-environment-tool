using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMap ()
    {
        //Path to map data
        string path = "Assets/MapData/building.txt";
        List<Building> listOfBuildings = new List<Building>();

        //Read file
        var buildingsData = File.ReadAllLines(path);

        for(int i = 0; i < buildingsData.Length; i++)
        {
            Building building = new Building();
            int extraLines = 0;

            //Split line from map file into individual numbers
            var lineOfText = buildingsData[i].Split(' ');

            //Add Building Height and Number to Building object
            building.BuildingHeight = Int32.Parse(lineOfText[5]);
            building.BuildingNumber = Int32.Parse(lineOfText[6]);

            //Add in first point and it's counterpart at building height
            building.Vertices.Add(new Vector3(Int32.Parse(lineOfText[1]), Int32.Parse(lineOfText[2]), 0));
            building.Vertices.Add(new Vector3(Int32.Parse(lineOfText[1]), Int32.Parse(lineOfText[2]), building.BuildingHeight));

            //Add in second point and it's counterpart at building height
            building.Vertices.Add(new Vector3(Int32.Parse(lineOfText[3]), Int32.Parse(lineOfText[4]), 0));
            building.Vertices.Add(new Vector3(Int32.Parse(lineOfText[3]), Int32.Parse(lineOfText[4]), building.BuildingHeight));

            //Checking if next lines are part of same building, if they are, adding to building object.
            for(int j=i;j<buildingsData.Length;j++)
            {
                var nextLine = buildingsData[j].Split(' ');

                //Checking if points already exists in building object
                if (nextLine[6] == building.BuildingNumber.ToString())
                {
                    //Adding points if they're a part of the building
                    if (!building.Vertices.Contains(new Vector3(Int32.Parse(nextLine[1]), Int32.Parse(nextLine[2]), 0)))
                    {
                        building.Vertices.Add(new Vector3(Int32.Parse(nextLine[1]), Int32.Parse(nextLine[2]), 0));
                        building.Vertices.Add(new Vector3(Int32.Parse(nextLine[1]), Int32.Parse(nextLine[2]), building.BuildingHeight));
                    }

                    if (!building.Vertices.Contains(new Vector3(Int32.Parse(nextLine[3]), Int32.Parse(nextLine[4]), 0)))
                    {
                        building.Vertices.Add(new Vector3(Int32.Parse(nextLine[3]), Int32.Parse(nextLine[4]), 0));
                        building.Vertices.Add(new Vector3(Int32.Parse(nextLine[3]), Int32.Parse(nextLine[4]), building.BuildingHeight));
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

        SceneManager.LoadScene(1);
    }
}

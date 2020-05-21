# Raytracing Environment Generation Tool

A tool for generating 3D environments for ray tracing applications.

## Getting Started

These steps will get a version of the project running on your machine. Development of this project was done exclusively on Windows 10.

### Prerequisites
````
Unity (2019.3 was used for this prject)
Visual Studio with Unity plugin
````

### Installing

Download or clone this project, and open it from Unity.

## Using the Tool

This tool can be used to generate an environment from a single file describing that environment, or from three files, describing the environment, a preplanned route for the user to follow and the ray trace data for every point along the route.

### Controls

The user can move around using the W,A,S and D keys or the arrow keys. The left shift button will allow the user to move around faster and the space button causes the user to jump.

If there is an associated route with an environment, holding the F key will advance through the route and the C button will reverse through it.

### Data inputs
#### Environment File

This file describes the environment to be generated. The very first line of the file determines if the environment will have an associated route and ray file, 1 if there are associated files, 0 if not. Each line of the file will then describe an edge to a block, as well as notating which block that edge relates to, and a height for that block.

The format of the file from line 2 onwards should be as follows, each number seperated by a single space:
````
x1 y1 x2 y2 height number x x
````
The first 4 numbers describe two points, describing a line. The 5th number is the height of the block, this should be the same for all lines relating to a block. The 6th line is the block number, denoting which lines describe which block. The final two numbers are not currently used, but exist in the files used included with the project. They should be able to be omitted.

Ensure there are no extra empty lines at the end of the file, as this may cause errors.

The final line of each block should have its finishing point be the starting point of the first line describing it, i.e. completing a polygon if it were to be drawn out.

The example below describes the first 3 blocks in an environment with associated data.
````
1
2379 3381 2379 3397 12 1 1 515
2379 3397 2363 3397 12 1 1 515
2363 3397 2360 3383 12 1 1 515
2360 3383 2379 3381 12 1 1 515
1091 3357 1110 3366 25 2 1 515
1110 3366 1144 3361 25 2 1 515
1144 3361 1144 3368 25 2 1 515
1144 3368 1170 3365 25 2 1 515
1170 3365 1170 3375 25 2 1 515
1170 3375 1158 3375 25 2 1 515
1158 3375 1159 3380 25 2 1 515
1159 3380 1110 3384 25 2 1 515
1110 3384 1095 3395 25 2 1 515
1095 3395 1089 3388 25 2 1 515
1089 3388 1103 3378 25 2 1 515
1103 3378 1102 3373 25 2 1 515
1102 3373 1087 3366 25 2 1 515
1087 3366 1091 3357 25 2 1 515
2039 3356 2041 3374 16 3 1 512
2041 3374 2044 3392 16 3 1 512
2044 3392 2033 3394 16 3 1 512
2033 3394 2030 3379 16 3 1 512
2030 3379 2028 3366 16 3 1 512
2028 3366 2018 3380 16 3 1 512
2018 3380 2009 3371 16 3 1 512
2009 3371 2020 3357 16 3 1 512
2020 3357 2039 3356 16 3 1 512
````


#### Route File

The format for the route is simple, each line contains the index and an x and z coordinate.

The only thing to point out here is that **each value is seperated by _tabs_**. If your file is seperated by spaces, it may actually be quicker to change the RouteLoader.cs file to split each line based on whitespace, rather than tabs.

For example
````
var lineOfText = routeData[i].Split('\t');

to 

var lineOfText = routeData[i].Split(' ');
````

An example of a route file is detailed below
````
1	613	687
2	607	703
3	596	708
4	586	713
5	576	719
6	564	724
7	553	730
8	540	736
9	529	742
10	515	748
````


#### Ray File

The format for this file is quite complex. Each line will be of one two types, either metadata describing how many rays there are in the next ray group, or describing a ray. An example is given below.

````
1 5 0 0 0 0 0 0 0 0 0 0 0 0 0
4 1281.36 1381.27 613 687 1043 1619 740 807 -7.80242e-012 1.85648e-008 -7.37236e-012 1.75416e-008 1.28633e-009 -3.06066e-006
4 1281.36 1381.27 613 687 981 1464 740 807 -1.51196e-008 -3.16261e-008 -1.42863e-008 -2.98829e-008 2.1454e-006 4.4876e-006
4 1281.36 1381.27 613 687 951 1386 740 807 1.56177e-008 8.39786e-009 1.47569e-008 7.93499e-009 -2.09517e-006 -1.1266e-006
4 1281.36 1381.27 613 687 939 1363 740 807 2.48145e-008 -8.34011e-009 2.34468e-008 -7.88042e-009 -3.28961e-006 1.10563e-006
4 1281.36 1381.27 613 687 931 1366 740 807 2.28796e-009 1.0042e-009 2.16185e-009 9.4885e-010 -3.05509e-007 -1.3409e-007
````

The first line is metadata describing the next five. The first value on the line is the index of the route the following rays are for. The second value is the amount of rays in that group. In this example, there are 5 rays for index 1 of the route.

The next five lines are descriptions of rays. The first value on each line tells us what type of ray it is, but this is not currently used in the program. The next four values are the positions of the transmitter and receiver. These are xy-coordinate pairs. The next two values (i.e (1043,1619) or (981,1464), positions 6 and 7) are penultimate points, which will not be visible to the user, so they are not used. The next 6 values are complex numbers describing the x, y and z components of the ray at that point.

The values in this file are again seperated by white space.


### Using own Files

To use your own data, you can change the path to each file in the EnvironmentBuilder.cs class.

The path for the environment file is located at the top of the class, simply called path. Place your file into the MapData folder in the project and change the path to reflect the name of your file.

If your data has associated route and ray data, repeat the process, but instead change the pathForRoute and pathForRays variables, which are located at the end of the start() function. These can easily be found by pressing CTRL + F and looking for either of the variable names or the 'if(mapExtendedFeatures)' line.

### Running the Tool

The tool can be run by either building the project, under 
````
File -> Build Settings -> Build and Run
````

Or, you can press the play button at the top of the screen. This is usually used for testing, and does not run as well as building the project, but will allow you to go into scene mode, by pressing ESC and clicking on the scene tab. This provides the user with a few tools to help validate the environment, such as being able to fly above and look at the entire environment.

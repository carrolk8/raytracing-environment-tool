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

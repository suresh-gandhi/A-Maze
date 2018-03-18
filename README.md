# A-Maze

Who doesn't like finding a route through a confusing network of intercommunicating paths and passages :blush:

This is a VR maze game made in Unity in which the user gets rewarded with treasure on finding the correct way out. The gameplay is as follows ->

* The user has to collect the coins.
* The user has to find the key to unlock the exit door.
* Then eventually he has to collect the treasure inside the temple.

The player moves via teleportation by clicking on the waypoints which are appropriately spawned in the whole environment. The game is targeted for mobile devices and for Google Cardboard HMD which is the reason why I did the following optimizations taking into account the efficiency.

* The spawning of waypoints is made dynamic rather than placing them all initially(This drastically increased the speed of the game as only 5 waypoint scripts were running at a time rather than 90(which is the total number of waypoints)).
* Rendering is enhanced through occlusion cutting.
* Baked lights are used rather than realtime illumination as most of the objects in the game are static.
* Also I used unlit(supports lightmap) shaders rather than the standard ones. It had some trade offs in look and feel but worked decently fine for mobile devices with good efficiency.

[Here](summigandhi.com) is the walkthrough of the game.

# Versions
* Unity 2017.2.0f3
* GVR Unity SDK v1.70.0




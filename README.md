# WpfDeskAppFlightGear

A Desktop app which connects to FlightGear simulator and show all the important data about the flight,such as the speed,direction and joystic controler movement.

General Description
This program controls an aircraft within the "[FlightGear]" (https://www.flightgear.org/) flight simulator. The program connects to the flight 
simulator as a client.
The client will send to the simulator the data that he gets from the user - a csv file that repesent the flight instructions.

### Collaborators
This program was developed by four student, Ori Choen, Ori Abramnovich, Aviv Dimri and Yosef Berebi, CS students from Bar-Ilan university, Israel.

### Code Design:
FlightSimulatorApp has been programmed by the MVVM design, as we used C Sharp's data binding mechanism,and xaml.

## Features:
###### Slider:
![IMG_0066](https://user-images.githubusercontent.com/80414213/114616969-327a8700-9cb0-11eb-90b1-b596e8b3bbf6.jpeg)

Using a slider, which can control the flight vessel. The slider repesent the real time in the flight,you may choose to go forward or backwards as you wish.
Also,You can decide about the speed the flight projected on the screen as you want.

###### Grafh:
![IMG_0065](https://user-images.githubusercontent.com/80414213/114616721-de6fa280-9caf-11eb-808d-dba013f7edf8.jpeg)

Using a list of the features that repesented all the featuers that are component in the flight-
you may choose one of the featuers and his grafh and the most correlation feature will show on the screen and update them self at real time.

In addion,the line regression line of the tow features,and the last 30 second will be repesented also.

###### Joystic:
A small joystic that show the movement of the plane during the flight.

###### Real-Time flight:
During the flight, you'll receive a real-time flight data, such as Altimeter, Direction, Air Speed, Pitch, Roll, Heading and more.

### Loading DLL:

If you want-you can also find a devion in the flight at real time runing:

There is tow algoritam that get a normal flight to learn from , and then a real flight to finding a point with a devion and represented it in bold(red color).

A. SimpleAnomalyDeteion - learning the most noraml distance between point and her line regression, and finding which points 
are more distant than the normal and alert it.

B. MinCircle - get the MinCircle that contains in all the normal point flight, and finding which point are inside the circle and alert it also in bold.

When you are loding Dll libary - the circle / line regression and their devion points at real time will appear in the MainWindow,
you may want to go specific to this time to learn about the devion more,the dev point will represented in red color.

### Adding your own Dll:

If you want you may add your own algoritam and load it at real time without compiling again - you just need to implement the interrface  by name IntrfaceDll - 
you may find it in the plugins folder the declaration which method you need to load.

### Instructions for load own DLL
1.Create a class by the name - dll.

2.Implement the function in IntrfaceDll.

3.There is a three function to implement - craete update and time.(you  can see the decleration in the plugins folder).

4.drow the shepes you want to shoe in the project.

5.Load the path dll file in run time of the project.

## Structure project:
There is a few folders:

1.In the main folder you have the solution project.

2.In the packages folder there is the Oxplot libary - you must have it in your project!

3.In myfirstproject folder there is all the files that needed.

4.In the plugins folder there is the dll lib for the algoritam.

5.The folder FilesUploading there is the xml and tow csv files to upload for the project.


# Installation for running the 
1.Open the FlightGear app (Downloads it if you d'ont hev - click here https://www.flightgear.org/).

2.Config the setting and write this in the commend line in the setiing:

--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small

--fdm=null.

3.Downloads the project by git clone form https://github.com/yosi058/WpfAppFlightGear.git.

4.Make sure you have the Oxplot lib on your project (otherwise-downloads it).

5.Build the app on the x64 - otherwise the dll will not working.

6.Provide xml file for all the names of element in the flight data.load the xml first.

7.Provide tow csv fiels - one is a normal flight and the second is a test flight to finding devion.


## Documentation






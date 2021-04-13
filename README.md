# WpfDeskAppFlightGear

A Desktop app which connects to FlightGear simulator and show all the importent data about the flight,such as the speed,direction and joystic controler movement.

General Description
This program controls an aircraft within the "[FlightGear]" (https://www.flightgear.org/) flight simulator. The program connects to the flight simulator as a client.
The client will send to the simulator the data that he get from the user - a csv file that repesent the flight instructions.

# Collaborators
This program was developed by four student, ori choen, ori abramnovich, aviv dimri and yosef berebi, CS students from Bar-Ilan university, Israel.

# Code Design:
FlightSimulatorApp has been programmed by the MVVM design, as we used C Sharp's data binding mechanism,and xaml.

# Features
Controller:
Using a sliders, you can control the flight vessel. The slider repesent the real time in the flight,you may choose to go forward or backwards as you wish.
you can also decide about the speed the flight projected on the screen as your want.

Grafh:
Using a list of the features that repesented all the featuers that are component in the flight-
you may choose one of the featuers and his grafh and the most coorolation feature will show on the screen and update them self at real time.
In addion,the line regression line of the tow features,and the last 30 second will be repesented also.

Joystic:
A small joystic that show the movement of the palne during the flight.


Real-Time flight:
During the flight, you'll receive real-time flight data, such as Altimeter, Direction, Air Speed, Pitch, Roll, Heading and more.

# Loading DLL:
if you want-you also can find a devion in the flight at real time runing:
There is tow algoritam that get a normal flight to learn from , and than a real flight to finding a point with a devion and represented it in bold(red color).
A. SimpleAnomalyDeteion - learning the most noraml distance between point and her line regression,and finding which points are more distant than the normal and alert it.
B. MinCircle - get the MinCircle that contion all the normal point flight,and finding which point are inside the circle and alert it also in bold.
When you loding Dll libary - the circle / line regression and her devion points at real time will appear in the MainWindow,
you may want to go specific to this time to learn about the devion more,the dev point will represented in red color.
# Adding your own Dll:
if you want you may add your own algoritam and load it at real time without compiling again - you just need to implement the interrface  by name IntrfaceDll - 
you may find it in the plugins folder the declaration which method you need to load.



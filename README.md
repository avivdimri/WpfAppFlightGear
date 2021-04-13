# WpfDeskAppFlightGear

A Desktop app which connects to FlightGear simulator and show all the importent data about the flight,such as the speed,dircation and joystic controler movement.

General Description
This program controls an aircraft within the "[FlightGear]" (https://www.flightgear.org/) flight simulator. The program connects to the flight simulator as a client.
The client will send to the simulator the data that he get from the user - a csv file that repesent the flight instructions.

Collaborators
This program was developed by four student, ori choen, ori abramnovich, aviv dimri and yosef berebi, CS students from Bar-Ilan university, Israel.

Code Design:
FlightSimulatorApp has been programmed by the MVVM design, as we used C Sharp's data binding mechanism,and xaml.

Features
Controller:
Using a sliders, you can control the flight vessel. The slider repesent the current time in the flight,you may choose to go forward or backwards as you wish.
you can also decide about the speed the flight projected on the screen as your want.

Grafh:
Using a list of the features that repesented all the featuers that are component in the flight-
you may choose one of the featuers and his grafh and the most coorolation feature will show on the screen and update them self at real time. 
Joystic:
A small joystic that show the movement of the palne during the flight.


Real-Time flight data:
During the flight, you'll receive real-time flight data, such as Longitude, Latitude, Air Speed, Pitch, Roll, Heading and more.


# PaddlerData
A simple database style app to log essential details of paddlers at an event for safety purposes. This is only my second WPF app and a quite a scale up from the 'Paddle Length Calculator'.

## Application Structure

The app was made using **C#** and the **.NET WPF** framework. data persistence is via xml, multiple documents are allowed (not silmultaneously) and managed using system dialog boxes 

## Rough instructions

There are fields to enter Name, Address and contact information of the paddler along with an emergency name and contact number.

There are two check boxes which can be used to select:

* The paddler has entered the terms and conditions.

* The paddler is now on the water.

When the 'Paddler On Water' checkbox is selected for a paddler, the time is logged. A count for the amount of paddlers along with a list of their names with the time they entered the water is displayed at the bottom. This makes it easy to see who is on the water and how long they have been there.

## More Information

More comprehensive instructions are available from the help page within the app, please view this if you want to know more.

## Privacy, GDPR etc.

This application was developed more for a demonstration project but could potentially be used. To that end, I am working on the necessary privacy inclusions. If in the meantime you wish to use this application in it's PRE-RELEASE format, it will be necessary that you take the appropriate steps to ensure the correct privacy legislation has been applied for your local juristiction. This may involve making the necesary modifications to the code and re-compliling rather than using the 'DEMO' release I have provisionally set up.

## The source code

Within the PaddnerData.sln file, there is a reference to a deployment project which is not included with the source. Therefore, if you fork or clone this repository, it is likely you will have to remove that reference in order to compile the project.
 
 

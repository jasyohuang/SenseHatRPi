# SenseHatRPi
Raspberry Pi SenseHat Sensor selection with joystick.. :v

#How it's work ?
move the joystick on the SenseHat to choose the sensor reading options. According to my code, LeftKey refers to temperature reading, RightKey to Humidity, DownKey to pressure and EnterKey to Scrolling Text

#What's behind the codes ?
In this project, I made a lot of Actions class and ActionSelectors. once the app start, the HomeSelector.cs will be called from the MainPage.xaml.cs... from that, HomeSelector.cs will call the home class which is the class to display a black screen on the led matrix and read the joystick movements. The same process happened inside the home.cs to choose another action in this project.
For the library, I used the Emmelsoft.IoT.RPi.SenseHat which is free to download from NuGet

#References
for further information about Raspberry Pi SenseHat and it's library on C#, you can access these links
https://github.com/emmellsoft/RPi.SenseHat (for Raspberry Pi SenseHat library and sample codes)
https://www.raspberrypi.org/products/sense-hat/ (for further information about the sensehat)

#Hope You Enjoy This Project :)

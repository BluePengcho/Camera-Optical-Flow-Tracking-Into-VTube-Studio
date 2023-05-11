# Camera Optical Flow Tracking Into VTube Studio

This project uses the [Unity3D-CameraOpticalFlow](https://github.com/Bonjour-Interactive-Lab/Unity3D-CameraOpticalFlow) unity package which is a GPU based optical flow system to generate the movement data. This project does not use OpenCV or any other imaging computer vision library, it mainly uses shaders to generate the data and seems to be resource light?? (this still needs more testing). 

**Head Tracking**
------
Head tracking works by generating 5 separate optical flow textures each only a small section of the video at the designated locations (and at a reduced resolution) to increase performance. Then by computing the average velocity at each location we get the XY vector tracker for that location (when there is no movement the tracker returns to X0,Y0).

<img src="https://github.com/BluePengcho/Camera-Optical-Flow-Tracking-Into-VTube-Studio/blob/main/Images/CameraOpticalFlowTrackingDiagram.png" width="70%" height="70%">

By generating 5 separate optical flow textures each only a small section of the video at the designated locations (and at a reduced resolution to increase performance). Then by computing the average velocity at each location we get the XY vector tracker for that location (when there is no movement the tracker returns to X0,Y0).

For the Left and Right trackers (green) we only get the Y component and for the Top and Bottom trackers (red) we only get the X component. And for the centre tracker (yellow) we get both the X & Y components. 

Then by getting the average vector (pink) from each of the trackers we are able to generate and providing a more accurate movement tracking in the X & Y axis for the whole video. 

Then converting those values to the respective VTube Studio parameters and sending them on to VTube Studio. 

**Note:** Unfortunately at the moment slow camera movements create a greater optical flow value and therefore larger vectors than fast camera movements. Creating mismatched movement/tracking data in VTube Studio this still needs to be improved.

**Note2:** Having more trackers at more points on the video would probably produce more accurate movement data but at an increased performance cost.  

**Eye Tracking**
------
Eye tracking works by looks at the area on the video with the most motion. This is deduce by generated an optical flow texture of the entire video (this texture is blurred and has a reduced texture size to increase performance). The generated texture is comprised of a Red and Green layer, the Red layer is movement in the X axis and the Green layer is movement in the Y axis. By combining the Red and Green layers we create a black and white image with the area/pixel with the highest contrast/brightness having the greatest amount of movement that frame. So by computing where this brightest area/pixel is we are able to deduce the X & Y location on the video with the greatest movement and essentially simulate where the user would/could be looking. Then finally convert the values to the respective VTube Studio parameters and send it to VTube Studio. 

**Head Tracking Configurable Settings**
------
The VTube Studio parameter values can be adjusted in the 'Head Tracking' game object and its 'HeadTracking' script component for the following values:-
XLive2dMin
XLive2dMmax
YLive2dMin	
YLive2dMax

The output values for X and Y can be inverted with toggles in the 'Head Tracking' game object and its 'HeadTracking' script component.



Eye Tracking Configurable Settings
------
Eye Tracking behaviour can be changed in the 'Eye Tracking Optical Flow Controller' game object and its 'Optical Flow' script component by adjusting the Lambda and the Threshold slider values.

Recommended values are :-
Lambda: 0.004
Threshold: 0.085

Eye Tracking Smoothness can be adjusted in the 'Eye Tracking' game object and its 'Eye Tracking' script component by adjusting the 'Smooth Speed' Value. It can also be adjusted more in the 'Eye Tracking' game object and its 'maximumScript' script component by adjusting the 'Smooth Speed' value.

Eye tracking Jittering can be adjusted in the 'Eye Tracking'  game object and its 'maximumScript' script component by adjusting the 'Set Distance' value.

The VTube Studio parameter values can be adjusted in the 'Eye Tracking' game object and its 'EyeTracking' script component for the following values:-
XLive2dMin
XLive2dMmax
YLive2dMin
YLive2dMax

The output values for X and Y can be inverted with toggles in the 'Eye Tracking' game object and its 'EyeTracking' script component.

The wonderful [VTS-Sharp Library](https://github.com/FomTarro/VTS-Sharp) is used to interface with VTube Studio.


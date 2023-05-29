# Camera Optical Flow Tracking Into VTube Studio

This project uses the [Unity3D-CameraOpticalFlow](https://github.com/Bonjour-Interactive-Lab/Unity3D-CameraOpticalFlow) unity package which is a GPU based optical flow system to generate movement data. Then converting this movement data into Head and Eye parameter values for use in VTube Studio. This project uses the wonderful [VTS-Sharp Library](https://github.com/FomTarro/VTS-Sharp) to interface with VTube Studio.    

This project does not use OpenCV or any other imaging computer vision library, it mainly uses shaders to generate the tracking data. 

**Download and Install Instructions**
------
Download **'CameraOpticalFlowTrackingIntoVTubeStudio.zip'** from the latest version in **'Releases'** (found on the right side of the project page). Or alternatively download the code as **'Download Zip'**. Unzip the folder then add the project in the Unity Hub (tested with Unity 2021.3.0f1 other versions might still work). Run in Unity and open the 'CameraOpticalFlowTracking' Scene in the Scenes folder.

**Head Tracking**
------
Head tracking works by generating 5 separate optical flow textures each only a small section of the video at the designated locations (and at a reduced resolution to increase performance). Then by computing the average velocity at each location we get the XY vector tracker for that location (when there is no movement the tracker returns to X0,Y0).

<img src="https://github.com/BluePengcho/Camera-Optical-Flow-Tracking-Into-VTube-Studio/blob/main/Assets/CameraOpticalFlowTrackingDiagram.png" width="70%" height="70%">

For the Left and Right trackers (green) we only get the Y component and for the Top and Bottom trackers (red) we only get the X component. And for the centre tracker (yellow) we get both the X & Y components. 

Then by getting the average vector (pink) from each of the trackers we are able to generate and provide more accurate movement tracking in the X & Y axis for the whole video. 

Then converting those values to the respective VTube Studio parameters and sending them onto VTube Studio. 

**Note:** Head tracking sensitivity can be adjusted with the **Lambda** and **Threshold** sliders (sliders values are currently not saved after runtime so please change the default slider values in the inspector after runtime to save the values). For more sensitive camera tracking set the Lambda & Threshold slider values to 0.

**Note2:** Unfortunately at the moment slow camera movements create a greater optical flow value and therefore larger vectors than fast camera movements. Creating mismatched movement/tracking data in VTube Studio this still needs to be improved.

**Note3:** Having more trackers at more points on the video would probably produce more accurate movement data but at an increased performance cost.  

**Eye Tracking**
------
Eye tracking works by looking at the area on the video with the most motion. This is deduce by generating an optical flow texture of the entire video (this texture is blurred and has a reduced texture size to increase performance). The generated texture is comprised of a red and green layer, the red layer is movement in the X axis and the green layer is movement in the Y axis. By combining the red and green layers we create a black and white image with the area/pixel with the highest contrast/brightness having the greatest amount of movement that frame. So by computing where this brightest area/pixel is we are able to deduce the X & Y location on the video with the greatest movement (the blue tracker) and essentially simulate where the user would/could be looking. We then smooth the blue tracker movement twice, first with the black tracker then secondly with the white tracker. Then finally convert the X & Y values of the white tracker into the respective VTube Studio parameters and send it onto VTube Studio. 

**Webcam/Video Input**
------
Tracking input can be either from Webcam or Video(.mp4 or other Unity supported video format). 

**Webcam Input**: disable the 'Video Input' game object and enable the 'Webcam Input' game object and selected the desired webcam in the webcam component found in the inspector.

**Video Input**: disable the 'Webcam Input' game object and enable the 'Video Input' game object. And chose the desired video in the Video component found in the inspector. (Note: the video must be in the unity project's folder)

**OBS Capture** 
------
For OBS capture it is recommended to use the **Webcam Input** method and feed in the webcam input into both OBS and Unity simultaneously for capture. The **Video Input** method is not recommended as in OBS you would have to use game capture on the Unity project which would still display the UI, you could remove the UI objects in the inspector and in the code. Or alternatively you could add a video/camera output plug-in/code in Unity (E.g. Spout or NDI) and capture the output feed in OBS.

**Head Tracking Configurable Settings**
------
The VTube Studio parameter values can be adjusted in the 'Head Tracking' game object and its 'HeadTracking' script component for the following values:-
- XLive2dMin
- XLive2dMmax
- YLive2dMin	
- YLive2dMax

The output values for X and Y can be inverted with toggles in the 'Head Tracking' game object and its 'HeadTracking' script component.

Eye Tracking Configurable Settings
------
Eye Tracking behaviour can be changed in the 'Eye Tracking Optical Flow Controller' game object and its 'EyeOpticalFlow' script component by adjusting the Lambda and the Threshold slider values (in the inspector not the UI sliders).

Recommended values are :-
- Lambda: 0.004
- Threshold: 0.085

Eye Tracking Smoothness can be adjusted in the 'Eye Tracking' game object and its 'EyeTracking' script component by adjusting the 'Smooth Speed' value. It can also be adjusted more in the 'Eye Tracking' game object and its 'maximumScript' script component by adjusting the 'Smooth Speed' value.

Eye tracking Jittering can be adjusted in the 'Eye Tracking' game object and its 'maximumScript' script component by adjusting the 'Set Distance' value.

The VTube Studio parameter values can be adjusted in the 'Eye Tracking' game object and its 'EyeTracking' script component for the following values:-
- XLive2dMin
- XLive2dMmax
- YLive2dMin
- YLive2dMax

The output values for X and Y can be inverted with toggles in the 'Eye Tracking' game object and its 'EyeTracking' script component.

**Connecting to VTube Studio** 
------
Ensure that in the settings of VTube Studio the **'Start API'** (found in VTube Studio Pluggins) is turned on. In Unity this project should connect to VTube Studio automatically once run, if this fails or becomes disconnected simply click the **'Connect'** button.

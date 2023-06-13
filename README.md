# Camera Optical Flow Tracking Into VTube Studio

This project uses the [Unity3D-CameraOpticalFlow](https://github.com/Bonjour-Interactive-Lab/Unity3D-CameraOpticalFlow) unity package which is a GPU based optical flow system to generate movement data. Then converting this movement data into Head and Eye parameter values for use in VTube Studio. This project uses the wonderful [VTS-Sharp Library](https://github.com/FomTarro/VTS-Sharp) to interface with VTube Studio. 

This project also uses the following GitHub projects (these are already included in the project there is no need add them):-
- [Spout4Unity](https://github.com/sloopidoopi/Spout4Unity) - receive and share textures using the Spout2SDK for Unity. 
- [UnityVLCPlayer](https://github.com/bosqmode/UnityVLCPlayer) - a minimalist libvlc player for Unity.

This project does not use OpenCV or any other imaging computer vision library, it mainly uses shaders to generate the tracking data. 

**Note:** This project assumes a horizontal 2:1 aspect ratio input video.

**Note2:** To use RTMP/RTSP/URL input you might have to allow the **'CameraOpticalFlowTracking.exe'** program through your firewall.

**Note3:** For RTMP/RTSP/URL input you must include the 'rtmp://' or 'rtsp://' or 'https://' respectively in the URL. Recommend turning on **'Auto(Re)Connect'** to help maintain connection in the event of a remote disconnection.

**Note4:** To use RTMP/RTSP input depending on your setup you will need a video communication server, recommend using 'MonaServer'.

**Known Issue:** Currently on first start up and connecting to VTube Studio the model tracking movements don't start properly and have jerky movements. Please wait a little bit for the movement to kick in correctly or try clicking the **'Connect'** and the **'Refresh Port List'** buttons. It is recommended to leave the **'Auto (Re)Connect'** for VTube Studio on (this issue is still being looked into).

**Download and Install Instructions**
------

Download **'CameraOpticalFlowTrackingForVTubeStudio.zip'** from the latest version in **'Releases'** (found on the right side of the project page. Unzip the folder and run 'CameraOpticalFlowTracking.exe'. 

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
Tracking input can be either from a **Webcam** or **RTMP/RTSP/URL**.

**OBS Capture** 
------
It is recommended to use Spout for OBS and turn on the Spout output in **'Video Output'** settings.

Another option is to use **'Game Capture'** or **'Window Capture'** in OBS. You can remove the UI objects by double clicking in the main video area of this plug-in to hide/show the UI. 

**Head Tracking Configurable Settings**
------
- Please set the FaceAngle Parameters found in the **'Tracking Setting'** to match your Live2d model parameter values as found in VTube Studio.

- Head Tracking can be toggled on/off in the main UI.

- The tracking behaviour can be changed by adjusting the Lambda and the Threshold slider values found under **'Head Tracking'** in the main UI.

  - Lambda Slider - effects the sensitivity of camera movement (default value is 0.07)

  - Threshold Slider - effects the resulting output vector size of the detected movement (default value is 0.15)

- The output values for X and Y can be inverted with toggles in the 'Tracking Settings' window under FaceAngle Parameters.

Eye Tracking Configurable Settings
------
- Please set the EyeRight Parameters found in the **'Tracking Setting'** to match your Live2d model parameter values as found in VTube Studio.

- Eye Tracking can be toggled on/off in the main UI.

- The tracking behaviour can be changed by adjusting the Lambda and the Threshold slider values found under **'Eye Tracking'** in the main UI.

  - Lambda Slider - effects the motion detection sensitivity (default value is 0.15)
  - Threshold Slider - effects the motion detection sensitivity (default value is 0.25)

- The output values for X and Y can be inverted with toggles in the **'Tracking Settings'** window under EyeRight Parameters.

- **'Eye tracking Video Center Bias'** found in the 'Video Settings' determines the looking at area of for eye tracking this can help prevent the eye from only looking at the edge areas of the video input and resulting in a odd shifty looking model in VTube Studio.  

- To help performance usage and improve the FPS, **'Eye Tracking Quality'** can be adjusted in the tracking settings.

- The eye tracking behaviour is very much effected by the FPS, the FPS can be set/locked in **'Video Settings'** window. 

- The **Eye Speed** movement and **Eye Smoothness** can also be adjusted in the tracking settings.

**Testing and calibrating** 
------
Recommend testing and calibrating the settings with the RTMP/RTSP/URL video input using these walkabout videos hosted on archive.org. 
**Note: Copy and paste the URL including the "https://"**

Walkabout Akihabara Japan - https://ia804700.us.archive.org/19/items/youtube-s-cWkQuy34s/s-cWkQuy34s.mp4

Walkabout in a quite Japanese neighbourhood - https://ia904709.us.archive.org/32/items/youtube-uCaM1DRgm6g/uCaM1DRgm6g.mp4

Walkabout Japanese countryside shopping area - https://ia801609.us.archive.org/2/items/youtube-SebpLpUvHOQ/SebpLpUvHOQ.mp4

Walkabout in Kawagoe Saitama Japan - https://ia804702.us.archive.org/34/items/youtube-x1lXgESxjEY/x1lXgESxjEY.mp4




**Connecting to VTube Studio** 
------
Ensure that in the settings of VTube Studio the **'Start API'** (found in VTube Studio Pluggins) is turned on. In Unity this project should connect to VTube Studio automatically once run, if this fails or becomes disconnected simply click the **'Connect'** button. It is recommended to leave the 'Auto (Re)Connect' for VTube Studio on.

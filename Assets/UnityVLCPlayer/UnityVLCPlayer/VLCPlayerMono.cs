using UnityEngine;
using UnityEngine.UI;

namespace bosqmode.libvlc
{
    public class VLCPlayerMono : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.RawImage m_rawImage;

        [SerializeField]
        private string url = "";

        public InputField input_URL;

        [SerializeField]
        [Min(0)]
        [Tooltip("Output resolution width, can be left 0 for automatic scaling")]
        private int width = 480;

        [SerializeField]
        [Min(0)]
        [Tooltip("Output resolution height, can be left 0 for automatic scaling")]
        private int height = 256;

        [Tooltip("Whether to automatically adjust the rawImage's scale to fit the aspect ratio")]
        [SerializeField]
        private bool autoscaleRawImage = true;

        private bool StartingNewPlayer = false;

        public Toggle mute;

        public Toggle WebcamToggle;
        public Toggle URLToggle;

        public Toggle AutoConnect;

        public Button ConnectButton;
        public Button DisconnectButton;

        private Texture2D tex;
        private VLCPlayer player;

        public Text URLStatus;

        public Image connectionLight;
        public bool isError = false;

        int _trackGetAttempts = 0;

        int _trackGetAttemptsTwo = 0;

        string oldurl = "";

        private void Start()
        {
            url = input_URL.text;

            ConnectButton.onClick.AddListener(delegate {
                ConnectButtonClick();
            });

            DisconnectButton.onClick.AddListener(delegate {
                DisconnectButtonClick();
            });

            mute.onValueChanged.AddListener(delegate {
                setmute();
            });

            WebcamToggle.onValueChanged.AddListener(delegate {
                InputToggle();
            });

            URLToggle.onValueChanged.AddListener(delegate {
                InputToggle();
            });


        }

        private void Update()
        {
            //Check VLC Player Status and display in the UI
            if (player != null)
            {
                if (URLStatus.text != "Error Try Reconnecting")
                {
                    string playerStatus = player.myPlayString();

                    if (playerStatus == "libvlc_Playing")
                    {
                        URLStatus.text = "Connected";
                        connectionLight.color = Color.green;
                    }

                    if (playerStatus == "libvlc_Buffering")
                    {
                        URLStatus.text = "Buffering";
                        connectionLight.color = Color.yellow;
                    }

                    if (playerStatus == "libvlc_Ended")
                    {
                        URLStatus.text = "Connection Ended";
                        connectionLight.color = Color.red;
                    }

                    if (playerStatus == "libvlc_Error")
                    {
                        URLStatus.text = "ERROR";
                        connectionLight.color = Color.red;
                    }

                }
            }


            //Check if VLC player is updating frames
            byte[] img;

            if (player != null && player.CheckForImageUpdate(out img))
            {

                _trackGetAttempts = 0;

                if (tex == null)
                {
                    if ((width <= 0 || height <= 0) && player.VideoTrack != null)
                    {
                        width = (int)player.VideoTrack.Value.i_width;
                        height = (int)player.VideoTrack.Value.i_height;
                    }

                    if (width > 0 && height > 0)
                    {
                        tex = new Texture2D(width, height, TextureFormat.RGB24, false, false);
                        m_rawImage.texture = tex;

                        if (autoscaleRawImage)
                        {
                            RectTransform rect = m_rawImage.rectTransform;
                            float ratio = height / (float)width;
                            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.rect.width * ratio);
                        }
                    }
                }
                else
                {
                    tex.LoadRawTextureData(img);
                    tex.Apply(false);
                }
            }
            else
            {
                //Reconnect to URL this creates a new VLC player and reconnects to the URL
                if (AutoConnect.isOn == true)
                {
                    _trackGetAttempts++;

                    if (_trackGetAttempts >= 100)
                    {
                        _trackGetAttempts = 0;

                        if (player == null)
                        {
                            ReStartPlayer();
                        }
                        else
                        {
                            if (player.myPlay() == true)
                            {
                                ReStartPlayer();
                            }
                        }
                    }
                    
                }
                else
                {
                //If Video Not Playing and not set to reconnect 
                   if (player != null)
                   {

                    _trackGetAttemptsTwo++;
                        
                        if (_trackGetAttemptsTwo >= 500)
                        {
                        _trackGetAttemptsTwo = 0;

                        string playerStatus = player.myPlayString();

                                if (playerStatus == "libvlc_Playing")
                                {
                                    URLStatus.text = "Error Try Reconnecting";
                                    connectionLight.color = Color.red;
                                }
                        }
                   }
                }
            }
        }

        private void setmute() //Mutes or Unmutes by creating a new VLC player with the requested Audio setting 
        {
            if (player != null)
            {
                ReStartPlayer();
            }
        }
                
        private void InputToggle() //Dispose of player if switching between input modes
        {
            if (URLToggle.isOn == false)
            {
                if (player != null)
                {
                    player?.Dispose();
                    player = null;
                    URLStatus.text = "Disconnected";
                    connectionLight.color = Color.gray;
                }
            }

            if (WebcamToggle.isOn == true)
            {
                if (player != null)
                {
                    player?.Dispose();
                    player = null;
                    URLStatus.text = "Disconnected";
                    connectionLight.color = Color.gray;
                }
            }

        }

        private  void ReStartPlayer()
        {
            isError = false;
            if (player != null)
                {
                player?.Dispose();
                player = new VLCPlayer(width, height, input_URL.text, !mute.isOn, AutoConnect.isOn, URLStatus);

                if (input_URL.text == oldurl)
                {
                    URLStatus.text = "Reconnecting";
                }
                else
                {
                    URLStatus.text = "Connecting";
                }
                oldurl = input_URL.text;

                connectionLight.color = Color.yellow;
            }
            else
            {
                player = new VLCPlayer(width, height, input_URL.text, !mute.isOn, AutoConnect.isOn, URLStatus);
                URLStatus.text = "Connecting";
                oldurl = input_URL.text;
                connectionLight.color = Color.yellow;
            }
        }

        private void ConnectButtonClick()
        {
            isError = false;
            if (player != null)
            {
                player?.Dispose();
                player = new VLCPlayer(width, height, input_URL.text, !mute.isOn, AutoConnect.isOn, URLStatus);

                if (input_URL.text == oldurl)
                {
                    URLStatus.text = "Reconnecting";
                }
                else
                {
                    URLStatus.text = "Connecting";
                }
                oldurl = input_URL.text;

                connectionLight.color = Color.yellow;
            }
            else
            {
                URLStatus.text = "Connecting";
                connectionLight.color = Color.yellow;
                player = new VLCPlayer(width, height, input_URL.text, !mute.isOn, AutoConnect.isOn, URLStatus);
                oldurl = input_URL.text;
            }
        }

        private void DisconnectButtonClick()
        {
            if (player != null)
            {
                player?.Dispose();
                player = null;
                URLStatus.text = "Disconnected";
                connectionLight.color = Color.gray;
            }
        }

        private void OnDestroy()
        {
            player?.Dispose();
        }
    }
}
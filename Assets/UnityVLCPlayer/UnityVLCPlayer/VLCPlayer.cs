﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


namespace bosqmode.libvlc
{
    public class VLCPlayer : IDisposable
    {
        private IntPtr _media;
        private IntPtr _mediaPlayer;
        private IntPtr _imageIntPtr;
        private IntPtr _libvlc;

        private LockCB _lockHandle;
        private UnlockCB _unlockHandle;
        private DisplayCB _displayHandle;
        private GCHandle _gcHandle;

        private bool _locked = true;
        private bool _update = false;
        private byte[] _currentImage;
        private int _width = 480;
        private int _height = 256;
        private int _channels = 3;
        private const string _defaultArgs = "--ignore-config;--no-xlib;--no-video-title-show;--no-osd";
        private libvlc_video_track_t? _videoTrack = null;
        private IntPtr _tracktorelease;
        private int _tracks;
        private volatile bool _cancel = false;
      
        private bool myAutoConnect = false;

        void Start()
        {

        }

        /// <summary>
        /// Gets the video tracks information that libvlc receives
        /// </summary>
        public libvlc_video_track_t? VideoTrack
        {
            get
            {
                return _videoTrack;
            }
        }

        /// <summary>
        /// Checks if image has been updated since last check, and outs the image bytes
        /// </summary>
        /// <param name="currentImage">arr where to store the image bytes</param>
        /// <returns>whether update happened or not</returns>
        public bool CheckForImageUpdate(out byte[] currentImage)
        {
            currentImage = null;
            if (_update)
            {
                currentImage = _currentImage;
                _update = false;
                return true;
            }
            else
            {

            }
            return false;
        }


        public VLCPlayer(int width, int height, string mediaUrl, bool audio, bool AutoConnect, Text getmyText)
        {
            _width = width;
            _height = height;

            _gcHandle = GCHandle.Alloc(this);

            string argstrings = _defaultArgs;
            if (!audio)
            {
                argstrings += ";--no-audio";
            }

            string[] args = argstrings.Split(';');

            _libvlc = LibVLCWrapper.libvlc_new(args.Length, args);

            if (_libvlc == IntPtr.Zero)
            {
                Debug.LogError("Failed loading new libvlc instance...");
                return;
            }

            _media = LibVLCWrapper.libvlc_media_new_location(_libvlc, mediaUrl);

            if (_media == IntPtr.Zero)
            {
                Debug.LogError("For some reason media is null, maybe typo in the URL?");
                return;
            }

            _mediaPlayer = LibVLCWrapper.libvlc_media_player_new(_libvlc);
            LibVLCWrapper.libvlc_media_player_set_media(_mediaPlayer, _media);

            _lockHandle = vlc_lock;
            _unlockHandle = vlc_unlock;
            _displayHandle = vlc_picture;

            LibVLCWrapper.libvlc_video_set_callbacks(_mediaPlayer, _lockHandle, _unlockHandle, _displayHandle, GCHandle.ToIntPtr(_gcHandle));
            LibVLCWrapper.libvlc_video_set_format(_mediaPlayer, "RV24", (uint)_width, (uint)_height, (uint)_width * (uint)_channels);
            LibVLCWrapper.libvlc_media_player_play(_mediaPlayer);

            myAutoConnect = AutoConnect;

            System.Threading.Thread t = new System.Threading.Thread(TrackReaderThread);

            t.Start();


        }

        public string myPlayString()
        {
            libvlc_state_t PlayerState = LibVLCWrapper.libvlc_media_get_state(_media);

            string playerstatus = PlayerState.ToString();
            return playerstatus;
        }

        public bool myPlay() //checks if vlcplayer state is endend
        {
            libvlc_state_t state = LibVLCWrapper.libvlc_media_get_state(_media);

            if (state.ToString() == "libvlc_Ended")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void TrackReaderThread()
        {
            int _trackGetAttempts = 0;
            while (_trackGetAttempts < 100 && _cancel == false)
            {
                libvlc_video_track_t? track = GetVideoTrack();

                if (track.HasValue && track.Value.i_width > 0 && track.Value.i_height > 0)
                {
                    _videoTrack = track;
                    if (_width <= 0 || _height <= 0)
                    {
                        _width = (int)_videoTrack.Value.i_width;
                        _height = (int)_videoTrack.Value.i_height;
                        LibVLCWrapper.libvlc_video_set_format(_mediaPlayer, "RV24", _videoTrack.Value.i_width, _videoTrack.Value.i_height, (uint)_width * (uint)_channels);
                    }
                    break;
                }

                if (myAutoConnect == false)
                {
                    _trackGetAttempts++;
                }
                else
                {
                    _trackGetAttempts = 0;
                }
 
                System.Threading.Thread.Sleep(500);
            }

            if (_trackGetAttempts >= 100)
            {
                Console.WriteLine("Maximum attempts of getting video track reached, maybe opening failed?");
            }
        }

        private void vlc_unlock(IntPtr opaque, IntPtr picture, ref IntPtr planes)
        {
            _locked = false;
        }

        private IntPtr vlc_lock(IntPtr opaque, ref IntPtr planes)
        {
            _locked = true;

            if (_imageIntPtr == IntPtr.Zero)
            {
                _imageIntPtr = Marshal.AllocHGlobal(_width * _channels * _height);
            }

            planes = _imageIntPtr;

            return _imageIntPtr;
        }

        private void vlc_picture(IntPtr opaque, IntPtr picture)
        {

            if (!_update)
            {
                _currentImage = new byte[_width * _channels * _height];
                Marshal.Copy(picture, _currentImage, 0, _width * _channels * _height);
                _update = true;
            }

        }

        private libvlc_video_track_t? GetVideoTrack()
        {
            libvlc_video_track_t? vtrack = null;
            IntPtr p;
            int tracks = LibVLCWrapper.libvlc_media_tracks_get(_media, out p); //Error Here

            _tracks = tracks;
            _tracktorelease = p;

            for (int i = 0; i < tracks; i++)
            {
                IntPtr mtrackptr = Marshal.ReadIntPtr(p, i * IntPtr.Size);
                libvlc_media_track_t mtrack = Marshal.PtrToStructure<libvlc_media_track_t>(mtrackptr);
                if (mtrack.i_type == libvlc_track_type_t.libvlc_track_video)
                {
                    vtrack = Marshal.PtrToStructure<libvlc_video_track_t>(mtrack.media);
                }
            }

            return vtrack;
        }

        public void Dispose()
        {
            _cancel = true;

            if (_tracktorelease != IntPtr.Zero)
            {
                LibVLCWrapper.libvlc_media_tracks_release(_tracktorelease, _tracks);
            }

            if (_mediaPlayer != IntPtr.Zero)
            {
                LibVLCWrapper.libvlc_media_player_release(_mediaPlayer);
            }

            if (_media != IntPtr.Zero)
            {
                LibVLCWrapper.libvlc_media_release(_media);
            }

            if (_libvlc != IntPtr.Zero)
            {
                LibVLCWrapper.libvlc_release(_libvlc);
            }

            _mediaPlayer = IntPtr.Zero;
            _media = IntPtr.Zero;
            _libvlc = IntPtr.Zero;
        }
    }

}
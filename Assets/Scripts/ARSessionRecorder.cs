﻿using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using NatML.Recorders;
using NatML.Recorders.Clocks;
using NatML.Recorders.Inputs;
using NatSuite.Sharing;
using NatSuite.Sharing.Internal;
using System.Threading.Tasks;
#if UNITY_ANDROID
using UnityEngine.XR.ARCore;
#endif


namespace ARRecorder
{
    public class ARSessionRecorder : MonoBehaviour
    {
        [SerializeField] private ARSession arSession;

        public string mp4path = null;

        public int videoWidth = Screen.width;
        public int videoHeight = Screen.height;
        public bool recordMicrophone;

        public event Action<bool> OnRecordingStatusChanged = null;
        public event Action<bool> OnPlaybackStatusChanged = null;

        public event Action<string> OnSendMessage = null;


        private void Awake()
        {
            mp4path = Path.Combine(Application.persistentDataPath, "arcore-session.mp4");
        }

        public async void ToggleRecording()
        {
#if UNITY_ANDROID

            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                OnSendMessage?.Invoke("!!!AR Session is not ARCore Session");
                return;
            }

            if (subsystem.recordingStatus.Recording())
            {

                subsystem.StopRecording();
                OnRecordingStatusChanged?.Invoke(false);
                OnSendMessage?.Invoke("Stopped recording ");

                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
                OnSendMessage?.Invoke("Stopped playback");
                OnPlaybackStatusChanged?.Invoke(false);
            }

            using var recordingConfig = new ArRecordingConfig(subsystem.session);
            mp4path = Path.Combine(Application.persistentDataPath,
                $"arcore-session{DateTime.Now:yyyyMMddHHHmmss}.mp4");
            recordingConfig.SetMp4DatasetFilePath(subsystem.session, mp4path);

            var screenRotation = Screen.orientation switch
            {
                ScreenOrientation.Portrait => 0,
                ScreenOrientation.LandscapeLeft => 90,
                ScreenOrientation.PortraitUpsideDown => 180,
                ScreenOrientation.LandscapeRight => 270,
                _ => 0
            };
            recordingConfig.SetRecordingRotation(subsystem.session, screenRotation);
            var trupath = recordingConfig.GetMp4DatasetFilePath(subsystem.session);

            subsystem.StartRecording(recordingConfig);

            OnRecordingStatusChanged?.Invoke(true);

            OnSendMessage?.Invoke("Start recording");



#endif
        } 
        public void TogglePlayback()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                OnSendMessage?.Invoke("!!!AR Session is not ARCore Session");
                return;
            }

            if (subsystem.playbackStatus.Playing())
            {
                subsystem.StopPlayback();
                OnPlaybackStatusChanged?.Invoke(false);
                OnSendMessage?.Invoke("Stopped playback");
                return;

            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
                OnPlaybackStatusChanged?.Invoke(false);
                OnSendMessage?.Invoke("Stopped playback");

                return;
                
            }

            if (!File.Exists(mp4path))
            {
                OnSendMessage?.Invoke($"!!!cannot find file path ");
                return;

            }

            subsystem.StartPlayback(mp4path);
            

            OnPlaybackStatusChanged?.Invoke(true);
            
            OnSendMessage?.Invoke("Start playback");
            
#endif
        }
    }
}
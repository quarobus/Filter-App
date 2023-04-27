using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeGalleryNamespace;
using NatML.Recorders;
using NatML.Recorders.Clocks;
using System.Threading.Tasks;
using NatML.Recorders.Inputs;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.XR.ARCore;
using System.IO;
using NatSuite.Sharing;

public class ScreenShot : MonoBehaviour
{
    [SerializeField]
    MediaPlayer mediaPlayer;

    bool takePicture;
    byte[] SCREENSHOT;


    private async void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (takePicture)
        {
            takePicture = false;

            var tempRend = RenderTexture.GetTemporary(source.width, source.height);
            Graphics.Blit(source, tempRend);

            Texture2D tempText = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            Rect rect = new Rect(0, 0, source.width, source.height);
            tempText.ReadPixels(rect, 0, 0, false);
            tempText.Apply();
            mediaPlayer.OpenScreen(tempText);
            RenderTexture.ReleaseTemporary(tempRend);
            SCREENSHOT = tempText.EncodeToPNG();
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(SCREENSHOT, "CameraTest", "CaptureImage.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        }
        Graphics.Blit(source, destination);
    }

    public void TakeScreenShot()
    {
        takePicture = true;
    }
    public byte[] theSCREEN()
    {
        return SCREENSHOT;
    }
}

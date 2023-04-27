using NatML.Recorders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NatML;
using NatML.Recorders.Clocks;


public class MediaPlayer : Screenn
{
    [SerializeField]
    RawImage mediaImage;

    protected override void Start()
    {
        base.Start();
        CloseScreen();
    }

    public void OpenScreen(Texture imagtext)
    {
        mediaImage.texture = imagtext;
        SetScreen(true);
    }
    public void CloseScreen()
    {
        SetScreen(false);
    }

    internal void OpenScreen(MP4Recorder recorder)
    {
        throw new NotImplementedException();
    }
}

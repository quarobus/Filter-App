using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userInsert : MonoBehaviour 
{
    string URL = "http://localhost/moodmedb/userInsert.php";
    public Button btnClick;
    public InputField inputUser;
    public event Action<string> OnSendMessage = null;

    void Update()
    {
        btnClick.onClick.AddListener(() => { AddPicture(); });
    }
    public InputField GetInputClick()
    {
        return inputUser;
    }
    public void AddPicture()
    {
        InputField tag = GetInputClick();
        string Tag = tag.ToString();
        ScreenShot OBJ = gameObject.GetComponent<ScreenShot>();
        byte[] SCREENSHOT = OBJ.theSCREEN();
        WWWForm form = new WWWForm();
        form.AddField("addTag", Tag);
        form.AddBinaryData("addScreenShot", SCREENSHOT);

        WWW www = new WWW(URL, form);
        if (www.text == "registered")
        {
            OnSendMessage?.Invoke("correctly");
        }
        else
        {
            OnSendMessage?.Invoke("not working!");
        }
    }
}

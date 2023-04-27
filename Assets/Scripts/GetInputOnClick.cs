using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInputOnClick : MonoBehaviour
{
    public Button btnClick;
    public InputField inputUser;

    public void Start()
    {
        btnClick.onClick.AddListener(GetInputClick);
    }

    public void GetInputClick()
    {
    }
}

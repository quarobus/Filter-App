using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userSelect : MonoBehaviour
{
    string URL = "http://localhost/moodmedb/userSelect.php";
    public string[] usersData; 

IEnumerator start()
    {
        WWW users = new WWW(URL);
        yield return users;
        string usersDataString = users.text;
        usersData = usersDataString.Split(';');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

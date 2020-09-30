using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_details_update : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Text username = GameObject.Find("username").GetComponent<Text>();
        Text level = GameObject.Find("level").GetComponent<Text>();
        string player_username = PlayerPrefs.GetString("username");
        string player_level = PlayerPrefs.GetString("level");
        username.text = player_username;
        level.text = player_level;
    }
}

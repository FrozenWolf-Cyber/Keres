using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_level : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text level = GameObject.Find("level").GetComponent<Text>();
        string player_level = PlayerPrefs.GetString("level");
        level.text = player_level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

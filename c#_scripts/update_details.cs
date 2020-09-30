using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class update_details : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text exp = GameObject.Find("exp").GetComponent<Text>();
        Text username = GameObject.Find("username").GetComponent<Text>();
        Text games_lost = GameObject.Find("games_lost").GetComponent<Text>();
        Text games_drawn = GameObject.Find("games_drawn").GetComponent<Text>();
        Text games_won = GameObject.Find("games_won").GetComponent<Text>();
        Text monsters_slayed = GameObject.Find("monsters_slayed").GetComponent<Text>();

        username.text = PlayerPrefs.GetString("username");
        games_won.text = PlayerPrefs.GetString("games_won");
        games_lost.text = PlayerPrefs.GetString("games_lost");
        games_drawn.text = PlayerPrefs.GetString("games_drawn");
        exp.text = PlayerPrefs.GetString("exp");

        monsters_slayed.text = PlayerPrefs.GetString("monsters_slayed");




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

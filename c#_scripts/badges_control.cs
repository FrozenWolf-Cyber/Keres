using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class badges_control : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite wolf;
    public Sprite panther;
    public Sprite dragon;
    public Sprite archer;
    public Sprite none;

    public List<Sprite> badge_sprite = new List<Sprite>();


    void Start()
    {
        PlayerPrefs.SetString("n","0");
        badge_sprite.Add(none);
        badge_sprite.Add(wolf);
        badge_sprite.Add(panther);
        badge_sprite.Add(dragon);
        badge_sprite.Add(archer);

    }

    public void switch_badges(int next)
    {
        int n = Int32.Parse(PlayerPrefs.GetString("n"));
        string[] player_badges = PlayerPrefs.GetString("badge").Split(',');
        string[] badge_names = new string[5] { "NEED TO GROW","THE HUNTER", "ATHELETE", "DRAGON SLAYER", "ARCHER" };
        string[] badges = PlayerPrefs.GetString("badge").Split(',');
        Text badge_name = GameObject.Find("badge_name").GetComponent<Text>();
        Image badge_img = GameObject.Find("badge").GetComponent<Image>();

        if (next == 1 && player_badges[0] == "none")
        {
            if (n < player_badges.Length-1 )
            {
                n = n + 1;
            }
        }

        else  
        {

            if (n > 0 && player_badges[0] == "none")
            {
                n = n - 1;
            }
        }

        PlayerPrefs.SetString("n", n.ToString());
        badge_name.text = badge_names[n];
        badge_img.sprite = badge_sprite[n];


    }





}

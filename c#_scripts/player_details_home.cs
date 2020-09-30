using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

// 
public class player_details_home : MonoBehaviour
{
    GameObject startbutton;
    Navigation customNav = new Navigation();
    // Start is called before the first frame update
    void Start()
    {



        customNav.mode = Navigation.Mode.None;
        startbutton = GameObject.Find("start");
        const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
        string targetUrl = DEFAULT_URL;
        string data;
        WWWForm form = new WWWForm();
        form.AddField("player_info", PlayerPrefs.GetString("id"));
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();

        while (true)
        {
            data = request.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        }
        string[] details = data.Split('&');
        PlayerPrefs.SetString("username", details[0]);
        PlayerPrefs.SetString("games_won", details[1]);
        PlayerPrefs.SetString("games_lost", details[2]);
        PlayerPrefs.SetString("games_drawn", details[3]);
        PlayerPrefs.SetString("level", details[4]);
        PlayerPrefs.SetString("exp", details[5]);
        PlayerPrefs.SetString("badge", details[6]);
        PlayerPrefs.SetString("monsters_slayed", details[7]);
        PlayerPrefs.SetString("gold", details[8]);
        PlayerPrefs.SetString("diamond", details[9]);
        PlayerPrefs.SetString("chest_info", details[10]);
        PlayerPrefs.SetString("emotes_owned", details[11]);
        PlayerPrefs.SetString("START", details[12]);
        string[] sevrer_date_time = calc_array_date_time(details[13]);
        var date = System.DateTime.Now;
        int year = Int32.Parse(date.Year.ToString());
        int month = Int32.Parse(date.Month.ToString());
        int date_ = Int32.Parse(date.Day.ToString());
        int minutes = Int32.Parse(date.Minute.ToString());
        int hours = Int32.Parse(date.Hour.ToString());
        int seconds = Int32.Parse(date.Second.ToString());
        System.DateTime a = new System.DateTime(Int32.Parse(sevrer_date_time[2]), Int32.Parse(sevrer_date_time[0]), Int32.Parse(sevrer_date_time[1]) , Int32.Parse(sevrer_date_time[3]), Int32.Parse(sevrer_date_time[4]), Int32.Parse(sevrer_date_time[5]));
        System.DateTime b = new System.DateTime(year, month, date_, hours, minutes, seconds);

        PlayerPrefs.SetString("time_diff", (b.Subtract(a).TotalMinutes*60).ToString());


        startbutton = GameObject.Find("start");
        startbutton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("START");

        if (PlayerPrefs.GetString("START") == "CANCEL")
        {

            ColorBlock cb = startbutton.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(244, 67, 54, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb;
            ColorBlock cb_ = startbutton.GetComponentInChildren<Button>().colors;
            cb_.highlightedColor = new Color32(255, 121, 97, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb_;
        }
        else
        {
            ColorBlock cb = startbutton.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(255, 255, 0, 255);
            cb.highlightedColor = new Color32(255, 255, 90, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb;

        }
    }

    // Update is called once per frame
    void Update()
    {
        Text username = GameObject.Find("username").GetComponent<Text>();
        Text level = GameObject.Find("level").GetComponent<Text>();
        Text gold = GameObject.Find("gold").GetComponent<Text>();
        Text diamond = GameObject.Find("diamond").GetComponent<Text>();
        string player_username = PlayerPrefs.GetString("username");
        string player_level = PlayerPrefs.GetString("level");
        string gold_ = PlayerPrefs.GetString("gold");
        string diamond_ = PlayerPrefs.GetString("diamond");

        username.text = player_username;
        level.text = player_level;
        gold.text = gold_;
        diamond.text = diamond_;


    }
    public string[] calc_array_date_time(string info)
    {
        string[] splitted_data = info.Split(' ');
        string[] date = splitted_data[0].Split('/');
        string[] time = splitted_data[1].Split(':');
        var myList = new List<string>();

        myList.AddRange(date);
        myList.AddRange(time);

        return myList.ToArray();
    }
}


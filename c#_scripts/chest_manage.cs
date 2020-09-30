using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class chest_manage : MonoBehaviour
{
    string targetUrl = "https://thekeres.herokuapp.com/";
    public Sprite normal_chest;
    public Sprite king_chest;
    public Sprite empty;
    string chest_info;
    Image chest1, chest2, chest3, chest4, chest5;
    GameObject chest_1_button, chest_2_button, chest_3_button, chest_4_button, chest_5_button;
    Text chest_1_text, chest_2_text, chest_3_text, chest_4_text, chest_5_text;
    double normal_chest_unlock_time = 5;
    double king_chest_unlock_time = 5;
    public List<Image> chest_img = new List<Image>();
    public List<Text> chest_time_text = new List<Text>();
    public List<GameObject> button_cmd = new List<GameObject>();
    Navigation customNav = new Navigation();
    // Start is called before the first frame update
    void Start()
    {
        customNav.mode = Navigation.Mode.None;

        chest1 = GameObject.Find("chest1_icon").GetComponent<Image>();
        chest2 = GameObject.Find("chest2_icon").GetComponent<Image>();
        chest3 = GameObject.Find("chest3_icon").GetComponent<Image>();
        chest4 = GameObject.Find("chest4_icon").GetComponent<Image>();
        chest5 = GameObject.Find("chest5_icon").GetComponent<Image>();

        chest_1_button = GameObject.Find("chest1_cmd");
        chest_2_button = GameObject.Find("chest2_cmd");
        chest_3_button = GameObject.Find("chest3_cmd");
        chest_4_button = GameObject.Find("chest4_cmd");
        chest_5_button = GameObject.Find("chest5_cmd");

        button_cmd.Add(chest_1_button);
        button_cmd.Add(chest_2_button);
        button_cmd.Add(chest_3_button);
        button_cmd.Add(chest_4_button);
        button_cmd.Add(chest_5_button);

        chest_1_text = GameObject.Find("chest1_time").GetComponent<Text>();
        chest_2_text = GameObject.Find("chest2_time").GetComponent<Text>();
        chest_3_text = GameObject.Find("chest3_time").GetComponent<Text>();
        chest_4_text = GameObject.Find("chest4_time").GetComponent<Text>();
        chest_5_text = GameObject.Find("chest5_time").GetComponent<Text>();


        chest_img.Add(chest1);
        chest_img.Add(chest2);
        chest_img.Add(chest3);
        chest_img.Add(chest4);
        chest_img.Add(chest5);



        chest_time_text.Add(chest_1_text);
        chest_time_text.Add(chest_2_text);
        chest_time_text.Add(chest_3_text);
        chest_time_text.Add(chest_4_text);
        chest_time_text.Add(chest_5_text);

    }

    // Update is called once per frame
    void Update()
    {


        string chest_info = PlayerPrefs.GetString("chest_info");

        chest_info = PlayerPrefs.GetString("chest_info");

        string[] chest_data_split = chest_info.Split('_');
        string[] chest_state = chest_data_split[0].Split(',');
        string[] chest_data_each_details = chest_data_split[1].Split(',');
        string[] chest_time = chest_data_split[2].Split(',');

        double time_diff = Int32.Parse(PlayerPrefs.GetString("time_diff"));


        for (int i = 0; i <= 4; i++)
        {
            button_cmd[i].GetComponentInChildren<Button>().navigation = customNav;
            if (chest_state[i] == "0")
            {
                Color c = chest_img[i].color;
                c.a = 0;
                chest_img[i].color=c;
                button_cmd[i].GetComponentInChildren<Text>().text = " ";
                button_cmd[i].SetActive(false);
            }

            else if (chest_state[i] == "1" || chest_state[i] == "2")
            {
                button_cmd[i].SetActive(true);
                if (chest_data_each_details[i] == "n")
                {

                    ColorBlock cb = button_cmd[i].GetComponentInChildren<Button>().colors;
                    cb.normalColor = new Color32(255, 64, 129, 255);
                    button_cmd[i].GetComponentInChildren<Button>().colors = cb;

                    button_cmd[i].GetComponentInChildren< Text >().text = "START";
                }
                else if (chest_data_each_details[i] == "0")
                {
                    button_cmd[i].GetComponentInChildren< Text >().text = " ";
                }
                else if (chest_data_each_details[i]== "p")
                {
                    string[] sevrer_date_time = calc_array_date_time(chest_time[i]);
                    System.DateTime a = new System.DateTime(Int32.Parse(sevrer_date_time[2]), Int32.Parse(sevrer_date_time[0]), Int32.Parse(sevrer_date_time[1]), Int32.Parse(sevrer_date_time[3]), Int32.Parse(sevrer_date_time[4]), Int32.Parse(sevrer_date_time[5]));
                    double remaining_time = 0;
                    if (chest_state[i] == "1")
                    {
                         remaining_time = normal_chest_unlock_time - calc_remaining_time(a, time_diff);
                    }
                    else if (chest_state[i] == "2")
                    {
                         remaining_time = king_chest_unlock_time - calc_remaining_time(a, time_diff);
                    }


                    if (remaining_time <= 0)
                    {
                        chest_rdy_to_unlock(i);
                        ColorBlock cb = button_cmd[i].GetComponentInChildren<Button>().colors;
                        cb.normalColor = new Color32(255, 208, 45, 255);
                        button_cmd[i].GetComponentInChildren<Button>().colors = cb;
                        ColorBlock cb_ = button_cmd[i].GetComponentInChildren<Button>().colors;
                        cb_.highlightedColor = new Color32(255, 208, 104, 255);
                        button_cmd[i].GetComponentInChildren<Button>().colors = cb_;
                        button_cmd[i].GetComponentInChildren<Text>().text = "OPEN";
                        chest_time_text[i].text = " ";

                    }
                    else
                    {
                        ColorBlock cb = button_cmd[i].GetComponentInChildren<Button>().colors;
                        cb.normalColor = new Color32(255, 157, 176, 255);
                        button_cmd[i].GetComponentInChildren<Button>().colors = cb;
                        ColorBlock cb_ = button_cmd[i].GetComponentInChildren<Button>().colors;
                        cb_.highlightedColor = new Color32(255, 141, 176, 255);
                        button_cmd[i].GetComponentInChildren<Button>().colors = cb_;
                        button_cmd[i].GetComponentInChildren<Text>().text = "WAIT";
                       
                        TimeSpan time = TimeSpan.FromSeconds(remaining_time);

                        string str = time.ToString(@"hh\:mm\:ss");
                       
                        chest_time_text[i].text = str;

                    }

                }
                else if (chest_data_each_details[i] == "d")
                {
                    ColorBlock cb = button_cmd[i].GetComponentInChildren<Button>().colors;
                    cb.normalColor = new Color32(255, 208, 45, 255);
                    button_cmd[i].GetComponentInChildren<Button>().colors = cb;
                    ColorBlock cb_ = button_cmd[i].GetComponentInChildren<Button>().colors;
                    cb_.highlightedColor = new Color32(255, 208, 104, 255);
                    button_cmd[i].GetComponentInChildren<Button>().colors = cb_;
                    button_cmd[i].GetComponentInChildren< Text >().text = "OPEN";

                }
                if (chest_state[i] == "1")
                {
                    chest_img[i].sprite = normal_chest;
                }
                else if (chest_state[i] == "2")
                {
                    chest_img[i].sprite = king_chest;
                }
            }


        };





    }

    public void start_chest(int n)
    {
        if (button_cmd[n].GetComponentInChildren<Text>().text == "START")
        {

            int time_diff = Int32.Parse(PlayerPrefs.GetString("time_diff"));
            string chest_info = PlayerPrefs.GetString("chest_info");

            const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
            string targetUrl = DEFAULT_URL;
            string data;
            WWWForm form = new WWWForm();
            form.AddField("chest_update", n);
            form.AddField("id", PlayerPrefs.GetString("id"));
            form.AddField("action", "start");
            var request = UnityWebRequest.Post(targetUrl, form);
            request.SendWebRequest();

            WWWForm form_reset = new WWWForm();
            form_reset.AddField("player_info", PlayerPrefs.GetString("id"));
            var request_reset = UnityWebRequest.Post(targetUrl, form_reset);
            request_reset.SendWebRequest();

            while (true)
            {
                data = request_reset.downloadHandler.text;
                if (data.Length > 0)
                {
                    break;
                }

            }
            string[] details = data.Split('&');
            PlayerPrefs.SetString("chest_info", details[10]);

        }
        else if (button_cmd[n].GetComponentInChildren<Text>().text == "OPEN")
        {

            remove_chest(n);

        }
    }

    public double calc_remaining_time(System.DateTime start_time,double time_diff)
    {
        var date = System.DateTime.Now;
        int year = Int32.Parse(date.Year.ToString());
        int month = Int32.Parse(date.Month.ToString());
        int date_ = Int32.Parse(date.Day.ToString());
        int minutes = Int32.Parse(date.Minute.ToString());
        int hours = Int32.Parse(date.Hour.ToString());
        int seconds = Int32.Parse(date.Second.ToString());

        System.DateTime system_time = new System.DateTime(year, month, date_, hours, minutes, seconds);
        return (system_time.Subtract(start_time).TotalMinutes * 60) - time_diff;
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

    public void chest_rdy_to_unlock(int chest_n)
    {
        button_cmd[chest_n].GetComponentInChildren<Button>().interactable = true;
        const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
        string targetUrl = DEFAULT_URL;
        string data;
        WWWForm form = new WWWForm();
        form.AddField("chest_update", chest_n);
        form.AddField("id", PlayerPrefs.GetString("id"));
        form.AddField("action", "open");
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();


        WWWForm form_reset = new WWWForm();
        form_reset.AddField("player_info", PlayerPrefs.GetString("id"));
        var request_reset = UnityWebRequest.Post(targetUrl, form_reset);
        request_reset.SendWebRequest();

        while (true)
        {
            data = request_reset.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        }
        string[] details = data.Split('&');
        PlayerPrefs.SetString("chest_info", details[10]);
    }

    public void remove_chest(int chest_n)
    {

        string chest_info = PlayerPrefs.GetString("chest_info");

        chest_info = PlayerPrefs.GetString("chest_info");
        string[] chest_data_split = chest_info.Split('_');
        string[] chest_state = chest_data_split[0].Split(',');
        string[] chest_data_each_details = chest_data_split[1].Split(',');
        string[] chest_time = chest_data_split[2].Split(',');


        button_cmd[chest_n].GetComponentInChildren<Button>().interactable = true;
        const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
        string targetUrl = DEFAULT_URL;
        string data;
        WWWForm form = new WWWForm();
        form.AddField("chest_update", chest_n);
        form.AddField("id", PlayerPrefs.GetString("id"));
        form.AddField("action", "remove");
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();


        WWWForm form_reset = new WWWForm();
        form_reset.AddField("player_info", PlayerPrefs.GetString("id"));
        var request_reset = UnityWebRequest.Post(targetUrl, form_reset);
        request_reset.SendWebRequest();

        while (true)
        {
            data = request_reset.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        }
        string[] details = data.Split('&');
        PlayerPrefs.SetString("chest_info", details[10]);
        if (chest_state[chest_n] == "1")
        {
            PlayerPrefs.SetString("chest_type", "normal");
        }
        else
        {
            PlayerPrefs.SetString("chest_type", "king");
        }
        SceneManager.LoadScene("chest_loot");
    }

}

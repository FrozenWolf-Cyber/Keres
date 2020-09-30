using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class shop_updater : MonoBehaviour
{
    Text gold, diamond;
    public Sprite gold_icon, diamond_icon;
    string[] emotes_owned;
     List<Image> item = new List<Image>();
     List<Image> item_selected = new List<Image>();
     List<Text> item_cost = new List<Text>();
     List<GameObject> item_buy = new List<GameObject>();
    List<Image> currency = new List<Image>();
    List<Image> item_owned = new List<Image>();
   
    string[] price_each ;


    // Start is called before the first frame update
    void Start()
    {


        for (int i = 1; i<=8; i++)
        {
            item_selected.Add(GameObject.Find("selected" + i.ToString()).GetComponent<Image>());
            item_buy.Add(GameObject.Find("itembuy" + i.ToString()));
            item_owned.Add(GameObject.Find("item_owned" + i.ToString()).GetComponent<Image>());
            item_cost.Add(GameObject.Find("itemcost" + i.ToString()).GetComponent<Text>());
            item.Add(GameObject.Find("item" + i.ToString()).GetComponent<Image>());
            currency.Add(GameObject.Find("currency" + (i-1).ToString()).GetComponent<Image>());
        }




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
        PlayerPrefs.SetString("gold", details[8]);
        PlayerPrefs.SetString("diamond", details[9]);
        PlayerPrefs.SetString("emotes_owned", details[11]);

        emotes_owned = details[11].Split(',');

        gold = GameObject.Find("gold").GetComponent<Text>();
        diamond = GameObject.Find("diamond").GetComponent<Text>();

        WWWForm form_ = new WWWForm();
        form_.AddField("emote_cost", " ");
        var request_ = UnityWebRequest.Post(targetUrl, form_);
        request_.SendWebRequest();

        while (true)
        {
            data = request_.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        }

        price_each = data.Split(',');

    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i <= 7; i++)
        {
            if (emotes_owned[i] == "1")
            {

                var tempColor = item_owned[i].color;
                tempColor.a = 1f;
                item_owned[i].color = tempColor;

            }

            else
            {
                var tempColor = item_owned[i].color;
                tempColor.a = 0f;
                item_owned[i].color = tempColor;

            }
        }

        gold = GameObject.Find("gold").GetComponent<Text>();
        diamond = GameObject.Find("diamond").GetComponent<Text>();
        string gold_ = PlayerPrefs.GetString("gold");
        string diamond_ = PlayerPrefs.GetString("diamond");
        gold.text = gold_;
        diamond.text = diamond_;

        for (int i = 0; i < 8; i++)
        {

            if (price_each[i].Substring(price_each[i].Length - 1) == "c")
            {
                currency[i].sprite = gold_icon;
            }
            else
            {
                currency[i].sprite = diamond_icon;
            }

            item_cost[i].text = price_each[i].Substring(0, price_each[i].Length - 1);
        }



    }

    public void back_to_homescreen()
    {
        SceneManager.LoadScene("player_home");

    }

    public void buy(int button_n)
    {
        string cost_c = "0";
        string cost_d = "0";
        int cost_comapre1 = 0;
        int cost_comapre2 = 0;
        if (price_each[button_n].Substring(price_each[button_n].Length - 1) == "c")
        {
            cost_c = price_each[button_n].Substring(0, price_each[button_n].Length - 1);
            cost_comapre1 = Int32.Parse(cost_c);
            cost_comapre2 = Int32.Parse(PlayerPrefs.GetString("gold"));
        }
        else
        {
            cost_d = price_each[button_n].Substring(0, price_each[button_n].Length - 1);
            cost_comapre1 = Int32.Parse(cost_d);
            cost_comapre2 = Int32.Parse(PlayerPrefs.GetString("diamond"));
        }

        
        if (item_buy[button_n].GetComponentInChildren<Text>().text == "BUY" && cost_comapre2>=cost_comapre1)
        {
            const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
            string targetUrl = DEFAULT_URL;
            string data;
            WWWForm form = new WWWForm();
            form.AddField("emote_buy", button_n.ToString() + '&'+PlayerPrefs.GetString("id"));
            var request = UnityWebRequest.Post(targetUrl, form);
            request.SendWebRequest();


            WWWForm form_ = new WWWForm();
            form_.AddField("player_res_gained", "-"+cost_c+ "&" + "-"+cost_d);
            form_.AddField("id", PlayerPrefs.GetString("id"));
            var request_ = UnityWebRequest.Post(targetUrl, form_);
            request_.SendWebRequest();

            Start();
        }

    }

}


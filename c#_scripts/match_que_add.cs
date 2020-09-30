using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class match_que_add : MonoBehaviour
{
    GameObject startbutton;
    Navigation customNav = new Navigation();
    float period = 0;
    // Start is called before the first frame update
    void Start()
    {
        customNav.mode = Navigation.Mode.None;
        startbutton = GameObject.Find("start");
        startbutton.GetComponentInChildren<Button>().navigation = customNav;
        customNav.mode = Navigation.Mode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (startbutton.GetComponentInChildren<Text>().text == "CANCEL")
        {

            if (period >= 2)
            {
                const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
                string targetUrl = DEFAULT_URL;
                string data;
                WWWForm form = new WWWForm();
                form.AddField("check_for_match", PlayerPrefs.GetString("id"));
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

                if (data != "0")
                {
                    PlayerPrefs.SetString("room", data);
                    startbutton.GetComponentInChildren<Text>().text = "START";
                    SceneManager.LoadScene("match_openning");

                }

                period = 0;
            }
            else
            {
                period = period += UnityEngine.Time.deltaTime;
            }
        }
    }

    public void add_to_que()
    {
        if (startbutton.GetComponentInChildren<Text>().text == "START")
        {
            period = 0;
            const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
            string targetUrl = DEFAULT_URL;
            string data;
            WWWForm form = new WWWForm();
            form.AddField("match_making", PlayerPrefs.GetString("id") + '&' + PlayerPrefs.GetString("level") + '&' + PlayerPrefs.GetString("games_won") + '&' + PlayerPrefs.GetString("games_lost") + '&' + PlayerPrefs.GetString("games_drawn"));
            var request = UnityWebRequest.Post(targetUrl, form);
            request.SendWebRequest();


            startbutton.GetComponentInChildren<Text>().text = "CANCEL";
            ColorBlock cb = startbutton.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(244, 67, 54, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb;
            ColorBlock cb_ = startbutton.GetComponentInChildren<Button>().colors;
            cb_.highlightedColor = new Color32(255, 121, 97, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb_;

            PlayerPrefs.SetString("START", "CANCEL");
        }
        else
        {
            period = 0;
            ColorBlock cb = startbutton.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(255, 255, 0, 255);
            cb.highlightedColor = new Color32(255, 255, 90, 255);
            startbutton.GetComponentInChildren<Button>().colors = cb;
            startbutton.GetComponentInChildren<Text>().text = "START";

            const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
            string targetUrl = DEFAULT_URL;
            string data;
            WWWForm form_ = new WWWForm();
            form_.AddField("remove", PlayerPrefs.GetString("id"));
            var request_ = UnityWebRequest.Post(targetUrl, form_);
            request_.SendWebRequest();

            PlayerPrefs.SetString("START", "START");
        }

    }


}

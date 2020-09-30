using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class game_initializer : MonoBehaviour
{
    Text player1, player2, playerlevel1, playerlevel2;
    // Start is called before the first frame update
    void Start()
    {

        const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
        string targetUrl = DEFAULT_URL;
        string data;
        WWWForm form = new WWWForm();
        form.AddField("opp_id", PlayerPrefs.GetString("room")+ '&'+PlayerPrefs.GetString("id"));
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();

        while (true)
        {
            data = request.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        };

        string opp_id = data;

        WWWForm form_ = new WWWForm();

        form_.AddField("player_info", opp_id);
        var request_ = UnityWebRequest.Post(targetUrl, form_);
        request_.SendWebRequest();

        while (true)
        {
            data = request_.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }

        };

        

        string[] dataeach = data.Split('&');

        player1 = GameObject.Find("player1").GetComponent<Text>();
        player2 = GameObject.Find("player2").GetComponent<Text>();
        playerlevel1 = GameObject.Find("playerlevel1").GetComponent<Text>();
        playerlevel2 = GameObject.Find("playerlevel2").GetComponent<Text>();

        player2.text = dataeach[0];
        playerlevel2.text = dataeach[1];
        player1.text = PlayerPrefs.GetString("username");
        playerlevel1.text = PlayerPrefs.GetString("level");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

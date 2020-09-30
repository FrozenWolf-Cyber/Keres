using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class check_login_details : MonoBehaviour
{
    const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
    string targetUrl = DEFAULT_URL;
    string data;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void send_details()
    {

        InputField mailid = GameObject.Find("username").GetComponent<InputField>();
        InputField password = GameObject.Find("password").GetComponent<InputField>();
        Text username_error = GameObject.Find("error_username").GetComponent<Text>();
        Text password_error = GameObject.Find("error_password").GetComponent<Text>();

        string mailid_ = mailid.text;
        string password_ = password.text;


        string response = check_username_password(mailid_, password_);

        if (response.Length > 1)
        {
        
            PlayerPrefs.SetString("id",response);
            collect_data(response);
        }
        else
        {
            Debug.Log(response);
            if (response == "3")
            {

                username_error.text = "Username doesn't exist";   
            }
            else
            {
                username_error.text = " ";
            }

            if (response == "0")
            {

                password_error.text = "Incorrect password";
            }
            else
            {
                password_error.text = " ";
            }
        }


    }

    public string check_username_password(string username, string password)
    {
        string details = username + "&" + password;
        WWWForm form = new WWWForm();
        form.AddField("login", details);
        if (username.Substring(username.Length - 4) == ".com")
        {
            form.AddField("type_of_login", "mailid");
        }
        else
        {
            form.AddField("type_of_login", "username");
        }
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

        return data;


    }

    public void collect_data(string user_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_info", user_id);
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
        PlayerPrefs.SetString("Change", " ");
        PlayerPrefs.SetString("date", " ");
        PlayerPrefs.SetString("month", " ");
        PlayerPrefs.SetString("gold", details[8]);
        PlayerPrefs.SetString("diamond", details[9]);
        PlayerPrefs.SetString("chest_info", details[10]);
        PlayerPrefs.SetString("emotes_owned", details[11]);
        PlayerPrefs.SetString("id", user_id);
        PlayerPrefs.SetString("year", " ");
        PlayerPrefs.SetString("gender", " ");
        PlayerPrefs.SetString("name", " ");
        PlayerPrefs.SetString("mailid", " ");
        PlayerPrefs.SetString("password", " ");
        PlayerPrefs.SetString("login", "true");
        SceneManager.LoadScene("player_home");

    }

}

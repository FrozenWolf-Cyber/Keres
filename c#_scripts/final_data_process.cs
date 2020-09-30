using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class final_data_process : MonoBehaviour
{
    string targetUrl = "https://thekeres.herokuapp.com/";
    string data;


    // Start is called before the first frame update
    void Start()
    {
        Text username_error = GameObject.Find("username_error").GetComponent<Text>();
        Text password_error = GameObject.Find("password_error").GetComponent<Text>();
        Text confirm_password_error = GameObject.Find("confirm_password_error").GetComponent<Text>();
        PlayerPrefs.SetString("Change", "False");
        PlayerPrefs.SetString("username_error", " ");
        PlayerPrefs.SetString("password_error", " ");
        PlayerPrefs.SetString("confirm_password_error", " ");
        Debug.Log(PlayerPrefs.GetString("password_error") );

    }

    // Update is called once per frame
    void Update()
    {

        Text tt = GameObject.Find("tt").GetComponent<Text>();
        Text username_error = GameObject.Find("username_error").GetComponent<Text>();
        Text password_error = GameObject.Find("password_error").GetComponent<Text>();
        Text confirm_password_error = GameObject.Find("confirm_password_error").GetComponent<Text>();


        if (PlayerPrefs.GetString("password_error") == " ")
        {

            password_error.text = " ";
            password_error.color = Color.black;

        }
        if (PlayerPrefs.GetString("password_error") != " ")
        {

            password_error.text = PlayerPrefs.GetString("password_error");
            password_error.color = Color.red;
        }

        if (PlayerPrefs.GetString("username_error") == " ")
        {
            username_error.text = " ";
            username_error.color = Color.black;
        }
        if (PlayerPrefs.GetString("username_error") != " ")
        {
            username_error.text = PlayerPrefs.GetString("username_error");
            username_error.color = Color.red;
        }

        if (PlayerPrefs.GetString("confirm_password_error") == " ")
        {
            confirm_password_error.text = " ";
            confirm_password_error.color = Color.black;
        }
        if (PlayerPrefs.GetString("confirm_password_error") != " ")
        {

            confirm_password_error.text = PlayerPrefs.GetString("confirm_password_error");
            confirm_password_error.color = Color.red;
        }


    }



    public void final_data()
    {
        Text tt = GameObject.Find("tt").GetComponent<Text>();

        InputField confirm_password = GameObject.Find("confirm_password").GetComponent<InputField>();
        InputField password = GameObject.Find("password").GetComponent<InputField>();
        InputField username = GameObject.Find("username").GetComponent<InputField>();
        Text username_error = GameObject.Find("username_error").GetComponent<Text>();
        Text password_error = GameObject.Find("password_error").GetComponent<Text>();
        Text confirm_password_error = GameObject.Find("confirm_password_error").GetComponent<Text>();



        string username_ = username.text;
        string password_ = password.text; 
        string confirm_password_ = confirm_password.text;



        if (username_.Length > 0 && password_.Length >= 8)
        {
            PlayerPrefs.SetString("password_error", " ");


            if (password_ == confirm_password_)
            {
                PlayerPrefs.SetString("confirm_password_error", " ");


                PlayerPrefs.SetString("username", username_);
                PlayerPrefs.SetString("password", password_);

                if (check_for_username(username_))
                {
                    string resposne = send_data();
                    SceneManager.LoadScene("login");
                    Debug.Log("SIGNUP COMPLETED");
                }
                else
                {
                    PlayerPrefs.SetString("username_error", "Username was already taken ");

                }
            }
            else
            {
                PlayerPrefs.SetString("confirm_password_error", "Doesn't match with your password");


            }



        }


        else
        {
            if (username_.Length == 0)
            {
                PlayerPrefs.SetString("username_error", "Please fill the username");


            }
            else
            {
                PlayerPrefs.SetString("username_error", " ");

            }

            if (password_.Length < 8)
            {
                PlayerPrefs.SetString("password_error", "Your password should have atleast 8 char ");


            }
            else
            {
                PlayerPrefs.SetString("password_error", " ");

            }
        }
    }

    public bool check_for_username(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("check_username", username);
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
        Debug.Log(data);
        if (data == "True")
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    public string send_data()
    {
        string date = PlayerPrefs.GetString("date");
        string month = PlayerPrefs.GetString("month");
        string year = PlayerPrefs.GetString("year");
        string name = PlayerPrefs.GetString("name");
        string gender = PlayerPrefs.GetString("gender");
        string mailid = PlayerPrefs.GetString("mailid");
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");
        WWWForm form = new WWWForm();
        string data_signup = mailid + '&' + username + '&' + password + '&' + name + '&' + date + '/' + month + '/' + year;
        Debug.Log(data_signup);
        form.AddField("signup", data_signup);
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
}

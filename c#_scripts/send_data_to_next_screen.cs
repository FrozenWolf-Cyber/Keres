using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class send_data_to_next_screen : MonoBehaviour
{
    string data;
    // Where to send our request
    const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
    string targetUrl = DEFAULT_URL;
    // Keep track of what we got back
    string recentData = "";
    void Start()
    {
        PlayerPrefs.SetString("Change", "False");
    }

    void Update()
    {

    }

    public void SavePlayer()

{
        InputField date = GameObject.Find("date").GetComponent<InputField>();
        InputField month = GameObject.Find("month").GetComponent<InputField>();
        InputField year = GameObject.Find("year").GetComponent<InputField>();
        InputField mailid = GameObject.Find("mailid").GetComponent<InputField>();
        Dropdown gender = GameObject.Find("gender").GetComponent<Dropdown>();
        InputField name = GameObject.Find("name").GetComponent<InputField>();
        Text new_ = GameObject.Find("new").GetComponent<Text>();
        Text mail_id_error = GameObject.Find("maild_id_error").GetComponent<Text>();

        string date_ = date.text;
        string month_ = month.text;
        string year_ = year.text;
        string gender_ = gender.options[gender.value].text;
        string name_ = name.text;
        string mailid_ = mailid.text;
        bool check = check_for_mail(mailid_);
        if (date_ == "Date" || month_ == "Month" || year_ == "Year" || gender_ == "Gender" || name_.Length <= 0  || mailid_.Length <= 0 && !check)
        {
            PlayerPrefs.SetString("Change", "False");
            if (!check)
            {
                mail_id_error.text = "This mailid is already been registered";
            }
            else
            {
                mail_id_error.text = " ";
            }
        }

        else
        {
            if (check_for_mail(mailid_))
            {

                string data = "True" + date_ + "&" + month_ + "&" + year_ + "&" + gender_ + "&" + name_ + "&" + mailid_;
                PlayerPrefs.SetString("Change", "True");
                PlayerPrefs.SetString("date", date_);
                PlayerPrefs.SetString("month", month_);
                PlayerPrefs.SetString("year", year_);
                PlayerPrefs.SetString("gender", gender_);
                PlayerPrefs.SetString("name", name_);
                PlayerPrefs.SetString("mailid", mailid_);

                SceneManager.LoadScene("sign_up_2");

            }
        }




    }

    public bool check_for_mail(string mail)
    {

        WWWForm form = new WWWForm();
        form.AddField("check_mailid", mail);
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();
        while (true){
            data = request.downloadHandler.text;
            if (data.Length > 0)
            {
                break;
            }
        }
        if (data == "True")
        {
            return true;
        }
        else {
            return false;

        }

    }











}





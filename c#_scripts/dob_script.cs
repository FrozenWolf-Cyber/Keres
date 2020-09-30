using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dob_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void date()
    {
        InputField date = GameObject.Find("date").GetComponent<InputField>();
        string date_entered = date.text;


        if (date_entered.Length==2)
        {

            if (!int.TryParse(date_entered.Substring(1, 1), out int n))
            {
                date.text = date_entered.Substring(0, 1);
            }
        }

        if (date_entered.Length == 1)
        {
            if (!int.TryParse(date_entered.Substring(0, 1), out int n))
            {
                date.text = "";
            }
        }

    }

    public void month()
    {
        InputField month = GameObject.Find("month").GetComponent<InputField>();

        string date_entered = month.text;


        if (date_entered.Length == 2)
        {

            if (!int.TryParse(date_entered.Substring(1, 1), out int n))
            {
                month.text = date_entered.Substring(0, 1);
            }
        }

        if (date_entered.Length == 1)
        {
            if (!int.TryParse(date_entered.Substring(0, 1), out int n))
            {
                month.text = "";
            }
        }



    }

    public void year()
    {
        InputField year = GameObject.Find("year").GetComponent<InputField>();

        string date_entered = year.text;


        if (date_entered.Length == 2)
        {

            if (!int.TryParse(date_entered.Substring(1, 1), out int n))
            {
                year.text = date_entered.Substring(0, 1);
            }
        }

        if (date_entered.Length == 1)
        {

            if (!int.TryParse(date_entered.Substring(0, 1), out int n))
            {
                year.text = "";
            }
        }

        if (date_entered.Length == 3)
        {

            if (!int.TryParse(date_entered.Substring(1, 2), out int n))
            {
                year.text = date_entered.Substring(0, 2);
            }
        }

        if (date_entered.Length == 4)
        {
            if (!int.TryParse(date_entered.Substring(2, 2), out int n))
            {
                year.text = date_entered.Substring(0, 3);
            }
        }



    }
}

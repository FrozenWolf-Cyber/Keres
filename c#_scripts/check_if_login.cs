using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class check_if_login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text title = GameObject.Find("title").GetComponent<Text>();
        if (title.color.a==0)
        {
            if (PlayerPrefs.GetString("login") == "true")
            {
                SceneManager.LoadScene("player_home");
            }
            else
            {
                SceneManager.LoadScene("login");
            }
        }
    }
}

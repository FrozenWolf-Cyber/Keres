using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class send_to_signup : MonoBehaviour
{

    void Start()
    {

        PlayerPrefs.SetString("Change", "False");

    }



    public void sign_up_loader(string scene)
    {
        if (PlayerPrefs.GetString("Change") == "True" )
        {
            SceneManager.LoadScene(scene);

        }

    }
    public void login_to_sign_up(string scene)
    {

            SceneManager.LoadScene(scene);



    }



}

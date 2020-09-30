using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logout : MonoBehaviour
{
    public void logout_to_logout()
    {
        PlayerPrefs.SetString("username", " ");
        PlayerPrefs.SetString("games_won", " ");
        PlayerPrefs.SetString("games_lost", " ");
        PlayerPrefs.SetString("games_drawn", " ");
        PlayerPrefs.SetString("level", " ");
        PlayerPrefs.SetString("exp", " ");
        PlayerPrefs.SetString("gold", " ");
        PlayerPrefs.SetString("diamond", " ");
        PlayerPrefs.SetString("chest_info", " ");
        PlayerPrefs.SetString("id", " ");
        PlayerPrefs.SetString("monsters_slayed", " ");
        PlayerPrefs.SetString("login", "false");
        SceneManager.LoadScene("login");

    }
}

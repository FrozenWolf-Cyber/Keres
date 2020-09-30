using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_home : MonoBehaviour
{
    public void send_to_home()
    {
        SceneManager.LoadScene("player_home");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_page : MonoBehaviour
{

    public void switch_to_scene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

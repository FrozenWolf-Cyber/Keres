using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class apperance_coin : MonoBehaviour
{
    // Start is called before the first frame update
    public Image chest,gold_coin_bg,diaomond_bg,gold_coin,diamond,gold_icon,diamond_icon;
    public Text gold_loot, diamond_loot, you_got;
    public GameObject ok;
    public Sprite chest_openned;
    int coins_Q = 0 ;
    int diamond_Q = 0;
    void Start()
    {
        if (PlayerPrefs.GetString("chest_type") == "normal")
        {
            System.Random random = new System.Random();

            coins_Q = random.Next(35, 135);
            diamond_Q = random.Next(1, 3); 
        }
        else
        {
            System.Random random = new System.Random();

            coins_Q = random.Next(200, 535);
            diamond_Q = random.Next(7, 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (chest.sprite == chest_openned)
        {
            gold_icon = GameObject.Find("gold_icon").GetComponent<Image>();
            diamond_icon = GameObject.Find("diamond_icon").GetComponent<Image>();
            diaomond_bg = GameObject.Find("diamond_bg").GetComponent<Image>();
            gold_coin_bg = GameObject.Find("gold_bg").GetComponent<Image>();
            gold_loot.text = coins_Q.ToString();
            diamond_loot.text = diamond_Q.ToString();

            ColorBlock cb = ok.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(100, 221, 23, 255);
            ok.GetComponentInChildren<Button>().colors = cb;

            you_got.text = "YOU GOT :-";

            Color32 c = new Color32(238, 128, 252, 255);
            diaomond_bg.color =  c;
            Color c1 = new Color32(255, 188, 64, 255);

            ok.GetComponentInChildren<Text>().text = "CLAIM";
            gold_coin_bg.color =  c1;
            diamond_icon.color = new Color32(238, 255, 255, 255);
            gold_icon.color = new Color32(255, 255, 255, 255);


        }
        else
        {
            gold_icon = GameObject.Find("gold_icon").GetComponent<Image>();
            diamond_icon = GameObject.Find("diamond_icon").GetComponent<Image>();
            diaomond_bg = GameObject.Find("diamond_bg").GetComponent<Image>();
            gold_coin_bg = GameObject.Find("gold_bg").GetComponent<Image>();
            gold_loot.text = " ";
            diamond_loot.text = " ";
            ok.GetComponentInChildren<Text>().text = " ";
            ColorBlock cb = ok.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(100, 221, 23, 0);
            ok.GetComponentInChildren<Button>().colors = cb;

            you_got.text = " ";

            diaomond_bg.color = new Color32(238, 128, 252, 0);
            gold_coin_bg.color = new Color32(255, 188, 64, 0);
            diamond_icon.color = new Color32(238, 128, 252, 0);
            gold_icon.color = new Color32(255, 188, 64, 0);
        }




    }

    public void  chest_()
    {
        PlayerPrefs.SetString("chest_type", " ");

        const string DEFAULT_URL = "https://thekeres.herokuapp.com/";
        string targetUrl = DEFAULT_URL;
        string data;
        WWWForm form = new WWWForm();
        form.AddField("player_res_gained", coins_Q+"&"+diamond_Q);
        form.AddField("id", PlayerPrefs.GetString("id"));
        var request = UnityWebRequest.Post(targetUrl, form);
        request.SendWebRequest();





    }


}

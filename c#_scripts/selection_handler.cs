using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection_handler : MonoBehaviour
{
    public Sprite selected , unselected;
    List<Image> item = new List<Image>();
    List<Image> item_selected = new List<Image>();
    List<Text> item_cost = new List<Text>();
    List<GameObject> item_buy = new List<GameObject>();
    // Start is called before the first frame update
    Navigation customNav = new Navigation();
    void Start()
    {
        customNav.mode = Navigation.Mode.None;
        for (int i = 1; i <= 8; i++)
        {
            item_selected.Add(GameObject.Find("selected" + i.ToString()).GetComponent<Image>());
        }

        for (int i = 1; i <= 8; i++)
        {
            item.Add(GameObject.Find("item" + i.ToString()).GetComponent<Image>());
        }

        for (int i = 1; i <= 8; i++)
        {
            item_cost.Add(GameObject.Find("itemcost" + i.ToString()).GetComponent<Text>());
        }

        for (int i = 1; i <= 8; i++)
        {

            item_buy.Add(GameObject.Find("itembuy" + i.ToString()));
        }



        for (int i = 0; i < 8; i++)
        {
            item_selected[i].sprite = unselected;
        }

        for (int i = 0; i<8; i++)
        {
           item_selected[i].rectTransform.sizeDelta = new Vector2(110, 140);
        }

        for (int i = 0; i < 8; i++)
        {

            ColorBlock cb = item_buy[i].GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color32(255, 64, 129, 0);
            item_buy[i].GetComponentInChildren<Button>().colors = cb;
            item_buy[i].GetComponentInChildren<Text>().text = " ";

        }

    }
    //255,214,0,255
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            item_buy[i].GetComponentInChildren<Button>().navigation = customNav;
        }
    }

    public void select(int selected_n)
    {
        for (int i = 0; i <= 7; i++)
        {
            not_select(i);
        }

        string[] emotes_owned = PlayerPrefs.GetString("emotes_owned").Split(',');
        item_selected[selected_n].sprite = selected;
        item_selected[selected_n].rectTransform.sizeDelta = new Vector2(110, 172);
        ColorBlock cb = item_buy[selected_n].GetComponentInChildren<Button>().colors;
        cb.normalColor = new Color32(255, 214, 0, 255);
        item_buy[selected_n].GetComponentInChildren<Button>().colors = cb;
        item_buy[selected_n].GetComponentInChildren<Text>().text = "BUY";


        if (emotes_owned[selected_n] == "0")
        {
            item_buy[selected_n].GetComponentInChildren<Text>().text = "BUY";
            ColorBlock cb_ = item_buy[selected_n].GetComponentInChildren<Button>().colors;
            cb_.normalColor = new Color32(255, 214, 0, 255);
            item_buy[selected_n].GetComponentInChildren<Button>().colors = cb_;

            //255,214,0,255
        }
        else
        {
            item_buy[selected_n].GetComponentInChildren<Text>().text = "OWNED";
            ColorBlock cb_ = item_buy[selected_n].GetComponentInChildren<Button>().colors;
            cb_.normalColor = new Color32(129, 255, 0, 255);
            item_buy[selected_n].GetComponentInChildren<Button>().colors = cb_;
            //129,255,0,255
        }
        

    }

    public void not_select(int selected_n)
    {
        item_buy[selected_n].GetComponentInChildren<Text>().text = " ";
        item_selected[selected_n].sprite = unselected;
        item_selected[selected_n].rectTransform.sizeDelta = new Vector2(110, 140);
        ColorBlock cb = item_buy[selected_n].GetComponentInChildren<Button>().colors;
        cb.normalColor = new Color32(255, 214, 0, 0);
        item_buy[selected_n].GetComponentInChildren<Button>().colors = cb;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;
    [SerializeField] Button lv1;
    [SerializeField] Button lv2;
    [SerializeField] Button lv3;
    [SerializeField] Button lv4;
    [SerializeField] Button lv5;
    [SerializeField] Button lv6;
    [SerializeField] Button lv7;
    [SerializeField] Button lv8;
    [SerializeField] Button lv9;
    [SerializeField] Button lv10;
    private List<Button> buttonList = new List<Button>();


    private void Start()
    {
        buttonList.Add(lv1);
        buttonList.Add(lv2);
        buttonList.Add(lv3);
        buttonList.Add(lv4);
        buttonList.Add(lv5);
        buttonList.Add(lv6);
        buttonList.Add(lv7);
        buttonList.Add(lv8);
        buttonList.Add(lv9);
        buttonList.Add(lv10);

        for (int levelnum = 0; levelnum < buttonList.Count; levelnum++)
        {
            buttonList[levelnum].transition = buttonList[0].transition;
        }

    }
    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);

                for (int levelnum = 0; levelnum < buttonList.Count; levelnum++)
               {
               
                   if (i == levelnum && i <= PersistenceManager.GetLastUnlockedLevel()-1) { buttonList[levelnum].interactable = true; }
                   else { buttonList[levelnum].interactable = false; }
                 
               
               
                 }

                 
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

    }
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questPanel;
    public GameObject completedQuestPanel;
    public GameObject inventoryPanel;
    public GameObject[] questStyle;
    [Space]
    public static int questNumber = 3;
    public Text questNumText;
    [Space]
    public static int Money = 0;
    public Text moneyText;
    [Space]
    public static int mushroomAmount;
    public Text mushroomText;
    [Space]
    public Interact interact;
    [Space]
    public Canvas oldmanCanvas;
    private bool isInventoryON = false;

    void Start()
    {
        Interact interact = GetComponent<Interact>();
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
    }

    public void OpenCompletedQuest()
    {
        completedQuestPanel.SetActive(true);
    }

    public void CollectReward()
    {
        Movement.canMove = true;
        interact.image.enabled = false;
        interact.texts[4].enabled = false;
        interact.completedQuestButton.SetActive(false);
        questStyle[0].SetActive(false);
        completedQuestPanel.SetActive(false);
        Money += 1;
        oldmanCanvas.enabled = false;
        interact.missCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        questNumText.text = questNumber.ToString();
        moneyText.text = "$ " + Money.ToString();
        mushroomText.text = mushroomAmount.ToString();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isInventoryON)
            {
                InventoryIsOFF();
            }
            else
            {
                InventoryIsON();
            }
        }
    }

    public void InventoryIsON()
    {
        inventoryPanel.SetActive(true);
        isInventoryON = true;
    }

    public void InventoryIsOFF()
    {
        inventoryPanel.SetActive(false);
        isInventoryON = false;
    }
}

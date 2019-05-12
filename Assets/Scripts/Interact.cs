using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//this script can be found in the Component section under the option Intro RPG/Player/Interact
//[AddComponentMenu("Intro PRG/RPG/Player/Interact")]
public class Interact : MonoBehaviour
{
    #region Variables
    public Image image;
    public Text[] texts;
    [Space]
    public GameObject nextButton, acceptButton, declineButton, finishButton, completedQuestButton;
    public bool isButtonPressed = false;
    [Space]
    public QuestManager quest;
    [Space]
    public GameObject player;
    public GameObject[] items;
    public int itemsIndex;
    public GameObject NPC;
    public float range = 5f;
    [Space]
    public bool isQuestDone = false;
    public bool missCompleted = false;
    [Space]
    public GameObject[] tutorials;
    #endregion

    #region Start
    private void Start()
    {
        QuestManager quest = GetComponent<QuestManager>();
        items = GameObject.FindGameObjectsWithTag("Item");
        NPC = GameObject.FindGameObjectWithTag("NPC");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region Update
    public void Update()
    {
        //if our interact key is pressed
        if (Input.GetButtonDown("Interact"))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, range))
            {
                #region NPC tag
                //and that hits info is tagged NPC
                if (hitInfo.collider.CompareTag("NPC"))
                {

                    if (missCompleted == false)
                    {
                        nextButton.SetActive(true);
                        texts[0].enabled = true;
                        image.enabled = true;
                        Movement.canMove = false;
                        //Debug that we hit a NPC
                        Debug.Log("You are talking to NPC");

                        if (isQuestDone == true)
                        {
                            nextButton.SetActive(false);
                            texts[0].enabled = false;
                            Movement.canMove = false;
                            image.enabled = true;
                            texts[4].enabled = true;
                            completedQuestButton.SetActive(true);
                            QuestManager.mushroomAmount -= 3;
                        }
                    }                    
                }
                #endregion
                #region Item
                //and that hits info is tagged Item
                if (hitInfo.collider.CompareTag("Item"))
                {
                    DestroyItems();

                    if(QuestManager.questNumber == 0)
                    {
                        isQuestDone = true;
                    }

                    //Debug that we hit an Item
                    Debug.Log("It is an Item");
                }
                #endregion
            }
        }
    }
    #endregion

    #region Destroying Items
    public void DestroyItems()
    {
        if(isButtonPressed == true)
        {
            Destroy(items[itemsIndex]);

            if (QuestManager.questNumber > 0)
            {
                QuestManager.questNumber--;
            }
            QuestManager.mushroomAmount++;
            itemsIndex++;
            Debug.Log("An item is destroyed");
        }
    }
    #endregion

    #region Button Manager
    public void NextButton()
    {
        texts[0].enabled = false;
        texts[1].enabled = true;
        nextButton.SetActive(false);
        acceptButton.SetActive(true);
        declineButton.SetActive(true);
    }

    public void AcceptButton()
    {
        isButtonPressed = true;
        texts[1].enabled = false;
        texts[2].enabled = false;
        acceptButton.SetActive(false);
        declineButton.SetActive(false);
        finishButton.SetActive(false);
        quest.questPanel.SetActive(false);
        image.enabled = false;
        Movement.canMove = true;
        quest.questStyle[0].SetActive(true);
    }

    public void DeclineButton()
    {
        texts[1].enabled = false;
        texts[3].enabled = true;
        acceptButton.SetActive(false);
        declineButton.SetActive(false);
        finishButton.SetActive(true);
    }

    public void FinishButton()
    {
        texts[1].enabled = false;
        texts[2].enabled = false;
        texts[3].enabled = false;
        finishButton.SetActive(false);
        image.enabled = false;
        texts[4].enabled = false;
        Movement.canMove = true;
    }
    #endregion


}







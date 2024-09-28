using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PickingALock : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text wastedTime;
    [SerializeField] private Text hacked;

    [SerializeField] private GameObject winMessage;
    [SerializeField] private GameObject loseMessage;

    [SerializeField] private Text firstPinText;
    [SerializeField] private Text secondPinText;
    [SerializeField] private Text thirdPinText;

    [SerializeField] private Text drillText;
    [SerializeField] private Text lockpickText;
    [SerializeField] private Text hammerText;
    
    private int[] pins = new int[3];
    private int[] drill = new int[3];
    private int[] lockpick = new int[3];
    private int[] hammer = new int[3];

    Random random = new Random();
    float timer = 100;
    float tempTimer;
    int hackedCount;

    public void ClickButtonTools(string toolName)
    {
        if (toolName == "drill")
            UseTool(drill, toolName);         
        else if (toolName == "lockpick")
            UseTool(lockpick, toolName);
        else
            UseTool(hammer, toolName);
    }

    public void UseTool(int[] tool, string toolName)
    {
        ReturnToolColor();

        pins[0] += tool[0];
        pins[1] += tool[1];
        pins[2] += tool[2];

        if (!CheckRangePinsExceed(tool, toolName))
            ShowChangedPins();

        Win();
    }

    //public void ClickButtonDrill()
    //{
    //    AssignColor();

    //    pins[0] += drill[0];
    //    pins[1] += drill[1];
    //    pins[2] += drill[2];

    //    if (!CheckRangePinsExceed(drill, "drill"))
    //        ShowChangedPins();
       
    //    Win();
    //}

    //public void ClickButtonLockpick()
    //{
    //    AssignColor();

    //    pins[0] += lockpick[0];
    //    pins[1] += lockpick[1];
    //    pins[2] += lockpick[2];

    //    if (!CheckRangePinsExceed(lockpick, "lockpick"))
    //        ShowChangedPins();
        
    //    Win();
    //}

    //public void ClickButtonHammer()
    //{
    //    AssignColor();

    //    pins[0] += hammer[0];
    //    pins[1] += hammer[1];
    //    pins[2] += hammer[2];

    //    if (!CheckRangePinsExceed(hammer, "hammer"))
    //        ShowChangedPins();
        
    //    Win();
    //}
    
    private bool CheckRangePinsExceed(int[] tool, string toolName)
    {
        bool isRangeExceed = false; 

        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i] < 0 || pins[i] > 10)
            {
                isRangeExceed = true;
                pins[0] -= tool[0];
                pins[1] -= tool[1];
                pins[2] -= tool[2];

                if (i == 0)
                {
                    firstPinText.color = Color.red;

                    if (toolName == "drill")
                        drillText.text = $"<color=red>{drill[0]}</color> | {drill[1]} | {drill[2]}";
                    else if (toolName == "lockpick")
                        lockpickText.text = $"<color=red>{lockpick[0]}</color> | {lockpick[1]} | {lockpick[2]}";
                    else
                        hammerText.text = $"<color=red>{hammer[0]}</color> | {hammer[1]} | {hammer[2]}";
                }
                else if (i == 1)
                {
                    secondPinText.color = Color.red;

                    if (toolName == "drill")
                        drillText.text = $"{drill[0]} | <color=red>{drill[1]}</color> | {drill[2]}";
                    else if (toolName == "lockpick")
                        lockpickText.text = $"{lockpick[0]} | <color=red>{lockpick[1]}</color> | {lockpick[2]}";
                    else
                        hammerText.text = $"{hammer[0]} | <color=red>{hammer[1]}</color> | {hammer[2]}";
                }
                else
                {
                    thirdPinText.color = Color.red;

                    if (toolName == "drill")
                        drillText.text = $"{drill[0]} | {drill[1]} | <color=red>{drill[2]}</color>";
                    else if (toolName == "lockpick")
                        lockpickText.text = $"{lockpick[0]} | {lockpick[1]} | <color=red>{lockpick[2]}</color>";
                    else
                        hammerText.text = $"{hammer[0]} | {hammer[1]} | <color=red>{hammer[2]}</color>";
                }
                break;
            }
        }
        return isRangeExceed;
    }

    //private void AssignColor()
    //{
    //    firstPinText.color = Color.black;
    //    secondPinText.color = Color.black;
    //    thirdPinText.color = Color.black;

    //    ReturnToolColor();
    //}

    private void ShowChangedPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (i == 0)
                firstPinText.text = pins[i].ToString();
            else if (i == 1)
                secondPinText.text = pins[i].ToString();
            else
                thirdPinText.text = pins[i].ToString();
        }
    }

    private void FillInRandomly()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i] = random.Next(11);

            if (i == 0)
                firstPinText.text = pins[i].ToString();
            else if (i == 1)
                secondPinText.text = pins[i].ToString();
            else
                thirdPinText.text = pins[i].ToString();
        }

        ChangeTools();

        //for (int i = 0; i < drill.Length; i++)
        //    drill[i] = random.Next(-2, 3);

        //for (int i = 0; i < lockpick.Length; i++)
        //    lockpick[i] = random.Next(-2, 3); //-2, 3

        //for (int i = 0; i < hammer.Length; i++)
        //    hammer[i] = random.Next(-2, 3);

        //ReturnToolColor();
    }

    public void ChangeTools()
    {
        for (int i = 0; i < drill.Length; i++)
            drill[i] = random.Next(-2, 3);

        for (int i = 0; i < lockpick.Length; i++)
            lockpick[i] = random.Next(-2, 3); //-2, 3

        for (int i = 0; i < hammer.Length; i++)
            hammer[i] = random.Next(-2, 3);

        ReturnToolColor();
    }

    public void Restart()
    {
        FillInRandomly();
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
        timer = 100;
        tempTimer = 0;
        ReturnToolColor();
    }
    
    private void ReturnToolColor()
    {
        drillText.text = $"{drill[0]} | {drill[1]} | {drill[2]}";
        lockpickText.text = $"{lockpick[0]} | {lockpick[1]} | {lockpick[2]}";
        hammerText.text = $"{hammer[0]} | {hammer[1]} | {hammer[2]}";

        firstPinText.color = Color.black;
        secondPinText.color = Color.black;
        thirdPinText.color = Color.black;
    }

    private void Win()
    {
        if (pins[0] == pins[1] && pins[0] == pins[2])
        {
            winMessage.SetActive(true);
            wastedTime.text = $"Потрачено {Mathf.Round(tempTimer)} секунд";
            hacked.text = $"Взломано замков: {++hackedCount}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FillInRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        if (!winMessage.activeSelf && !loseMessage.activeSelf)
        {
            tempTimer += Time.deltaTime;
            //wastedTime.text = $"Потрачено {Mathf.Round(tempTimer)} секунд";

            if (timer >= 0)
            {
                timeText.text = timer.ToString();
                timer = 100 - Mathf.Round(tempTimer);
            }
            else
            {
                loseMessage.SetActive(true);
                hackedCount = 0;
                hacked.text = $"Взломано замков: {hackedCount}";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PickingALock : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject message;

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

    public void ClickButtonDrill()
    {
        pins[0] += drill[0];
        pins[1] += drill[1];
        pins[2] += drill[2];

        if (!CheckRangePinsExceed(drill))
            ShowChangedPins();
    }

    public void ClickButtonLockpick()
    {
        pins[0] += lockpick[0];
        pins[1] += lockpick[1];
        pins[2] += lockpick[2];

        if (!CheckRangePinsExceed(lockpick))
            ShowChangedPins();
    }

    public void ClickButtonHammer()
    {
        pins[0] += hammer[0];
        pins[1] += hammer[1];
        pins[2] += hammer[2];

        if (!CheckRangePinsExceed(hammer))
            ShowChangedPins();
    }
    
    private bool CheckRangePinsExceed(int[] tool)
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
                break;
            }
        }
        return isRangeExceed;
    }

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

        for (int i = 0; i < drill.Length; i++)
            drill[i] = random.Next(-1, 3);

        for (int i = 0; i < lockpick.Length; i++)
            lockpick[i] = random.Next(-1, 3);

        for (int i = 0; i < hammer.Length; i++)
            hammer[i] = random.Next(-1, 3);

        drillText.text = $"{drill[0]} | {drill[1]} | {drill[2]}";
        lockpickText.text = $"{lockpick[0]} | {lockpick[1]} | {lockpick[2]}";
        hammerText.text = $"{hammer[0]} | {hammer[1]} | {hammer[2]}" ;
    }

    public void Restart()
    {
        FillInRandomly();
        message.SetActive(false);
        timer = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillInRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != 0)
        {
            timeText.text = timer.ToString();
            timer = 100 - Mathf.Round(Time.time);
        }
        else
        {
            message.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    public static UpdateScore instance;

    public Text moneyDisplay, scoreDisplay;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public void DisplayScore(int value)
    {
        if (scoreDisplay)
            scoreDisplay.text = value.ToString("00000000");
    }
}
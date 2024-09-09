using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] Image[] heartSprites;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void OnEnable()
    {
        GameManagement.instance.BecomeGameCanvas(this);
        UpdateMoney();
    }
    public void UpdateHealth(int value, bool increase)
    {
        if (value >= heartSprites.Length)
            throw new Exception("Health bigger than array");
        if (increase)        
            heartSprites[value].sprite = fullHeart;        
        else
            heartSprites[value].sprite = emptyHeart;        
    }

    public void UpdateMoney()
    {
        moneyText.text = "X" + GameManagement.instance.GetMoney();
    }
}

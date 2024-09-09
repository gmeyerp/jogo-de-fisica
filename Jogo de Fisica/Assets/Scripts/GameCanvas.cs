using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;
    [SerializeField] Image[] heartSprites;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    // Start is called before the first frame update
    void OnEnable()
    {
        GameManagement.instance.BecomeGameCanvas(this);
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
}

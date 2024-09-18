using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;   
    [SerializeField] Image[] heartSprites;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Button upgradeButton;
    [SerializeField] UpgradeCanvas upgradeCanvas;

    public Turret upgradeTarget;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
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

    public void UpgradeButtonStatus(bool enable, Turret targetTurret)
    {
        upgradeButton.interactable = enable;
        upgradeTarget = targetTurret;
        if(enable == false)
        {
           UpgradeCanvas.instance.gameObject.SetActive(enable); //desliga o canvas quando o player sai da torre
        }
    }

    public Turret GetUpgradeTarget()
    {
        return upgradeTarget;
    }


    public void ToggleUpgradeCanvas()
    {
        if (upgradeCanvas.gameObject.activeSelf)
        {
            upgradeCanvas.gameObject.SetActive(false);
        }
        else
        {
            upgradeCanvas.gameObject.SetActive(true);
            UpgradeCanvas.instance.RefreshButtons(upgradeTarget);
        }              
    }
}

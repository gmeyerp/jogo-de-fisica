using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCanvas : MonoBehaviour
{
    public static UpgradeCanvas instance;
    Turret targetTurret;
    [SerializeField] Button[] upgradeButtons;
    [SerializeField] Image[] upgradeBaseImage;
    [SerializeField] TextMeshProUGUI[] upgradeText;
    [SerializeField] Sprite doneImage;
    [SerializeField] Sprite baseImage;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void RefreshButtons(Turret targetTurret)
    {
        this.targetTurret = targetTurret;
        if (targetTurret == null)
        {
            Debug.Log("Referencia de Turret nula");
            return;
        }
        for (int i = 0; i < targetTurret.upgrades.Length; i++)
        {
            if (targetTurret.upgrades[i])
            {
                upgradeButtons[i].interactable = false;
                upgradeBaseImage[i].sprite = doneImage;
            }
            else
            {
                upgradeBaseImage[i].sprite = baseImage;
                if (GameManagement.instance.GetMoney() < targetTurret.costs[i])
                {
                    upgradeButtons[i].interactable = false;
                }
                else
                {
                    upgradeButtons[i].interactable = true;
                }
            }
        }
    }

    public void SetTargetTurret(Turret targetTurret)
    {
        this.targetTurret = targetTurret;
    }

    public void UpgradeDamageIncrease()
    {
        targetTurret.UpgradeDamageIncrease();
        GameManagement.instance.ChangeMoney(-targetTurret.costs[0]);
        RefreshButtons(targetTurret);
    }

    public void UpgradePenetrationShot()
    {
        targetTurret.UpgradePenetrationShot();
        GameManagement.instance.ChangeMoney(-targetTurret.costs[1]);
        RefreshButtons(targetTurret);
    }

    public void UpgradeRangeIncrease()
    {
        targetTurret.UpgradeRangeIncrease();
        GameManagement.instance.ChangeMoney(-targetTurret.costs[2]);
        RefreshButtons(targetTurret);
    }

    public void UpgradeFireSpeed()
    {
        targetTurret.UpgradeFireSpeed();
        GameManagement.instance.ChangeMoney(-targetTurret.costs[3]);
        RefreshButtons(targetTurret);
    }

    public void SetShootStyle(int index, ShootStyle style)
    {
        targetTurret.SetShootStyle(index, style);
    }
}

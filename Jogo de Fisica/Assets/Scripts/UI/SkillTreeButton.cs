using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] SkillTreeButton[] dependants;
    [SerializeField] ITreeUpgrade upgrade;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip error;
    [SerializeField] Color purchaseColor;
    [SerializeField] Color originalColor;
    public bool isPurchased;
    [SerializeField] Button selfButton;
    [SerializeField] Image image;
    // Start is called before the first frame update
    void Awake()
    {
        if (upgrade == null)
        {
            upgrade = GetComponent<ITreeUpgrade>();
        }
        Debug.Log(upgrade);
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    public void MakeButtonInteractable()
    {
        selfButton.interactable = true;
        isPurchased = false;
        image.color = originalColor;

        if (UpgradeCanvas.instance.targetTurret.boughtUpgrades.Contains(upgrade))
        {
            isPurchased = true;
            image.color = purchaseColor;
            foreach (SkillTreeButton button in dependants)
            {
                button.MakeButtonInteractable();
            }
        }
        else
        {
            foreach (SkillTreeButton button in dependants)
            {
                button.MakeButtonUninteractable();
            }
        }
    }

    public void MakeButtonUninteractable()
    {
        image.color = originalColor;
        selfButton.interactable = false;
        foreach (SkillTreeButton button in dependants)
        {
            button.MakeButtonUninteractable();
        }
    }

    public void BuyUpgrade()
    {
        if (isPurchased)
            return;
        if(GameManagement.instance.GetMoney() < price)
        {
            SoundManager.instance.PlaySFX(error);
        }
        else
        {
            GameManagement.instance.ChangeMoney(-price);
            Purchase();
        }
    }

    public void Purchase()
    {
        isPurchased = true;
        image.color = purchaseColor;
        if (upgrade == null)
        {
            upgrade = GetComponent<ITreeUpgrade>();
        }
        upgrade.UpgradeTurret(UpgradeCanvas.instance.targetTurret);
        foreach (SkillTreeButton button in dependants)
        {
            button.MakeButtonInteractable();
        }
    }
}

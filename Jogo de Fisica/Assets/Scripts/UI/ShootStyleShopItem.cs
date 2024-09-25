using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootStyleShopItem : Draggable<ShootStyle>
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textElement;

    private new void Start()
    {
        base.Start();

        image.sprite = Value.Sprite;
        textElement.text = $"{Value.Price}";

        GameManagement.instance.OnMoneyChanged += UpdateAvailability;
        UpdateAvailability();
    }

    private void UpdateAvailability()
    {
        if (Value.Price <= GameManagement.instance.GetMoney())
        {
            SetDraggable(true);
            image.color = Color.white;
        }
        else
        {
            SetDraggable(false);
            image.color = Color.gray;
            ResetPosition();
        }
    }
}

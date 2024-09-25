using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootStyleSlot : DropTarget<ShootStyle>
{
    [SerializeField] private int styleIndex;
    [SerializeField] private UpgradeCanvas upgradeCanvas;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    protected override void OnDrop(ShootStyle style)
    {
        upgradeCanvas.SetShootStyle(styleIndex, style);
        image.sprite = style.Sprite;
    }
}

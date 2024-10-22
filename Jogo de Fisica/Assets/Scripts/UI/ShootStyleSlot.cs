using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootStyleSlot : DragDropTarget<ShootStyle>
{
    [SerializeField] private int styleIndex;
    [SerializeField] private UpgradeCanvas upgradeCanvas;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        UpdateShootStyle();
    }

    protected override void OnDrop(ShootStyle style)
    {
        SetShootStyle(style);
    }

    public void UpdateShootStyle()
    {
        ShootStyle style = upgradeCanvas.GetShootStyle(styleIndex);
        if (style != null)
        {
            image.sprite = style.Sprite;
        }
    }

    public void SetShootStyle(ShootStyle style)
    {
        if (upgradeCanvas.TryBuyShootStyle(styleIndex, style))
        {
            UpdateShootStyle();
        }
    }
}

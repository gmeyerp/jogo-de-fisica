using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetShootStyle : DropTarget<ShootStyle>
{
    [SerializeField] private int styleIndex;
    [SerializeField] private UpgradeCanvas upgradeCanvas;

    protected override void OnDrop(ShootStyle style)
    {
        upgradeCanvas.SetShootStyle(styleIndex, style);
    }
}

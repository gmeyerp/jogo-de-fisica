using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ITreeUpgrade
{
    public override void UpgradeTurret(Turret turret)
    {
        Debug.Log("Upgrade: " + turret.gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

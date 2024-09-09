using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    Vector3 lastSafePosition;
    [SerializeField] int playerHealth;
    [SerializeField] int playerMaxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetSafePosition(Vector3 position)
    {
        lastSafePosition = position;
    }

    public Vector3 GetSafePosition()
    {
        return lastSafePosition;
    }

    public void ReduceHealth()
    {
        Debug.Log("Take Damage");
        playerHealth--;
        if (playerHealth <= 0)
        {
            //GameOver()
        }
    }
}

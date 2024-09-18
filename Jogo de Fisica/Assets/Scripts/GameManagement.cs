using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    Vector3 lastSafePosition;
    [SerializeField] int playerHealth;
    [SerializeField] int playerMaxHealth;
    [SerializeField] int money = 0;
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
        playerHealth--;
        GameCanvas.instance.UpdateHealth(playerHealth, false);
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        Destroy(gameObject);
    }

    public void RecoverHealth()
    {
        if(playerHealth < playerMaxHealth)
        {
            playerHealth++;
            Debug.Log(playerHealth);
            try
            {
                GameCanvas.instance.UpdateHealth(playerHealth, true);
            }
            catch (Exception E)
            {
                Debug.Log(E.Message);
                GameCanvas.instance.UpdateHealth(playerHealth - 1, true);
            }
        }         
    }

    public void Victory()
    {
        int cena = SceneManager.GetActiveScene().buildIndex;
        if (cena == 1)
        {
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
            Destroy(gameObject);
        }        
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void ChangeMoney (int amount)
    {
        money += amount;
        GameCanvas.instance.UpdateMoney();
    }

    public int GetMoney()
    {
        return money;
    }
}

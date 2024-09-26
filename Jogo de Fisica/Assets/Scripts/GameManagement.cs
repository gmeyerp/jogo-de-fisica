using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    Transform lastSafePosition;
    [SerializeField] int playerHealth;
    [SerializeField] int playerMaxHealth;
    [SerializeField] int money = 0;
    int lastScene = 0;
    public Action OnMoneyChanged;
    bool isPaused;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject); //retornei isso porque permite o retry do game over recome�ar o level que estaa
    }

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void SetSafePosition(Transform safeSpot)
    {
        lastSafePosition = safeSpot;
    }

    public Transform GetSafePosition()
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
        lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOverScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(lastScene);
        Destroy(gameObject);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        GetNextLevel();       
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void ChangeMoney (int amount)
    {
        money += amount;

        OnMoneyChanged();
    }

    public int GetMoney()
    {
        return money;
    }

    public void PauseGame()
    {
        Debug.Log("Pause");
        if (isPaused)
        {
            Time.timeScale = 1f;
            UIManager.instance.pauseCanvas.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            UIManager.instance.pauseCanvas.SetActive(true);
            isPaused = true;
        }        
    }

    public int GetLastScene()
    { 
        return lastScene;
    }

    public void GetNextLevel()
    {
        int cena = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(cena + 1);
        Destroy(gameObject);
    }

    public void DoubleShoot(float delay, Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower, Bullet prefab)
    {
        StartCoroutine(DelayShoot(delay, direction, spawnPosition, spawnRotation, weaponPower, prefab));
    }

    IEnumerator DelayShoot(float delay, Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower, Bullet prefab)
    {
        Debug.Log("waiting for double");
        yield return new WaitForSeconds(delay);
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Shoot(direction * weaponPower);
    }


}

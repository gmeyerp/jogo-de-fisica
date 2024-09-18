using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Track Configurations")]
    [SerializeField] private Track[] tracks;

    [Header("Wave Configurations")]
    [SerializeField] private Wave[] wavePrefabs;
    private Wave[] waves;
    [SerializeField] private float waveCooldown;
    private float waveCooldownLeft;

    private int currentWaveIndex = -1;
    private Wave CurrentWave => currentWaveIndex < waves.Length ? waves[currentWaveIndex < 0 ? 0 : currentWaveIndex] : null;

    private void Awake()
    {
        waves = new Wave[wavePrefabs.Length];
        for (int i = 0; i < wavePrefabs.Length; i++)
        {
            Wave wavePrefab = wavePrefabs[i];
            Wave waveInstance = wavePrefab.Instantiate(manager: this);
            waveInstance.name = wavePrefab.name;
            waves[i] = waveInstance;
        }
    }

    private void Start()
    {
        waveCooldownLeft = waveCooldown;
    }

    private void Update()
    {
        if (CurrentWave != null)
        {
            HandleWave();
        }
        else
        {
            GameManagement.instance.Victory();
        }
    }

    private void HandleWave()
    {
        if (CurrentWave.IsSpawning) return;
        if (EnemiesLeft > 0) return;


        if (waveCooldownLeft > Time.deltaTime)
        {
            waveCooldownLeft -= Time.deltaTime;
            return;
        }
        else
        {
            waveCooldownLeft = 0;

            currentWaveIndex++;
            CurrentWave.Send();

            waveCooldownLeft = waveCooldown;
        }
    }

    private Track GetNextTrack() => tracks[0];

    public void Send(Enemy enemyPrefab)
    {
        Track track = GetNextTrack();
        track.Send(enemyPrefab);
    }

    public int EnemiesLeft => tracks.Sum(track => track.EnemiesLeft);
}

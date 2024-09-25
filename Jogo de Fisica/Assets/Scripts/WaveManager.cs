using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Track Configurations")]
    [SerializeField] private Track[] tracks;
    [SerializeField] int trackCount;

    [Header("Wave Configurations")]
    [SerializeField] private Wave[] wavePrefabs;
    private Wave[] waves;
    [SerializeField] private float waveCooldown;
    private float waveCooldownLeft;

    private int nextWaveIndex = 0;
    private Wave NextWave => nextWaveIndex < waves.Length ? waves[nextWaveIndex] : null;

    private void Awake()
    {
        waves = new Wave[wavePrefabs.Length];
        for (int i = 0; i < wavePrefabs.Length; i++)
        {
            Wave wavePrefab = wavePrefabs[i];
            Wave waveInstance = wavePrefab.Instantiate(manager: this);
            waveInstance.name = $"Wave {i + 1}";
            waves[i] = waveInstance;
        }
    }

    private void Start()
    {
        waveCooldownLeft = waveCooldown;
    }

    private void Update()
    {
        if (NextWave != null)
        {
            HandleWave();
        }
        else if (EnemiesLeft == 0)
        {
            GameManagement.instance.Victory();
        }
    }

    private void HandleWave()
    {
        if (NextWave.IsSpawning) return;
        if (EnemiesLeft > 0) return;


        if (waveCooldownLeft > Time.deltaTime)
        {
            waveCooldownLeft -= Time.deltaTime;
            return;
        }
        else
        {
            waveCooldownLeft = 0;

            NextWave.Send();
            nextWaveIndex++;
            
            waveCooldownLeft = waveCooldown;
        }
    }

    private Track GetNextTrack(int trackCount) => tracks[trackCount];

    public void Send(Enemy enemyPrefab)
    {
        Track track = GetNextTrack(trackCount);
        track.Send(enemyPrefab);
        trackCount++;
        trackCount = trackCount % tracks.Length;
    }

    public int EnemiesLeft => tracks.Sum(track => track.EnemiesLeft);
}

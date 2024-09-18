using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Track Configurations")]
    [SerializeField] private Track[] tracks;

    [Header("Wave Configurations")]
    [SerializeField] private WaveSO[] waves;
    [SerializeField] private float waveCooldown;
    private float waveCooldownLeft;

    private int currentWaveIndex = 0;
    private WaveSO CurrentWave => waves != null && currentWaveIndex < waves.Length ? waves[currentWaveIndex] : null;

    private void Start()
    {
        foreach (var wave in waves)
        { wave.SetManager(this); }

        waveCooldownLeft = waveCooldown;
    }

    private void Update()
    {
        HandleWave();
    }

    private void HandleWave()
    {
        if (CurrentWave == null) return;
        if (CurrentWave.IsSpawning) return;

        if (waveCooldownLeft > Time.deltaTime)
        {
            waveCooldownLeft -= Time.deltaTime;
            return;
        }
        else
        { waveCooldownLeft = 0; }


        if (!CurrentWave.IsOngoing)
        {
            CurrentWave.StartWave();
        }
        else
        {
            CurrentWave.ResetWave();
            currentWaveIndex++;
            waveCooldownLeft = waveCooldown;
        }
    }

    private Track GetNextTrack() => tracks[0];

    public void Send(Enemy enemy)
    {
        Track track = GetNextTrack();

        track.Send(enemy);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class WaveSO : ScriptableObject
{
    [SerializeField] private Enemy[] waveDefinition;
    [SerializeField] private float enemyCooldown;
    [SerializeField] private WaveManager manager;
    [SerializeField] private Coroutine waveCoroutine;

    [SerializeField] private bool isSpawning;
    private void Awake()
    {
        isSpawning = false;
    }

    private IEnumerator WaveCoroutine(WaveManager waveManager)
    {
        if (isSpawning)
        {
            Debug.LogWarning("Tried starting a wave while it is already in progress!");
            yield break;
        }

        isSpawning = true;

        for (int i = 0; i < waveDefinition.Length; i++)
        {
            Enemy enemy = waveDefinition[i];

            waveManager.Send(enemy);

            yield return new WaitForSeconds(enemyCooldown);
        }

        isSpawning = false;
    }

    public void SetManager(WaveManager manager)
    { this.manager = manager; }

    public void StartWave()
    {
        if (manager == null)
        {
            Console.Error.WriteLine("Cannot start a wave without a manager!");
            return;
        }

        waveCoroutine = manager.StartCoroutine(WaveCoroutine(manager));
    }

    public void ResetWave()
    {
        manager.StopCoroutine(waveCoroutine);
        isSpawning = false;
        waveCoroutine = null;
    }

    public bool IsOngoing => waveCoroutine != null;
    public bool IsSpawning => isSpawning;
}

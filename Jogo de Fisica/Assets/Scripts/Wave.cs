using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private Enemy[] waveDefinition;
    [SerializeField] private float enemyCooldown;
    private WaveManager manager;

    private int spawning = 0;

    private IEnumerator WaveCoroutine()
    {
        spawning++;

        for (int i = 0; i < waveDefinition.Length; i++)
        {
            Enemy enemy = waveDefinition[i];

            manager.Send(enemy);

            yield return new WaitForSeconds(enemyCooldown);
        }

        spawning--;
    }

    public void Send()
    {
        StartCoroutine(WaveCoroutine());
    }

    public Wave Instantiate(WaveManager manager)
    {
        Wave instance = Instantiate(this, manager.transform);
        instance.waveDefinition = waveDefinition;
        instance.enemyCooldown = enemyCooldown;
        instance.manager = manager;
        return instance;
    }

    public bool IsSpawning => spawning > 0;
}
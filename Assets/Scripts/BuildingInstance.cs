using Rubin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInstance : MonoBehaviour
{
    public BuildingData Data;

    public List<ParticleSystem> ParticleSystem;

    private Ticker ticker;

    private void Awake()
    {
        ticker = TickerCreator.CreateNormalTime(GameManager.Instance.Balance.CooldownForAllBuildings);
    }

    private void Update()
    {
        if (ticker.Push())
        {
            foreach (var result in Data.Result)
            {
                var inventoryRef = GameManager.Instance.Inventory.CountableResources.Find(x => x.ResourceType == result.ResourceType);
                inventoryRef.Count += result.Count;
            }
            foreach (var particle in ParticleSystem)
            {
                particle.Play();
            }
        }
    }
}

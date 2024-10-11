using Rubin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceExtraction
{
    public CountableResource CountableResource;
    public float Cooldown;
}

public class BuildingInstance : MonoBehaviour
{
    public BuildingData Data;
    public ResourceInventory Inventory;
    public ResourceExtraction Extraction; 

    private Ticker ticker;

    public void Initialize(BuildingData data)
    {
        Data = data;
    }

    private void Awake()
    {
        ticker = TickerCreator.CreateNormalTime(Extraction.Cooldown);
    }

    private void Update()
    {
        if (ticker.Push())
        {
            var countableResource = Inventory.CountableResources.Find(x => x.ResourceType == Extraction.CountableResource.ResourceType);
            countableResource.Count += Extraction.CountableResource.Count;
        }
    }
}
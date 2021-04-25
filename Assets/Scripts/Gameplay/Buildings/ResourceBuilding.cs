using System;
using UnityEngine;

public class ResourceBuilding : BuildingBase
{
    private ResourceBuildingSO m_rBuilding = null;
    public ResourceType m_resourceType { get; private set; }

    public int getHarvestAmount => m_harvestAmount;
    private int m_harvestAmount = 10;
    protected override void Setup()
    {
        m_rBuilding = (ResourceBuildingSO)m_building;
        m_resourceType = m_rBuilding.getResourceType;

        Invoke("Harvest", m_rBuilding.getHarvestTime);
    }

    public void Harvest()
    {
        m_owningController.m_resourceManager.GainResource(m_rBuilding.getResourceType, getHarvestAmount);
        Invoke("Harvest", m_rBuilding.getHarvestTime);
    }
}
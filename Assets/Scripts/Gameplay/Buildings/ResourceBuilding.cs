using System;
using UnityEngine;

public class ResourceBuilding : BuildingBase
{
    private ResourceBuildingSO m_rBuilding = null;
    public ResourceType m_resourceType { get; private set; }

    public int m_harvestAmount => _harvestAmount;
    private int _harvestAmount = 10;
    protected override void Setup()
    {
        Debug.Log("Building : " + gameObject.name + " Has been build!");

        m_rBuilding = (ResourceBuildingSO)m_building;
        m_resourceType = m_rBuilding.m_resourceType;

        Invoke("Harvest", m_rBuilding.m_harvestTime);
    }

    public void Harvest()
    {
        m_owningController.m_resourceManager.GainResource(m_rBuilding.m_resourceType, m_harvestAmount);
        Invoke("Harvest", m_rBuilding.m_harvestTime);
    }
}
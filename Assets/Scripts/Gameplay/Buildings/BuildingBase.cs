using System;
using UnityEngine;

public class BuildingBase : MonoBehaviour, ITargetable
{
    protected BuildingSO m_building = null;

    protected Controller m_owningController;

    public BuildingType m_buildingType { get; private set; }

    public void OnBuild(BuildingSO a_building, Controller a_controller)
    {
        m_buildingType = a_building.getBuildingType;
        m_building = a_building;
        m_owningController = a_controller;
        m_owningController.AddBuilding(this);
        Setup();
    }

    protected virtual void Setup()
    {

    }

    private void OnDestroy()
    {
        if (m_owningController != null)
        {
            m_owningController.RemoveBuilding(this);
        }
    }
}
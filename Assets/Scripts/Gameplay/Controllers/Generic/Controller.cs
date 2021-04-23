using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Buildings
{
    public BuildingBase m_building;
    public Vector3 m_location;
}

public class Controller : MonoBehaviour
{
    public GameManager getGameManager => m_gameManager;
    private GameManager m_gameManager;
    public List<Buildings> getBuildings => m_availableBuildings;
    protected List<Buildings> m_availableBuildings = new List<Buildings>();
    public List<BaseUnit> getUnits => m_availableUnits;
    protected List<BaseUnit> m_availableUnits = new List<BaseUnit>();
    public ResourceManager m_resourceManager { get; protected set; }
    public int m_controllerId { get; private set; }


    public Builder m_builder { get; private set; }
    public Transform m_buildingHolder;
    public float getMilitaryStrenght => m_militaryStrenght;
    protected float m_militaryStrenght = 0;

    protected List<Squad> m_squads = new List<Squad>();

    public void Setup(int a_id, GameManager a_gm)
    {
        m_gameManager = a_gm;
        m_controllerId = a_id;
        m_builder = new Builder(this);
        m_resourceManager = new ResourceManager(this);
    }
    public void AddBuilding(BuildingBase a_building)
    {
        m_availableBuildings.Add(new Buildings()
        {
            m_building = a_building,
            m_location = a_building.transform.position
        });
    }
    public void RemoveBuilding(BuildingBase a_building)
    {
        foreach(Buildings b in m_availableBuildings)
        {
            if(b.m_building == a_building)
            {
                m_availableBuildings.Remove(b);
                break;
            }
        }
    }
    public void AddUnit(BaseUnit a_unit)
    {
        m_militaryStrenght += a_unit.getUnitSo.getMilitaryStrenght;
        m_availableUnits.Add(a_unit);
    }
    public void RemoveUnit(BaseUnit a_unit)
    {
        m_militaryStrenght -= a_unit.getUnitSo.getMilitaryStrenght;
        m_availableUnits.Remove(a_unit);
    }
    public void RemoveSquad(Squad a_squad)
    {
        m_squads.Remove(a_squad);
        a_squad = null;
    }

    protected virtual void Update()
    {
        foreach(Squad s in m_squads)
        {
            s.UpdateSquad();
        }
    }
}

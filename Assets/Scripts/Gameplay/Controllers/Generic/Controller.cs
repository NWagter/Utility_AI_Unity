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
    public List<Buildings> GetBuildings => m_availableBuildings;
    protected List<Buildings> m_availableBuildings = new List<Buildings>();
    public ResourceManager m_resourceManager { get; protected set; }
    public int m_controllerId { get; private set; }
    public Builder m_builder { get; private set; }
    public Transform m_buildingHolder;

    public void Setup(int a_id)
    {
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

}

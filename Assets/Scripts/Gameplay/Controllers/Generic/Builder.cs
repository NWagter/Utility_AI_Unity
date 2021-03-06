using System;
using UnityEngine;
using System.Collections;

public class Builder
{
    private Controller m_controller;
    public Builder(Controller a_controller)
    {
        m_controller = a_controller;
    }

    public GameObject BuildBuilding(BuildingSO a_building, Vector3 a_pos, Vector3 a_rot)
    {
        var building = GameObject.Instantiate(a_building.getBuildingObject, a_pos, Quaternion.Euler(a_rot));
        building.GetComponent<BuildingBase>().OnBuild(a_building, m_controller);
        building.transform.SetParent(m_controller.m_buildingHolder);

        return building;
    }

}

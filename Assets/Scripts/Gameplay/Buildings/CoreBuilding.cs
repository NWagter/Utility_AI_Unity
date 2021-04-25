using System.Collections.Generic;
using UnityEngine;
using Unity;

public class CoreBuilding : BuildingBase
{
    private CoreBuildingSO m_cBuilding = null;

    private List<Vector3> m_buildPoints = new List<Vector3>();

    protected override void Setup()
    {
        m_cBuilding = (CoreBuildingSO)m_building;

        Vector3 pos = transform.position;
        for(float x = -15; x <= 15; x+= 15)
        {
            for (float z = -15; z <= 15; z += 15)
            {
                if(x == 0 && z == 0)
                {
                    continue;
                }

                m_buildPoints.Add(pos + new Vector3(x, 0, z));
            }
        }
    }

    public bool SpotLeft() { return (m_buildPoints.Count > 0); }

    public Vector3 GetBuildSpot()
    {
        Vector3 spot = Vector3.zero;

        if(m_buildPoints.Count <= 0)
        {
            return spot;
        }

        int point = Random.Range(0, m_buildPoints.Count - 1);

        spot = m_buildPoints[point];
        m_buildPoints.RemoveAt(point);

        return spot;
    }
}
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class UnitProductionBuilding : BuildingBase
{
    public struct ProductionItem
    {
        public UnitSO m_unit;
        public float m_productionTime;

        public ProductionItem(UnitSO a_unit, float a_time)
        {
            m_unit = a_unit;
            m_productionTime = a_time;
        }
    }

    private UnitProductionBuildingSO m_uBuilding = null;

    private Vector3 m_rallyPoint = Vector3.zero;

    private Queue<ProductionItem> m_productionQueue = new Queue<ProductionItem>();

    protected override void Setup()
    {
        Debug.Log("Building : " + gameObject.name + " Has been build!");

        m_uBuilding = (UnitProductionBuildingSO)m_building;
        m_rallyPoint = transform.position + (transform.forward * -10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_rallyPoint, 5);
    }

    public bool QueueAvailable()
    {
        return (m_productionQueue.Count < m_uBuilding.m_maxQueue);
    }

}
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class UnitProductionBuilding : BuildingBase
{
    [System.Serializable]
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

    [SerializeField]
    private Vector3 m_rallyPoint = Vector3.zero;

    private Queue<ProductionItem> m_productionQueue = new Queue<ProductionItem>();

    private float m_timer;

    protected override void Setup()
    {
        m_uBuilding = (UnitProductionBuildingSO)m_building;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_rallyPoint, 5);
    }

    private void Update()
    {
        if(m_productionQueue.Count > 0)
        {
            ProductionItem item = m_productionQueue.Peek();
            if(m_timer >= item.m_productionTime)
            {
                m_productionQueue.Dequeue();
                // Recruit Unit
                BaseUnit unit = Instantiate(item.m_unit.getUnitObject, transform.position, Quaternion.identity).GetComponent<BaseUnit>();
                unit.Setup(item.m_unit, transform.position + m_rallyPoint, m_owningController);

                m_owningController.AddUnit(unit);
                m_timer = 0;
            }

            m_timer += Time.deltaTime;
        }
    }

    public bool RecruitUnit(UnitSO a_unit)
    {
        m_productionQueue.Enqueue(new ProductionItem(a_unit, a_unit.getBuildTime));

        return true;
    }

    public bool QueueAvailable()
    {
        return (m_productionQueue.Count < m_uBuilding.getMaxQueue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreepLayer : MonoBehaviour, ITargetable
{
    [SerializeField]
    private CreepSO m_creep = null;
    [SerializeField]
    private Vector3 m_rallyPoint;

    public float getWanderRadius => m_wanderRadius;
    [SerializeField]
    private float m_wanderRadius;

    private List<BaseCreep> m_creeps = new List<BaseCreep>();

    public float getStrenght => m_strenght;
    private float m_strenght;

    private void Start()
    {
        for (int i = 0; i < m_creep.getAmountofCreeps; i++)
        {
            SpawnCreep();
        }
    }

    public void AddCreep(BaseCreep a_creep)
    {
        m_strenght += a_creep.getCreep.getStrenght;
        m_creeps.Add(a_creep);
    }

    public void RemoveCreep(BaseCreep a_creep)
    {
        m_strenght -= a_creep.getCreep.getStrenght;
        m_creeps.Remove(a_creep);
    }
    private void SpawnCreep()
    {
        var creep = Instantiate(m_creep.getCreepObject, transform.position, Quaternion.identity);
        BaseCreep unit = creep.GetComponent<BaseCreep>();
        unit.Setup(this, m_creep);
        unit.Navigate(transform.position + m_rallyPoint);
    }
}


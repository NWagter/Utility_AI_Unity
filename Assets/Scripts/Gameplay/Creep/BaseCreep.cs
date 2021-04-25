using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseCreep : MonoBehaviour, ITargetable
{
    private NavMeshAgent m_agent = null;

    public CreepSO getCreep => m_creep;
    private CreepSO m_creep;
    private CreepLayer m_layer;

    private Vector3 m_layerPoint;
    private float m_wanderDistance;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
    public void Setup(CreepLayer a_layer, CreepSO a_creep)
    {
        m_layer = a_layer;
        m_creep = a_creep;

        m_layerPoint = m_layer.transform.position;
        m_wanderDistance = m_layer.getWanderRadius;

        m_layer.AddCreep(this);
    }

    private void Update()
    {
        if(Vector3.Distance(m_agent.destination, transform.position) < 0.5f)
        {
            Wander();
        }
    }

    public void Navigate(Vector3 a_point)
    {
        m_agent.SetDestination(a_point);
    }

    protected void Wander()
    {
        Vector3 location = (Random.onUnitSphere * m_wanderDistance) + m_layerPoint;
        Navigate(location);
    }
    public GameObject GetTargetObject()
    {
        return gameObject;
    }

    public TargetType GetTargetType()
    {
        return TargetType.Building;
    }
    public float GetStrenght()
    {
        return m_creep.getStrenght;
    }
    public bool CanTarget(Controller a_controller)
    {
        return true;
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class BaseUnit : MonoBehaviour
{
    private NavMeshAgent m_agent = null;

    public UnitSO getUnitSo => m_unitSo;
    private UnitSO m_unitSo;


    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
    public void Setup(UnitSO a_unitSO)
    {
        m_unitSo = a_unitSO;
    }

    public void Navigate(Vector3 a_point)
    {
        m_agent.SetDestination(a_point);
    }
}


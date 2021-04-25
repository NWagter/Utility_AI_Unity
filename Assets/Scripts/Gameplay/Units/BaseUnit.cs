using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class BaseUnit : MonoBehaviour, ITargetable
{
    private Controller m_owningController;
    private NavMeshAgent m_agent = null;

    public UnitSO getUnitSo => m_unitSo;
    private UnitSO m_unitSo;

    private Squad m_sqaud = null;
    private Vector3 m_spawnPoint;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>(); 
        m_sqaud = null;
    }
    public void Setup(UnitSO a_unitSO, Vector3 a_rallyPoint, Controller a_controller)
    {
        m_owningController = a_controller;
        m_unitSo = a_unitSO;
        m_spawnPoint = a_rallyPoint;
        Navigate(a_rallyPoint);
    }

    public void Navigate(Vector3 a_point)
    {
        m_agent.SetDestination(a_point);
    }
    public void StopNavigating()
    {
        m_agent.isStopped = true;
    }

    public void SetSqaud(Squad a_squad)
    {
        m_sqaud = a_squad;
    }

    public void RemoveSquad()
    {
        m_sqaud = null;
    }

    public void ReturnToSpawn()
    {
        Navigate(m_spawnPoint);
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
        return m_unitSo.getMilitaryStrenght;
    }
    public bool CanTarget(Controller a_controller)
    {
        return (m_owningController != a_controller);
    }
}


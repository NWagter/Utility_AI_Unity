using UnityEngine;
using System.Collections.Generic;

public enum SquadState
{
    Idle,
    Move,
    Attacking
}

[System.Serializable]
public class Squad
{
    private Controller m_ownerController = null;
    private List<BaseUnit> m_units = new List<BaseUnit>();

    private GameObject m_target = null;

    public float getStrenght => m_strenght;
    private float m_strenght = 0;

    private SquadState m_sState;
    private Vector3 centroid;

    public Squad(Controller a_controller, List<BaseUnit> a_units, GameObject a_target)
    {
        m_ownerController = a_controller;
        m_units = a_units;
        m_target = a_target;
        m_sState = SquadState.Idle;

        Vector3 c = Vector3.zero;

        foreach (BaseUnit unit in m_units)
        {
            m_ownerController.RemoveUnit(unit);
            unit.SetSqaud(this);
            m_strenght += unit.getUnitSo.getMilitaryStrenght;
            c += unit.transform.position;
        }

        centroid = c / m_units.Count;
    }

    public void OnDisband()
    {
        foreach(BaseUnit unit in m_units)
        {
            m_ownerController.AddUnit(unit);
            unit.RemoveSquad();
            unit.ReturnToSpawn();
        }

        m_strenght = 0;
        m_ownerController.RemoveSquad(this);
    }

    public bool UpdateSquad()
    {
        if (m_target == null)
        {
            OnDisband();
            return false;
        }

        if (m_sState == SquadState.Idle)
        {
            MoveToTarget();
        }

        if(m_sState == SquadState.Move)
        {
            if (Vector3.Distance(centroid, m_target.transform.position) < 2.5f)
            {
                m_sState = SquadState.Attacking;

            }

            CalcCentroid();
        }

        if(m_sState == SquadState.Attacking)
        {
            //Attack?
            m_ownerController.getGameManager.RemoveCreep(m_target);
            GameObject.Destroy(m_target);
        }

        return true;
    }

    private void MoveToTarget(bool a_stop = false)
    {
        m_sState = SquadState.Move;

        foreach(BaseUnit unit in m_units)
        {
            if (!a_stop)
            {
                unit.Navigate(m_target.transform.position);
            }
            else
            {
                unit.StopNavigating();
            }
        }
    }

    private void CalcCentroid()
    {
        Vector3 c = Vector3.zero;

        foreach(BaseUnit unit in m_units)
        {
            c += unit.transform.position;
        }

        centroid = c / m_units.Count;
    }

    public void AddToSquad(BaseUnit a_unit)
    {
        m_units.Add(a_unit);
        a_unit.SetSqaud(this);
        m_strenght += a_unit.getUnitSo.getMilitaryStrenght;
        m_ownerController.RemoveUnit(a_unit);
    }
    public void RemoveToSquad(BaseUnit a_unit)
    {
        m_units.Remove(a_unit);
        m_strenght -= a_unit.getUnitSo.getMilitaryStrenght;
        a_unit.RemoveSquad();
        m_ownerController.AddUnit(a_unit);
    }
}

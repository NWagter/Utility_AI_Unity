using UnityEngine;
using System.Collections.Generic;

public enum SquadState
{
    Idle,
    Move,
    Attacking
}

public class Squad
{
    private Controller m_ownerController = null;
    private List<BaseUnit> m_units = new List<BaseUnit>();

    private Squad m_parentSquad = null;
    public bool getActive => m_active;
    private bool m_active = false;
    private List<Squad> m_childSquads = new List<Squad>();

    private GameObject m_targetObject = null;
    private ITargetable m_target = null;

    public float getStrenght => m_strenght;
    private float m_strenght = 0;

    private SquadState m_sState;

    private Vector3 centroid;
    private float m_detectionRadius = 35;

    public Squad(Controller a_controller, List<BaseUnit> a_units, ITargetable a_target, Squad a_squadParent = null)
    {
        m_ownerController = a_controller;
        m_ownerController.AddSquad(this);
        m_parentSquad = a_squadParent;

        m_units = a_units;
        m_target = a_target;
        m_targetObject = m_target.GetTargetObject();
        m_sState = SquadState.Idle;

        Vector3 c = Vector3.zero;
        m_active = true;

        foreach (BaseUnit unit in m_units)
        {
            if (a_squadParent == null)
            {
                m_ownerController.RemoveUnit(unit);
            }
            else
            {
                a_squadParent.RemoveFromSquad(unit);
            }

            unit.SetSqaud(this);
            m_strenght += unit.getUnitSo.getMilitaryStrenght;
            c += unit.transform.position;
        }

        centroid = c / m_units.Count;
    }

    public void OnDisband()
    {
        m_active = false;

        if (m_parentSquad != null && m_parentSquad.getActive)
        {
            foreach (BaseUnit unit in m_units)
            {
                m_parentSquad.AddToSquad(unit);
                unit.SetSqaud(m_parentSquad);
            }

            m_strenght = 0;
            m_parentSquad.RemoveSquad(this);
        }
        else
        {
            foreach (BaseUnit unit in m_units)
            {
                m_ownerController.AddUnit(unit);
                unit.RemoveSquad();
                unit.ReturnToSpawn();
            }

            m_strenght = 0;
            m_ownerController.RemoveSquad(this);
        }

    }

    public bool UpdateSquad()
    {
        List<ITargetable> targetables = CombatHelper.SearchForTargets(centroid, m_detectionRadius, m_ownerController);

        if (targetables.Count > 0)
        {
            //New Target gernal Target
            if (m_targetObject == null)
            {
                m_sState = SquadState.Idle;
                m_target = GetTarget(targetables);

                if(m_target == null)
                {
                    OnDisband();
                    return false;
                }

                m_targetObject = m_target.GetTargetObject();

            }
            else
            {
                //Only take units away that don't influence the win change
                ITargetable newTarget = GetTarget(targetables, m_target.GetStrenght());

                if (newTarget != null)
                {
                    List<BaseUnit> squadUnits = new List<BaseUnit>();
                    // fill the sqaud with units worth of the newTarget strenght
                    float strenght = 0;
                    foreach(BaseUnit unit in m_units)
                    {
                        squadUnits.Add(unit);
                        strenght += unit.GetStrenght();
                        if(strenght >= newTarget.GetStrenght())
                        {
                            break;
                        }
                    }

                    Squad childSquad = new Squad(m_ownerController, squadUnits, newTarget, this);
                    m_childSquads.Add(childSquad);
                    return false;
                }
            }
        }

        if (m_targetObject == null)
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
            if (Vector3.Distance(centroid, m_targetObject.transform.position) < 3.5f)
            {
                m_sState = SquadState.Attacking;

            }
            else
            {
                MoveToTarget();
            }

            CalcCentroid();
        }

        if(m_sState == SquadState.Attacking)
        {
            //Attack?
            m_ownerController.getGameManager.RemoveCreep(m_targetObject);
            GameObject.Destroy(m_targetObject);
            m_target = null;
            return false;
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
                unit.Navigate(m_targetObject.transform.position);
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
    public void RemoveFromSquad(BaseUnit a_unit)
    {
        m_units.Remove(a_unit);
        m_strenght -= a_unit.getUnitSo.getMilitaryStrenght;
        a_unit.RemoveSquad();
        m_ownerController.AddUnit(a_unit);
    }
    public void RemoveSquad(Squad a_squad)
    {
        m_childSquads.Remove(a_squad);
    }
    private ITargetable GetTarget(List<ITargetable> a_targetables, float a_requiredStrenght = 0)
    {
        ITargetable target = null;

        float distance = -1;
        float maxStrenght = m_strenght - a_requiredStrenght;

        foreach (ITargetable t in a_targetables)
        {
            if (t == m_target || t == null)
                continue;

            if (t.GetStrenght() > maxStrenght)
                continue;

            float dist = Vector3.Distance(centroid, t.GetTargetObject().transform.position);

            if (distance == -1 || dist < distance)
            {
                distance = dist;
                target = t;
            }
        }

        return target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilAI;

public class AIController : Controller
{
    [SerializeField]
    private UtilAI.UtilityBrainSO m_brainSO = null;
    private UtilAI.UtilityBrain m_brain = null;

    [SerializeField]
    private List<UtilAI.ActionSO> m_availableActions = new List<UtilAI.ActionSO>();

    [SerializeField]
    private BuildingSO m_keep;

    private int m_resourceLayer;

    public float getBuildingTimer => m_buildTimer;
    private float m_buildTimer = 0.5f;


    private void Start()
    {
        m_brain = new UtilAI.UtilityBrain(this, m_availableActions, m_brainSO);
        m_builder.BuildBuilding(m_keep, transform.position, Quaternion.identity.eulerAngles);
        m_resourceLayer = 1 << 7;
    }

    public void Attack(AttackType a_attackType)
    {
        ITargetable target = FetchTarget(a_attackType);

        if (target == null)
            return;

        //Create Squad
        List<BaseUnit> units = new List<BaseUnit>();

        float strenght = 0;
        foreach(BaseUnit unit in m_availableUnits)
        {
            if((target.GetStrenght() * 2.5f) < strenght)
            {
                break;
            }

            units.Add(unit);
            strenght += unit.getUnitSo.getMilitaryStrenght;
        }

        Squad squad = new Squad(this, units, target);
    }

    private ITargetable FetchTarget(AttackType a_type)
    {
        ITargetable returnValue = null;

        if (a_type == AttackType.Creep)
        {
            float distance = -1;
            foreach (CreepLayer cLayer in getGameManager.GetCreepLayers())
            {
                float dist = Vector3.Distance(m_availableBuildings[0].m_location, cLayer.transform.position);

                if ((returnValue == null || (returnValue.GetStrenght() <= cLayer.getStrenght)) && dist < 120)
                {
                    if (distance <= -1 || dist < distance)
                    {
                        returnValue = cLayer.GetComponent<ITargetable>();
                        distance = dist;
                    }
                }
            }
        }
        else if(a_type == AttackType.Enemy)
        {
            foreach (Controller c in getGameManager.GetControllers())
            {
                if (c == this)
                    continue;

                if (returnValue == null || (returnValue.GetStrenght() < c.getMilitaryStrenght))
                {
                    if(c.getBuildings.Count > 0)
                        returnValue = c.getBuildings[0].m_building.GetComponent<ITargetable>();
                }
            }
        }

        return returnValue;
    }

    protected override void Update()
    {
        if(m_buildTimer > 0)
            m_buildTimer -= Time.deltaTime;

        m_brain.UpdateBrain(Time.deltaTime);
        m_resourceManager.Update(Time.deltaTime);

        base.Update();
    }

    public void RecruitUnit(UnitSO a_unit)
    {
        UnitProductionBuilding building = null;

        foreach (Buildings b in m_availableBuildings)
        {
            if (b.m_building.m_buildingType == BuildingType.Military)
            {
                var pBuilding = (UnitProductionBuilding)b.m_building;

                if (pBuilding.QueueAvailable())
                {
                    building = pBuilding;
                    break;
                }
            }
        }

        if (building != null && m_resourceManager.SpentResource(a_unit.getCost))
        {
            building.RecruitUnit(a_unit);
        }
    }

    public Vector3 FindBuildingSpot(BuildingSO a_building)
    {
        Vector3 buildingLocation = Vector3.zero;
        //Use Core as a location for the building

        foreach (Buildings b in m_availableBuildings)
        {
            if (b.m_building.m_buildingType == BuildingType.Keep)
            {
                switch (a_building.getBuildingType)
                {
                    case BuildingType.Resource:
                        {
                            ResourceType type = ((ResourceBuildingSO)a_building).getResourceType;

                            switch (type)
                            {
                                case ResourceType.food:
                                    // Open area within a x range of keep
                                    return FindFarmSpot(b.m_location, 70, 15, 25);
                                case ResourceType.wood:
                                    // Area with an x amount of trees in range of the keep
                                    return FindSawmillSpot(b.m_location, 50, 15, 10, 3);
                            }

                        }
                        break;
                    case BuildingType.Military:
                        {
                            return FindBuildingSpot(b.m_location, 50, 25);
                        }
                }
            }
        }
        return buildingLocation;
    }

    public void Build(BuildingSO a_building)
    {
        Vector3 location = FindBuildingSpot(a_building);

        if (location != Vector3.zero && m_resourceManager.SpentResource(a_building.getCost)) // double check if resources still available
        {
            m_buildTimer = 1.5f;
            m_builder.BuildBuilding(a_building, location, new Vector3(0, 0, 0));
        }

    }

    Vector3 FindSawmillSpot(Vector3 a_keep, float a_radius, float a_size, float a_sawRange, int a_minTrees)
    {
        Vector3 location = Vector3.zero;

        bool canPlace = true;

        location = (Random.onUnitSphere * Random.Range(a_radius * 0.5f, a_radius)) + a_keep;
        location.y = a_keep.y;


        RaycastHit[] outhit = Physics.SphereCastAll(location, a_sawRange, transform.forward, a_sawRange, m_resourceLayer);

        int amount = 0;

        if (outhit.Length > 0)
        {
            foreach (RaycastHit hit in outhit)
            {
                if (CheckResourceType(hit.collider.gameObject, ResourceType.wood))
                {
                    amount++;
                }
            }
        }

        foreach (Buildings b in m_availableBuildings)
        {
            if (Vector3.Distance(location, b.m_location) < a_size)
            {
                canPlace = false;
            }
        }

        if (!canPlace || amount < a_minTrees)
        {
            return Vector3.zero;
        }

        return location;
    }

    Vector3 FindFarmSpot(Vector3 a_keep, float a_radius, float a_size, float a_minFarmDistance, int it = 0)
    {
        Vector3 location = Vector3.zero;
        bool canPlace = true;

        location = (Random.onUnitSphere * Random.Range(a_radius * 0.5f, a_radius)) + a_keep;
        location.y = a_keep.y;


        foreach (Buildings b in m_availableBuildings)
        {
            if (Vector3.Distance(location, b.m_location) < a_size ||
                (Vector3.Distance(location, b.m_location) < a_minFarmDistance
                && (b.m_building.m_buildingType == BuildingType.Resource && ((ResourceBuilding)b.m_building).m_resourceType == ResourceType.food)))
            {
                canPlace = false;
            }
        }

        if(!canPlace)
        {
            return Vector3.zero;
        }

        return location;
    }

    Vector3 FindBuildingSpot(Vector3 a_keep, float a_radius, float a_size)
    {
        Vector3 location = Vector3.zero;


        location = (Random.onUnitSphere * Random.Range(a_radius * 0.5f, a_radius)) + a_keep;
        location.y = a_keep.y;

        foreach (Buildings b in m_availableBuildings)
        {
            if (Vector3.Distance(location, b.m_location) < a_size)
            {
                return Vector3.zero;
            }
        }

        return location;
    }

    private bool CheckResourceType(GameObject a_object, ResourceType a_type)
    {
        if (a_object.GetComponent<ResourceObject>() && a_object.GetComponent<ResourceObject>().getResourceType == a_type)
        {
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if(m_buildTimer > 0)
            m_buildTimer -= Time.deltaTime;

        m_brain.UpdateBrain(Time.deltaTime);
        m_resourceManager.Update(Time.deltaTime);
    }

    public void RecruitUnit(UnitSO a_unit)
    {
        UnitProductionBuilding building = null;

        foreach (Buildings b in m_availableBuildings)
        {
            if (b.m_building.m_buildingType == BuildingType.Military)
            {
                building = (UnitProductionBuilding)b.m_building;

                if (!building.QueueAvailable())
                    building = null;
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
                                    return FindFarmSpot(b.m_location, 50, 35);
                                case ResourceType.wood:
                                    // Area with an x amount of trees in range of the keep
                                    return FindSawmillSpot(b.m_location, 50, 10, 3);
                            }

                        }
                        break;
                    case BuildingType.Military:
                        {
                            return FindBuildingSpot(b.m_location, 50, 10);
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

    Vector3 FindSawmillSpot(Vector3 a_keep, float a_radius, float a_sawRange, int a_minTrees)
    {
        Vector3 location = Vector3.zero;

        bool canPlace = true;

        location = (Random.onUnitSphere * a_radius) + a_keep;
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
            if(b.m_building.m_buildingType == BuildingType.Resource
                && ((ResourceBuilding)b.m_building).m_resourceType == ResourceType.wood)
            {
                if (Vector3.Distance(location, b.m_location) < a_sawRange)
                {
                    canPlace = false;
                }
            }
        }

        if (!canPlace || amount < a_minTrees)
        {
            return Vector3.zero;
        }

        return location;
    }

    Vector3 FindFarmSpot(Vector3 a_keep, float a_radius, float a_minFarmDistance, int it = 0)
    {
        Vector3 location = Vector3.zero;
        bool canPlace = true;

        location = (Random.onUnitSphere * a_radius) + a_keep;
        location.y = a_keep.y;


        foreach (Buildings b in m_availableBuildings)
        {
            if(b.m_building.m_buildingType == BuildingType.Resource
                && ((ResourceBuilding)b.m_building).m_resourceType == ResourceType.food)
            {
                if(Vector3.Distance(location, b.m_location) < a_minFarmDistance)
                {
                    canPlace = false;
                }
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


        location = (Random.onUnitSphere * a_radius) + a_keep;
        location.y = a_keep.y;

        RaycastHit[] outhit = Physics.SphereCastAll(location, a_size, transform.forward);
        if (outhit.Length > 0)
        {
            foreach(RaycastHit hit in outhit)
            {
                if (CheckResourceType(hit.collider.gameObject, ResourceType.wood) || hit.collider.GetComponent<BuildingBase>() != null)
                {
                    return Vector3.zero;
                }
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

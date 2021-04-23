using UnityEngine;

public enum BuildingType
{
    Resource,
    Military,
    Keep
}

public class BuildingSO : ContentSO
{
    public ResourcesCollection getCost => m_cost;
    public BuildingType getBuildingType => m_buildingType;

    public GameObject getBuildingObject => m_buildingObject;

    [SerializeField]
    private GameObject m_buildingObject = null;
    [SerializeField]
    private ResourcesCollection m_cost;
    [SerializeField]
    private BuildingType m_buildingType;
}

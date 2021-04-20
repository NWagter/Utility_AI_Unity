using UnityEngine;

public enum BuildingType
{
    Resource,
    Military,
    Keep
}

public class BuildingSO : ContentSO
{
    public ResourcesCollection m_cost => _cost;
    public BuildingType m_buildingType => _buildingType;

    public GameObject m_buildingObject => _buildingObject;

    [SerializeField]
    private GameObject _buildingObject = null;
    [SerializeField]
    private ResourcesCollection _cost;
    [SerializeField]
    private BuildingType _buildingType;
}

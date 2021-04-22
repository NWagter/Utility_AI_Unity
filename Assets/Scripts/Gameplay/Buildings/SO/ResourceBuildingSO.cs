using UnityEngine;

[CreateAssetMenu(fileName = "ResourceBuilding", menuName = "Content/Buildings/Resource Building", order = 1)]
public class ResourceBuildingSO : BuildingSO
{
    public float m_harvestTime => _harvestTime;
    public ResourceType m_resourceType => _resourceType;

    [SerializeField]
    private float _harvestTime;

    [SerializeField]
    private ResourceType _resourceType;
}

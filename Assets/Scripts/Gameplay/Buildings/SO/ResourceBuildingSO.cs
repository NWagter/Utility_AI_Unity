using UnityEngine;

[CreateAssetMenu(fileName = "ResourceBuilding", menuName = "Content/Buildings/Resource Building", order = 1)]
public class ResourceBuildingSO : BuildingSO
{
    public float getHarvestTime => m_harvestTime;
    public ResourceType getResourceType => m_resourceType;

    [SerializeField]
    private float m_harvestTime;

    [SerializeField]
    private ResourceType m_resourceType;
}

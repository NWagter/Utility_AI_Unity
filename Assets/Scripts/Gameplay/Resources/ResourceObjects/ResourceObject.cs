using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public ResourceType getResourceType => m_type;

    [SerializeField]
    private ResourceType m_type;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public ResourceType m_resourceType => _type;

    [SerializeField]
    private ResourceType _type;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    food,
    wood,
    stone
}
[System.Serializable]
public struct ResourcesCollection
{
    public int m_food;
    public int m_wood;
    public int m_stone;

    public ResourcesCollection(int a_food = 75, int a_wood = 150, int a_stone = 50)
    {
        m_food = a_food;
        m_wood = a_wood;
        m_stone = a_stone;
    }
}


[System.Serializable]
public class ResourceManager
{

    private ResourcesCollection m_resources;

    public ResourceManager()
    {
        m_resources = new ResourcesCollection(75, 150, 50);
    }

    public bool CanSpend(ResourcesCollection a_spendable)
    {
        if( m_resources.m_food >= a_spendable.m_food &&
            m_resources.m_stone >= a_spendable.m_stone &&
            m_resources.m_wood >= a_spendable.m_wood)

        {
            return true;
        }

        return false;
    }

    public void GainResource(ResourceType a_type, int a_amount)
    {
        switch(a_type)
        {
            case ResourceType.food:
                m_resources.m_food += a_amount;
                break;
            case ResourceType.wood:
                m_resources.m_wood += a_amount;
                break;
            case ResourceType.stone:
                m_resources.m_stone += a_amount;
                break;
        }
    }

    public void GainResource(ResourcesCollection a_amount)
    {
        m_resources.m_food += a_amount.m_food;
        m_resources.m_wood += a_amount.m_wood;
        m_resources.m_stone += a_amount.m_stone;
    }


    public bool SpentResource(ResourceType a_type, int a_amount)
    {
        switch (a_type)
        {
            case ResourceType.food:

                if (m_resources.m_food < a_amount)
                {
                    return false;
                }

                m_resources.m_food -= a_amount;

                break;
            case ResourceType.wood:

                if (m_resources.m_wood < a_amount)
                {
                    return false;
                }

                m_resources.m_wood -= a_amount;

                break;
            case ResourceType.stone:

                if (m_resources.m_stone < a_amount)
                {
                    return false;
                }

                m_resources.m_stone -= a_amount;

                break;
        }

        return true;
    }
    public bool SpentResource(ResourcesCollection a_amount)
    {
        bool canSpend = false;
        if (m_resources.m_food >= a_amount.m_food &&
                    m_resources.m_stone >= a_amount.m_stone &&
                    m_resources.m_wood >= a_amount.m_wood)

        {
            canSpend = true;
        }

        if (canSpend)
        {
            m_resources.m_food -= a_amount.m_food;
            m_resources.m_wood -= a_amount.m_wood;
            m_resources.m_stone -= a_amount.m_stone;
        }

        return canSpend;
    }
   
    public int GetResource(ResourceType a_type)
    {
        switch(a_type)
        {
            case ResourceType.food:
                return m_resources.m_food;
            case ResourceType.wood:
                return m_resources.m_wood;
            case ResourceType.stone:
                return m_resources.m_stone;
        }

        return 0;
    }
    public ResourcesCollection GetResources() { return m_resources; }
}

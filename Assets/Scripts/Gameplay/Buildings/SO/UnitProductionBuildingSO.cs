using UnityEngine;

[CreateAssetMenu(fileName = "UnitProductionBuilding", menuName = "Content/Buildings/Unit Production Building", order = 1)]
public class UnitProductionBuildingSO : BuildingSO
{
    public UnitSO getUnit => m_unit;
    public int getMaxQueue => m_maxQueue;

    [SerializeField]
    private UnitSO m_unit;
    [SerializeField]
    private int m_maxQueue = 5;
}

using UnityEngine;

[CreateAssetMenu(fileName = "UnitProductionBuilding", menuName = "Content/Buildings/Unit Production Building", order = 1)]
public class UnitProductionBuildingSO : BuildingSO
{
    public UnitSO m_unit => _unit;
    public int m_maxQueue => _maxQueue;

    [SerializeField]
    private UnitSO _unit;
    [SerializeField]
    private int _maxQueue = 5;
}

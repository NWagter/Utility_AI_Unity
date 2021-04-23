using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Content/Units/Unit", order = 1)]
public class UnitSO : ContentSO
{
    public string getName => m_name;
    public float getMilitaryStrenght => m_militaryStrenght;
    public GameObject getUnitObject => m_unitObject;

    public ResourcesCollection getCost => m_cost;
    public float getBuildTime => m_buildTime;

    [SerializeField]
    private string m_name = "Unit";
    [SerializeField]
    private float m_militaryStrenght = 0.5f;
    [SerializeField]
    private GameObject m_unitObject;

    [SerializeField]
    private ResourcesCollection m_cost;
    [SerializeField]
    private float m_buildTime;
}

using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Content/Units/Unit", order = 1)]
public class UnitSO : ContentSO
{
    public string m_name => _name;
    public float m_militaryStrenght => _militaryStrenght;
    public ResourcesCollection m_cost => _cost;

    [SerializeField]
    private string _name = "Unit";
    [SerializeField]
    private float _militaryStrenght = 0.5f;
    [SerializeField]
    private ResourcesCollection _cost;
}

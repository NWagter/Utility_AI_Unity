using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Content/Units/Unit", order = 1)]
public class UnitSO : ContentSO
{
    public string m_name => _name;
    public float m_militaryStrenght => _militaryStrenght;
    public GameObject m_unitObject => _unitObject;

    public ResourcesCollection m_cost => _cost;
    public float m_buildTime => _buildTime;

    [SerializeField]
    private string _name = "Unit";
    [SerializeField]
    private float _militaryStrenght = 0.5f;
    [SerializeField]
    private GameObject _unitObject;

    [SerializeField]
    private ResourcesCollection _cost;
    [SerializeField]
    private float _buildTime;
}

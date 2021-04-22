using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Content/Units/Unit", order = 1)]
public class UnitSO : ContentSO
{
    public string m_name => _name;

    [SerializeField]
    private string _name = "Unit";
}

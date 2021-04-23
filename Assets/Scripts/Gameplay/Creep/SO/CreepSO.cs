using UnityEngine;


[CreateAssetMenu(fileName = "Creep", menuName = "Content/Units/Creep", order = 1)]
public class CreepSO : ContentSO
{
    public int getAmountofCreeps => m_amountOfCreeps;
    public float getRespawnTIme => m_respawnTime;

    public string getName => m_name;
    public GameObject getCreepObject => m_creepObject;
    public float getStrenght => m_strenght;

    [Header("Creep Layer")]

    [SerializeField]
    private int m_amountOfCreeps = 2;
    [SerializeField]
    private float m_respawnTime;

    [Header("Creep Units")]

    [SerializeField]
    private string m_name = "Unit";
    [SerializeField]
    private GameObject m_creepObject;
    [SerializeField]
    private float m_strenght = 1.0f;

}

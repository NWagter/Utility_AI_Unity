using UnityEngine;

public enum TargetType
{
    Building,
    Unit
}

public interface ITargetable
{
    public GameObject GetTargetObject();
    public TargetType GetTargetType();
    public float GetStrenght();

    public bool CanTarget(Controller a_controller);
}

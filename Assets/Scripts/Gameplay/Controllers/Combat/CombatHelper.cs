using UnityEngine;
using System.Collections.Generic;


public class CombatHelper
{
    public static List<ITargetable> SearchForTargets(Vector3 a_pos, float a_range, Controller a_controller)
    {
        List<ITargetable> targets = new List<ITargetable>();

        //Sphere cast
        RaycastHit[] outhit = Physics.SphereCastAll(a_pos, a_range, Vector3.forward);


        foreach (RaycastHit hit in outhit)
        {
            if (hit.transform.GetComponentInParent<ITargetable>() != null)
            {
                ITargetable target = hit.transform.GetComponentInParent<ITargetable>();

                if(target.CanTarget(a_controller))
                    targets.Add(target);
            }
        }

        return targets;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public static PlayerVariables current;

    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Playerstate playerState = Playerstate.none;
}
public enum Playerstate
{
    none,
    placingBuilding
}
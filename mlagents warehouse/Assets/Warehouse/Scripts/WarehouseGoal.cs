using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseGoal : MonoBehaviour
{
    public WarehouseAgent warehouseAgent;

    public void GoalReached()
    {
        warehouseAgent.Done();
    }
}

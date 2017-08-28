using UnityEngine;
using System.Collections;

public class CheckCollison {

    private Vector3 positionLimitMin, positionLimitMax;

    public bool CheckLimits(Vector3 pos1, Vector3 pos2, int limit)
    {
        CalcLimits(pos2, limit);
        if (pos1.x >= this.positionLimitMin.x && pos1.x <= this.positionLimitMax.x)
        {          
            if (pos1.z >= this.positionLimitMin.z && pos1.z <= this.positionLimitMax.z)
            {
                return true;
            }
        }
        return false;
    }

    private void CalcLimits(Vector3 position, int limit)
    {
        this.positionLimitMin = new Vector3(Mathf.FloorToInt(position.x) - limit, Mathf.FloorToInt(position.y) - limit, Mathf.FloorToInt(position.z) - limit);
        this.positionLimitMax = new Vector3(Mathf.FloorToInt(position.x) + limit, Mathf.FloorToInt(position.y) + limit, Mathf.FloorToInt(position.z) + limit);
    }

	
}

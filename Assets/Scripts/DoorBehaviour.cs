using UnityEngine;
using System.Collections;

public class DoorBehaviour: DefautBehaviours
{

    private bool status;


    public DoorBehaviour(GameObject obj)
    {
        this.SetGameObject(obj);
        this.SetAnimController(obj.GetComponent<Animator>());
        doorState();
    }

   

    private bool doorState()
    {
        this.SetPosition(this.GetGameObject().GetComponent<Transform>().localRotation.eulerAngles);
        if (Mathf.FloorToInt(this.GetPosition().y) >= -1) 
        {
            return this.status = false;
        }
        return this.status = true;
    }

    public void doorAction()
    {
        doorState();
        if(this.status == false)
        {
            this.GetAnimController().Play("DoorOpen");
        }
        else
        {
            this.GetAnimController().Play("DoorClose");
        }

    }

}

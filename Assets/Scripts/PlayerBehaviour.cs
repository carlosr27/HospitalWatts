using UnityEngine;
using System.Collections;

public class PlayerBehaviour: DefautBehaviours {


    public PlayerBehaviour(GameObject obj)
    {
        this.SetGameObject(obj);
        this.SetPosition(new Vector3(obj.GetComponent<Transform>().position.x, obj.GetComponent<Transform>().position.y, obj.GetComponent<Transform>().position.z));
    }
}

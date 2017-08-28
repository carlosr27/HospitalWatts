using UnityEngine;
using System.Collections;

abstract public class DefautBehaviours {
    private string name;
    private GameObject gameobjct;
    private GameObject parentObj;
    private Vector3 tranObj;
    private Animator animController;

    protected Animator SetAnimController(Animator anim)
    {
        return this.animController = anim;
    }

    public Animator GetAnimController()
    {
        return this.animController;
    }

    protected GameObject SetInstatiatePrefab(string path)
    {
        return this.gameobjct = (GameObject)(GameObject.Instantiate(Resources.Load(path)) as GameObject);
    }

    public GameObject GetGameObject()
    {
        return this.gameobjct;
    }

    protected GameObject SetGameObject(GameObject obj)
    {
        return this.gameobjct = obj;
    }

    public Vector3 GetPosition()
    {
        return this.tranObj;
    }

    public Vector3 SetPosition(Vector3 pos)
    {
        return this.tranObj = new Vector3(pos.x, pos.y, pos.z);
    }

    public string GetName()
    {
        return this.name;
    }

    protected string SetName(string objName)
    {
        return this.name = objName;
    }

    public GameObject GetParent()
    {
        return this.parentObj;
    }

    protected GameObject SetParent(string objName)
    {
        return this.parentObj = GameObject.Find(objName);
    }
    

}

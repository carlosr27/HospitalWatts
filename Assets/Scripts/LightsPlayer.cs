using UnityEngine;


public class LightsPlayer : DefautBehaviours
{
    private bool pLightFlag = true;
   

    public LightsPlayer(GameObject obj)
    {
        this.SetGameObject(obj);    
    }

    public bool GetPLightFlag()
    {
        return this.pLightFlag;
    }

    public bool SetPLightFlag(bool flag)
    {
        return this.pLightFlag = flag;
    }
   
    public void GameObjectControll(bool lightRule)
    {
        if(lightRule)
        {
            this.GetGameObject().SetActive(!this.GetGameObject().activeSelf);
            this.pLightFlag = !pLightFlag;
        }
    }

  


}

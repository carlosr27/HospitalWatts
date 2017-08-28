using UnityEngine;


public class LightsCenario : DefautBehaviours{

    // posição em Z e X das luzes
    private float sceneLightsX;
    private float sceneLightsZ;

    public LightsCenario(string name, string path)
    {
        this.SetInstatiatePrefab(path);
        this.GetGameObject().name = this.SetName(name);
        this.GetGameObject().GetComponent<Transform>().position = this.SetPosition(new Vector3(0f, 3.294f, 0f));
        this.GetGameObject().GetComponent<Transform>().parent = this.SetParent("ScnLights").GetComponent<Transform>();
    }

    public float GetSceneLightsX()
    {
        return this.sceneLightsX;
    }

    public float SetSceneLightsX(float x)
    {
        return this.sceneLightsX = x;
    }

    public float GetSceneLightsZ()
    {
        return this.sceneLightsZ;
    }

    public float SetSceneLightsZ(float z)
    {
        return this.sceneLightsZ = z;
    }

    public void updatePos(Vector3 newPos)
    {
        this.SetPosition(newPos);
        this.GetGameObject().GetComponent<Transform>().position = this.GetPosition();
    }



}

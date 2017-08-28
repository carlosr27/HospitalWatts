using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BateryPlayer : LightsPlayer {

    private float bateryCharge = 2f;
    private Color32 bateryColor;
    private SpriteRenderer baterySprite;
  

    public BateryPlayer(GameObject obj):base(obj)
    {
        this.SetGameObject(obj);
        this.baterySprite = SetBatterySprite(this.GetGameObject().GetComponent<SpriteRenderer>());
        this.bateryColor = SetBatteryColor(this.GetBatterySprite().color);
        
    }

    
    public float GetBatteryCharge()
    {
        return this.bateryCharge;
    }

    public float SetBatteryCharge(float battery)
    {
        return this.bateryCharge = battery;
    }

    public Color32 GetBatteryColor()
    {
        return this.bateryColor;
    }

    public Color32 SetBatteryColor(Color32 color)
    {
        return this.bateryColor = color;
    }

    public Color32 GetBatteryColorGreen()
    {
        return new Color32(83, 231, 12, 255);
    }
    public Color32 GetBatteryColorRed()
    {
        return new Color32(231, 12, 25, 255);
    }

    public SpriteRenderer GetBatterySprite()
    {
        return this.baterySprite;
    }

    public SpriteRenderer SetBatterySprite(SpriteRenderer sprite)
    {
        return this.baterySprite = sprite;
    }

    public void UpdateBateryScene()
    {
        this.GetGameObject().GetComponent<Transform>().localScale = new Vector3(this.GetBatteryCharge(), this.GetGameObject().GetComponent<Transform>().localScale.y,
            this.GetGameObject().GetComponent<Transform>().localScale.z);
    }

    /********************************* THREADS *********************************/

    

    /*********************************** END ***********************************/

}

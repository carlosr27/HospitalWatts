using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;



public class Controller : MonoBehaviour {
    /******** CLASS REFRENCES ********/
    private BateryPlayer battery;
    private CheckCollison checkCollison;
    //private CRUD crud;
    private DoorBehaviour[] doorBehaviour;
    private Enemy enemy;
    private LightsCenario[] sceneLightsX, sceneLightsZ;
    private LightsPlayer playerLight;
    private PlayerBehaviour playerBehaviour;
    /************** END ***************/

    /*********** VARIABLES ************/
    private GameObject[] sceneDoors;
    private bool lostGame = false;
    private int timer;
    private AudioSource ambientAudio;
    public AudioClip[] audios;
    /************** END ***************/


    private Thread _calcBattery;
    private Thread _calcLights;
    private Thread _checkEnemyStatus;
    private Thread _enemyPoints;
    private Thread _timerScore;

    private void Start()
    {
        /*********** DATABASE ****************/
        // crud = new CRUD();
        /************** END ***************/

        /*********** LIGHTS ****************/
        sceneLightsX = new LightsCenario[7];
        sceneLightsZ = new LightsCenario[4];
        for (int i = 0; i < sceneLightsX.Length; i++)
        {
            sceneLightsX[i] = new LightsCenario("LightsX" + i, "Prefab/sceneLight");
        }

        for (int i = 0; i < sceneLightsZ.Length; i++)
        {
            sceneLightsZ[i] = new LightsCenario("LightsZ" + i, "Prefab/sceneLight");
        }

        _calcLights = new Thread(_calcLightsPos);
        _calcLights.Start();
        /************** END ***************/

        /*********** PLAYER LANTER ****************/
        playerLight = new LightsPlayer(GameObject.Find("LightCenter"));
        /************** END ***************/

        /*********** PLAYER BATTERY ****************/
        battery = new BateryPlayer(GameObject.Find("Battery"));
        /************** END ***************/

        /*********** PLAYER BEHAVIOUR ****************/
        playerBehaviour = new PlayerBehaviour(GameObject.FindGameObjectWithTag("Player"));
        /************** END ***************/

        /*********** DOOR BEHAVIOUR ****************/
        sceneDoors = GameObject.FindGameObjectsWithTag("Door");
        doorBehaviour = new DoorBehaviour[sceneDoors.Length];
        /************** END ***************/

        /*********** ENEMY ****************/
        enemy = new Enemy(GameObject.FindGameObjectWithTag("enemy"));
        /************** END ***************/

        /********* CHECK COLLISON *********/
        checkCollison = new CheckCollison();
        /************** END ***************/







        /*********** LIGHTS ****************/
        for (int i = 0; i < sceneLightsX.Length; i++)
        {
            sceneLightsX[i].updatePos(new Vector3(sceneLightsX[i].GetSceneLightsX(), sceneLightsX[i].GetPosition().y, sceneLightsX[i].GetSceneLightsZ()));
        }
        for (int i = 0; i < sceneLightsZ.Length; i++)
        {
            sceneLightsZ[i].updatePos(new Vector3(sceneLightsZ[i].GetSceneLightsX(), sceneLightsZ[i].GetPosition().y, sceneLightsZ[i].GetSceneLightsZ()));
        }
        /************** END ***************/

        /*********** PLAYER BATTERY ****************/
        _calcBattery = new Thread(_BatteryCalc);
        _calcBattery.IsBackground = true;
        _calcBattery.Start();
        /************** END ***************/

        /*********** DOOR BEHAVIOUR ****************/
        for (int i = 0; i < sceneDoors.Length; i++)
        {
            doorBehaviour[i] = new DoorBehaviour(sceneDoors[i]);
        }
        /************** END ***************/

        /*********** ENEMY ****************/    
        _enemyPoints = new Thread(_patrolPoints);
        _enemyPoints.Start();
        _checkEnemyStatus = new Thread(_checkStatus);
        _checkEnemyStatus.IsBackground = true;
        _checkEnemyStatus.Start();
        StartCoroutine(enemy.patrol());
        StartCoroutine(enemy.playAudio());
        /************** END ***************/

        /*********** SCORE ****************/
        _timerScore = new Thread(_scoreCount);
        _timerScore.IsBackground = true;
        _timerScore.Start();
        /************** END ***************/

        /*********** CONTROLLER AUDIOS ****************/
        ambientAudio = this.GetComponent<AudioSource>();
        StartCoroutine(playAudioCenario());
        /************** END ***************/

    }

    private void Update()
    {
        /*********** PLAYER LANTER ****************/
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerLight.GameObjectControll(PLightRules());
        }
        /************** END ***************/

        /*********** PLAYER BATTERY ****************/
        battery.GetBatterySprite().color = battery.GetBatteryColor();
        battery.UpdateBateryScene();
        if (playerLight.GetPLightFlag())
        {
            if (!PLightRules())
            {
                playerLight.GameObjectControll(true);
            }
        }
        /************** END ***************/

        /*********** PLAYER BEHAVIOUR ****************/
        playerBehaviour.SetPosition(new Vector3(playerBehaviour.GetGameObject().GetComponent<Transform>().position.x,
            playerBehaviour.GetGameObject().GetComponent<Transform>().position.y, playerBehaviour.GetGameObject().GetComponent<Transform>().position.z));
        /************** END ***************/

        /*********** ENEMY ****************/
        enemy.SetPosition(new Vector3(enemy.GetGameObject().GetComponent<Transform>().position.x,
            enemy.GetGameObject().GetComponent<Transform>().position.y, enemy.GetGameObject().GetComponent<Transform>().position.z));
        /************** END ***************/


        /*********** OPEN DOOR ****************/
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < sceneDoors.Length; i++)
            {
                if(checkCollison.CheckLimits(playerBehaviour.GetPosition(), sceneDoors[i].GetComponent<Transform>().position, 2))
                {
                    doorBehaviour[i].doorAction();
                }
            }

            if (checkCollison.CheckLimits(playerBehaviour.GetPosition(), GameObject.FindGameObjectWithTag("EndDoor").GetComponent<Transform>().position, 2))
            {
                SecurityClose();
                //crud.insert(timer);
                PlayerPrefs.SetInt("Score", timer);
                SceneManager.LoadScene(2);
            }

        }
        /************** END ***************/


        /*********** CHECK END GAME ****************/
        if (lostGame)
        {
            SecurityClose();
            SceneManager.LoadScene(1);
        }
        /************** END ***************/


    }

    void OnApplicationQuit()
    {
        SecurityClose(); 
    }

    private void SecurityClose()
    {
        try
        {
            _calcLights.Abort(); _calcBattery.Abort(); _checkEnemyStatus.Abort();
            _enemyPoints.Abort(); _timerScore.Abort();
        }
        catch (ThreadAbortException e)
        {
            Debug.Log(e);
        }
            
        
        StopAllCoroutines();
    }

    private bool PLightRules()
    {
        if (battery.GetBatteryCharge() > 0f)
        {
            return true;
        }
        return false;
    }

    /******************************* COROUTINES *******************************/

    public IEnumerator playAudioCenario()
    {
        while (true)
        {
            int rand = Mathf.FloorToInt(Random.Range(0, 1));

            
            Debug.Log(audios[rand]);
            ambientAudio.clip = audios[rand];
            ambientAudio.Play();
            

            yield return new WaitForSeconds(audios[rand].length);
        }
    }

    /*********************************** END ***********************************/


    /********************************* THREADS *********************************/

    private void _BatteryCalc()
    {
        while (true)
        {
            if (playerLight.GetPLightFlag() == true)
            {
                if (battery.GetBatteryCharge() > 0.1f)
                {
                    battery.SetBatteryColor(battery.GetBatteryColorGreen());
                    battery.SetBatteryCharge(battery.GetBatteryCharge() - 0.1f);
                    if (battery.GetBatteryCharge() >= 0.09f && battery.GetBatteryCharge() <= 0.7f)
                    {
                        battery.SetBatteryColor(battery.GetBatteryColorRed());
                    }
                    else
                    {
                        battery.SetBatteryColor(battery.GetBatteryColorGreen());
                    }
                }
                else
                {
                    battery.SetBatteryCharge(0.0f);
                }

                Thread.Sleep(10000);
            }
            else
            {
                Thread.Sleep(500);
            }
        }
    }

    ///calculo da posição inicial das luzes
    private void _calcLightsPos()
    {
        sceneLightsX[0].SetSceneLightsX(4.59f);
        sceneLightsX[0].SetSceneLightsZ(5.6f);
        sceneLightsZ[0].SetSceneLightsX(12.7f);
        sceneLightsZ[0].SetSceneLightsZ(-50.93f);
        for (int i = 1; i < sceneLightsX.Length; i++)
        {
            sceneLightsX[i].SetSceneLightsX(4.59f);
            sceneLightsX[i].SetSceneLightsZ(sceneLightsX[i - 1].GetSceneLightsZ() - 11.5f);
        }
        for (int i = 1; i < sceneLightsZ.Length; i++)
        {
            if (i == 1)
            {
                sceneLightsZ[i].SetSceneLightsX(-3.2f);
            }
            else
            {
                sceneLightsZ[i].SetSceneLightsX(sceneLightsZ[i - 1].GetSceneLightsX() - 12f);
            }
                sceneLightsZ[i].SetSceneLightsZ(-51.1f);
        }   
    }

    private void _checkStatus()
    {
        while(true)
        {
            enemy.SetChasePlayer(checkCollison.CheckLimits(enemy.GetPosition(), playerBehaviour.GetPosition(), 6));
            lostGame = checkCollison.CheckLimits(enemy.GetPosition(), playerBehaviour.GetPosition(), 2);
            Thread.Sleep(100);
        }
    }

    private void _patrolPoints()
    {
        enemy.patrolPoints[0] = new Vector3(-27.99f, -0.185f, -51.32f);
        enemy.patrolPoints[1] = new Vector3(4.73f, -0.185f, -51.32f);
        enemy.patrolPoints[2] = new Vector3(4.49f, -0.185f, -64.23f);
        enemy.patrolPoints[3] = new Vector3(12.93f, -0.185f, -51.17f);
        enemy.patrolPoints[4] = new Vector3(4.5f, -0.185f, -18.9f);
        enemy.patrolPoints[5] = new Vector3(4.5f, -0.185f, 6.9f);
    }

    private void _scoreCount()
    {
        while(true)
        {
            timer +=  1;
            Thread.Sleep(1000);
        }
    }



    /*********************************** END ***********************************/

}

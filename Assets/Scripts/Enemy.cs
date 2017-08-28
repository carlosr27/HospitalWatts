using UnityEngine;
using System.Collections;

public class Enemy: DefautBehaviours {

    private NavMeshAgent walkMesh;
    public Vector3[] patrolPoints;
    private int patrolIndex;
    private bool chasePlayer;
    private AudioSource audio;

	public Enemy(GameObject obj)
    {
        this.walkMesh = obj.GetComponent<NavMeshAgent>();
        this.SetGameObject(obj);
        this.patrolPoints = new Vector3[6];
        this.chasePlayer = false;
        this.audio =  obj.GetComponent<AudioSource>();
        audio.clip = (AudioClip)Resources.Load("Audio/blbla.wav");
        obj.GetComponent<Transform>().position = this.SetPosition(new Vector3(-27.99f, -0.185f, -51.32f));
    }

    private Vector3 updatePatrolPoint()
    {
        int index = this.patrolIndex;
        while(this.patrolIndex == index)
        {
            index = Mathf.FloorToInt(Random.Range(0, 6));
        }
        this.patrolIndex = index;
        return this.patrolPoints[this.patrolIndex];        
    }


    public Vector3 GetPatrolPoint()
    {
        return this.patrolPoints[patrolIndex];
    }

    public bool GetChasePlayer()
    {
        return this.chasePlayer;
    }

    public bool SetChasePlayer(bool decision)
    {
        return this.chasePlayer = decision;
    }


    /******************************* COROUTINES *******************************/

    public IEnumerator playAudio()
    {
        while (true)
        {
            audio.Play();
            
            yield return new WaitForSeconds(2000f);
        }
    }

    public IEnumerator patrol()
    {
        while(true)
        {
            if (this.chasePlayer == false)
            {
                if (this.walkMesh.remainingDistance < 1f)
                {
                    this.walkMesh.destination = updatePatrolPoint();
                }
            }
            else
            {
                this.walkMesh.destination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            }
            //Debug.Log(this.chasePlayer);
            yield return new WaitForSeconds(.5f);
        }
    }

    /*********************************** END ***********************************/
}

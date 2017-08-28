using UnityEngine;
using UnityEngine.UI;

public class EndgameController : MonoBehaviour {

    private CRUD crud;
    public GameObject text;

    // Use this for initialization
    void Start () {

        /*********** DATABASE ****************/
        //crud = new CRUD();
        /************** END ***************/

        //text.GetComponent<Text>().text += crud.Select()[i]+"'s".ToString();
        text.GetComponent<Text>().text = PlayerPrefs.GetInt("Score")+ "'s".ToString();
        //text.GetComponent<Text>().text += "\n";
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}

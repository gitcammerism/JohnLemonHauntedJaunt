using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject player;
    public GameObject gameEnding;
    public GameEnding endScript;
    
    private bool timerDone = false;
    private float waitTime;

    void Start()
    {
        //Find game objects
        player = GameObject.FindWithTag("Player");
        gameEnding = GameObject.FindWithTag("GameController");
        endScript = gameEnding.GetComponent<GameEnding>();
        //Timer starts
        StartCoroutine(Waiting());
    }

    void Update()
    {
        //Turns towards the player
        transform.LookAt(player.transform);
        
        //Moves after wait
        if(timerDone == true)
        {
            transform.Translate(Vector3.forward * 1.3f * Time.deltaTime);
        }
        
    }

    
    IEnumerator Waiting()
    {
        //Provides a chasing buffer
        yield return new WaitForSeconds(3f);
        timerDone = true;
    }


    void OnTriggerEnter(Collider other)
    {
        //Runs if collision after wait time
        if(other.gameObject.tag == "Player" && timerDone == true)
        {

            if (endScript == null)
            {
                Debug.Log("endScript not found");
            }
            else
            {
                //Ends game
                endScript.CaughtPlayer();
            }
            
        }
    }

}

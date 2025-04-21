using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject lavaMonster;
    private GameObject monsterPrefab;
    public GameObject explosion;
    public GameObject spawnner;

    public Vector3 spawnPos;
    private bool spawned = false;
    private Coroutine lifeSpan;
    private float chargeTime;
    public bool attack = false;
    private Animation anim;

    public EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        //Sets spawn position
        spawnPos = spawnner.transform.position;
    }


    void SpawnMonster()
    {
        //Plays explosion effect
        ParticleSystem ex = Instantiate(explosion, spawnPos, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(ex.gameObject, ex.main.duration + ex.main.startLifetime.constantMax);
        //Instantiates monster prefab
        monsterPrefab = Instantiate(lavaMonster, spawnPos, Quaternion.identity);
        anim = monsterPrefab.GetComponent<Animation>();
        enemyScript = monsterPrefab.GetComponent<EnemyScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Performs monster lifecycle on trigger
        if(other.tag == "Player" && spawned == false)
        {
            StartCoroutine(lifeTime());
            spawned = true;
        }
    }

    IEnumerator lifeTime()
    {
        //Spawns monster
        SpawnMonster();
        //Follows before attack anim
        chargeTime = Random.Range(6f, 8f);
        yield return new WaitForSeconds(chargeTime);
        //formally attacks
        attack = true;
        //Plays attack animation
        anim.Play("Anim_Attack");
        yield return new WaitForSeconds(2f);
        //Destroys monster
        Destroy(monsterPrefab);
        spawned = false;
    }

    
}

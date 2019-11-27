using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletCollisionClientWhoShooted : NetworkBehaviour
{
    private float brojac = 0;
    private int dmg;
    private string playerWhoShooted;

    void Start()
    {
        dmg = GetComponent<Weapon>().damage;
        playerWhoShooted = GetComponent<Weapon>().playerWhoShooted;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            //Debug.Log("usao");
            PlayersManager.players[playerWhoShooted].GetComponent<PlayerStatus>().CmdTakeDamage(other.gameObject.name,playerWhoShooted, dmg);
        }
        //Efekat ovde!!!
        Destroy(gameObject);
    }
    void Update()
    {
        brojac += Time.deltaTime;
        if (brojac > 2.0f)
        {
            Destroy(gameObject);
        }
    }
}

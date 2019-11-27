using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerStatus:NetworkBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    [SyncVar]
    public int health = 100;
    private string playerName = "";
    [SyncVar]
    private int kills = 0;
    [SyncVar]
    private int deaths = 0;
    [SerializeField]
    public Image healthBar;
    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Text txtKills;
    [SerializeField]
    private Text txtDeaths;

    public string PlayerName { get => playerName; set => playerName = value; }
    public int Health { get => health; set => health = value; }
    public int Kills { get => kills; set => kills = value; }
    public int Deaths { get => deaths; set => deaths = value; }

    void Start()
    {
        health = maxHealth;
        txtName.text = PlayerName;
    }
    public override string ToString() {
        return playerName +" "+ kills +" "+ deaths +" "+health;
    }
    [Command]
    public void CmdTakeDamage(string whoIsHitted,string whoShooted,int dmg)
    {
        PlayerStatus hittedPlayer = PlayersManager.players[whoIsHitted];
        hittedPlayer.health -= dmg;
        int realHealth = hittedPlayer.health;
        if (realHealth <= 0)
        {
            PlayersManager.players[whoShooted].MakeKill();
            PlayersManager.players[whoIsHitted].Killed();
        }
    }
    public void MakeKill() { 
        kills += 1;
        RpcPerformKill(kills);
    }
    [ClientRpc]
    public void RpcPerformKill(int kills)
    {
        txtKills.text = kills + "";
    }
    public void Killed()
    {
        deaths += 1;
        health = 99999;
        RpcPerformDeath(deaths);
    }
    [ClientRpc]
    public void RpcPerformDeath(int deaths)
    {
        transform.Translate(Vector3.up * 50);
        txtDeaths.text = deaths + "";
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Move>().enabled = false;
        StartCoroutine(RespawnWait());
    }
    public IEnumerator RespawnWait()
    {
        yield return new WaitForSeconds(3);
        health = maxHealth;
        GetComponent<Rigidbody>().useGravity = true;
        if (isLocalPlayer)
        {
            GetComponent<Move>().enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager:MonoBehaviour
{
    public static Dictionary<string, PlayerStatus> players = new Dictionary<string, PlayerStatus>();
    public static void AddPlayer(string playerName,PlayerStatus player)
    {
        players.Add(playerName, player);
    }
    public static void DeletePlayer(string playerName)
    {
        players.Remove(playerName);
    }
    public static PlayerStatus GetPlayer(string playerName)
    {
        return players[playerName];
    }
    void OnGUI() {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();
        foreach(string key in players.Keys)
        {
            GUILayout.Label(key + "--" + players[key].PlayerName + "--" + players[key].Health+"--"+ players[key].Kills + "--" + players[key].Deaths);
            players[key].healthBar.fillAmount = (float)players[key].health / (float)players[key].maxHealth; 
            players[key].healthBar.fillAmount = (float)players[key].health / (float)players[key].maxHealth;
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

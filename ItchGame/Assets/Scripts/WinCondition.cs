using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private static List<PlayerDamagable> m_players = null;
    private int m_livingPlayers = 0;

    private void LateUpdate()
    {
        m_livingPlayers = 0;
        m_players.ForEach(Check);

        if (m_livingPlayers < 2)
        {
            Debug.Log(">>>>>>>>> REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        }
    }

    private void Check(PlayerDamagable x)
    {
        if (x != null)
        {
            if (x.gameObject.activeSelf) m_livingPlayers++;
        }
        else
        {
            m_players.Remove(x);
        }
    }

    internal static void RegisterPlayer(PlayerDamagable x)
    {
        m_players.Add(x);
    }

    internal static void RemovePlayer(PlayerDamagable x)
    {
        m_players.Remove(x);
    }
}
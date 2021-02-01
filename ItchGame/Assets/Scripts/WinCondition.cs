using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private List<PlayerDamagable> m_players = null;
    private int m_livingPlayers = 0;

    private void Start()
    {
        StartCoroutine(Find());
    }

    private void LateUpdate()
    {
        m_livingPlayers = 0;
        m_players.ForEach(check);

        if (m_livingPlayers < 2)
        {
            Debug.Log(">>>>>>>>> REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        }
    }

    private void check(PlayerDamagable x)
    {
        if (x.gameObject.activeSelf) m_livingPlayers++;
    }

    private IEnumerator Find()
    {
        yield return new WaitForSeconds(2);
        m_players = new List<PlayerDamagable>(FindObjectsOfType<PlayerDamagable>());
    }
}
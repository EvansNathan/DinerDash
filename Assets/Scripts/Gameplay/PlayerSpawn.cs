using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    void Start()
    {
        var temp = GameState.Instance.playerCharacters[GameState.Instance.selectedCharacter];

        Instantiate(temp, gameObject.transform.position, Quaternion.identity);
    }
    
}

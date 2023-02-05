using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private UnityEngine.Tilemaps.Tilemap grid;
    [SerializeField]
    private List<bacteria_tracking> bacteria;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation); //Spawns the player at the location of the PlayerSpawn gameobject
        playerInstance.GetComponent<PlayerPawn>().grid = this.grid; //The grid is stored in the PlayerSpawn, since the grid will be a different instance for each level.

        foreach(bacteria_tracking bacter in bacteria)
        {
            bacter.Player = playerInstance;
        }
    }
}

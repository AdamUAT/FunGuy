using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WBloodPawn : MonoBehaviour
{
    [SerializeField]
    private Tilemap path; //The grid that Wblood's path exhists in, and will be invisible at runtime.
    [SerializeField]
    private Tile pathTile; //The tile that represents the Wblood's path.

    [SerializeField]
    private float movementSpeed = 1.0f;

    //Where the Wblood should stop moving.
    private Vector2 destination;
    //This will tell if the Wblood overshot it's destination, which can happen if the FPS drops.
    private Vector2 originalPosition;
    //This is the direction the Wblood should be moving.
    private Vector2 direction;

    //Kickstarts the Wblood on its path.
    private void Start()
    {
        originalPosition = transform.position;

        for (int i = 0; i < 4; i++)
        {
            direction = new Vector2(Mathf.Sin(Mathf.PI * i / 2), Mathf.Cos(Mathf.PI * i / 2));
            Vector2 potentialDestination = direction.normalized * path.cellSize + (Vector2)transform.position;
            if (path.GetTile(path.WorldToCell(potentialDestination)) == pathTile)
            {
                destination = (Vector2)transform.position + direction.normalized * path.cellSize;
                return;
            }
        }
    }

    private void Update()
    {
        //Moves wblood in the direction of its destination.
        transform.position += (Vector3)direction.normalized * Time.deltaTime * movementSpeed;

        //Detects if the Wblood reached its destination, or overshot it.
        if (((Vector2)transform.position - originalPosition).magnitude >= (destination - originalPosition).magnitude)
        {
            transform.position = destination; //If it overshot, then it teleports to where it should be.
            MoveToNextPath();
        }
    }

    private void MoveToNextPath()
    {

        Vector3Int previousPath = path.WorldToCell(originalPosition); //Stores which path the Wblood was previously on, and won't travel there unless it's the end of the line.

        destination = originalPosition;
        originalPosition = transform.position;

        for (int i = 0; i < 4; i++)
        {

            direction = new Vector2(Mathf.Sin(Mathf.PI * i / 2), Mathf.Cos(Mathf.PI * i / 2));
            Vector2 potentialDestination = direction.normalized * path.cellSize + (Vector2)transform.position;
            if (path.GetTile(path.WorldToCell(potentialDestination)) == pathTile && potentialDestination != destination)
            {
                destination = (Vector2)transform.position + direction.normalized * path.cellSize;
                return;
            }
        }
        //This code runs after it tries to find a new path not taken, meaning it will take the first path it finds, which will be backtracking.
        for (int i = 0; i < 4; i++)
        {

            direction = new Vector2(Mathf.Sin(Mathf.PI * i / 2), Mathf.Cos(Mathf.PI * i / 2));
            Vector2 potentialDestination = direction.normalized * path.cellSize + (Vector2)transform.position;
            if (path.GetTile(path.WorldToCell(potentialDestination)) == pathTile)
            {
                destination = (Vector2)transform.position + direction.normalized * path.cellSize;
                return;
            }
        }
    }
}

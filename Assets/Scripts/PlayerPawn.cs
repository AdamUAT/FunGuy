using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerPawn : MonoBehaviour
{
    public Tilemap grid;

    //Tells the Update function to continue moving and to ignore any movement inputs.
    private bool isMoving = false;
    //Where the player should stop moving.
    private Vector2 destination;
    //This will tell if the player overshot it's destination, which can happen if the FPS drops.
    private Vector2 originalPosition;
    //This is the direction the player should be moving.
    private Vector2 direction;
    //This is the speed the player should move. It is exposed to the designers for testing.
    [SerializeField]
    private float movementSpeed;
    //These are the auto tiles that will be filled in as the player moves around the map.
    [SerializeField]
    private RuleTile rootTiles;


    private void Update()
    {
        if (isMoving)
        {
            transform.position += (Vector3)direction.normalized * Time.deltaTime * movementSpeed;
            //Detects if the player reached its destination, or overshot it.
            if (((Vector2)transform.position - originalPosition).magnitude >= (destination - originalPosition).magnitude)
            {
                Vector3Int gridPosition = grid.WorldToCell(destination);
                grid.SetTile(gridPosition, rootTiles);
                isMoving = false;
                transform.position = destination; //If it overshot, then it teleports to where it should be.

            }
        }
    }

    public void MoveUp()
    {
        if(CanMove(transform.position, Vector2.up))
        {
            MoveTowards(Vector2.up);
        }
    }
    public void MoveDown()
    {
        if (CanMove(transform.position, Vector2.down))
        {
            MoveTowards(Vector2.down);
        }
    }
    public void MoveLeft()
    {
        if (CanMove(transform.position, Vector2.left))
        {
            MoveTowards(Vector2.left);
        }
    }
    public void MoveRight()
    {
        if (CanMove(transform.position, Vector2.right))
        {
            MoveTowards(Vector2.right);
        }
    }

    //Interprets where to move the player and starts moving it.
    public void MoveTowards(Vector2 direction)
    {
        isMoving = true;
        //grid.cellSize shows how far away the next cell is, and since the player will be in the middle of the cell, the destination should be the middle of the next cell.
        destination = transform.position + (Vector3)(direction.normalized * grid.cellSize);
        originalPosition = transform.position;
        this.direction = direction;
    }

    //Checks to see if the player is trying to move to an eligible grid cell.
    private bool CanMove(Vector2 position, Vector2 direction)
    {
        //Converts the current position of the player to the would-be position of the player in the grid's coordinates.
        Vector3Int gridPosition = grid.WorldToCell(position + (direction.normalized * grid.cellSize));
        Collider2D[] tempColliders = Physics2D.OverlapCircleAll(position + (direction.normalized * grid.cellSize), 0.1f); //saves the collider so we only have to call overlap cicle once.
        foreach(Collider2D collider in tempColliders)
        {
            if (!collider.isTrigger)
                return false;
        }
        return (grid.GetColliderType(gridPosition) == Tile.ColliderType.None && !isMoving); //If the cell in the grid that the player won't be moving to has no collider and the player isn't already moving, then return true.
    }
}

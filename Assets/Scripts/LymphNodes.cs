using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickableType { SCORE, ITEM }
public class LymphNodes : MonoBehaviour

{
    [SerializeField]
    private PickableType type;
    [SerializeField]
    private int points = 0;
    [SerializeField]
   
    bool isCollected = false;
    private void Update()
    {
        if (isCollected)
        {
            if (type.Equals(PickableType.SCORE))
            {
                GameManager.instance.addScorePoints(points);
               
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            isCollected = true;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCasualChatTrigger : MonoBehaviour
{

  [SerializeField]
  Vector2 randomChatRange = new Vector2(5.0f, 15.0f);

  public UnityEvent onCasualChatTrigger;
  float nextTriggerTime;

  private float GetRandomTime()
  {
    return Random.Range(randomChatRange.x, randomChatRange.y);
  }
  // Start is called before the first frame update
  void Start()
  {
    nextTriggerTime = Time.time + GetRandomTime();
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time > nextTriggerTime)
    {
      onCasualChatTrigger.Invoke();
      nextTriggerTime = Time.time + GetRandomTime();
    }

  }
}

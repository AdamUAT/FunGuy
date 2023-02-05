using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverExpand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField]
   private float expanded_scale = 1.1f;

   [SerializeField]
   private float expand_rate = 0.5f;

   private Vector3 original_scale = Vector3.one;
   private bool mouse_entered = false;
   private float timeStart = 0.0f;

   private void Start()
   {
      original_scale = transform.localScale;
   }

   private void Update()
   {
      if(mouse_entered)
      {
         // Calculate size based on time passed
         float frequency = 1.0f/expand_rate;
         float time_elapsed = Time.time - timeStart;

         //Using Abs(Sin(t)) to give a bouncing effect
         float transition = Mathf.Abs(Mathf.Sin(time_elapsed*frequency));
         float expanded_scale_temp = Mathf.Lerp(1.0f, expanded_scale, transition);

         
         // Scale to new size
         transform.localScale = original_scale * expanded_scale_temp;
      }
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
      // Start timer
      mouse_entered = true;
      timeStart = Time.time;
   }
 
   public void OnPointerExit(PointerEventData eventData)
   {
      // Returns to Original size.
      mouse_entered = false;
      transform.localScale = original_scale; 
   }
}
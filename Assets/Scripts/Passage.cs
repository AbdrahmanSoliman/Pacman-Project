using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
   public Transform connection;

   private void OnTriggerEnter2D(Collider2D other) 
   {
        Vector3 position = other.transform.position; // to store the z value specifically, since we don't want to change the z value
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;

        other.transform.position = position;
   }
}

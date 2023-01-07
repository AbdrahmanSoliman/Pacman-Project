using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;

    private void OnEnable() 
    {
        StopAllCoroutines();    
    }

    private void OnDisable() 
    {
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());    
        }   
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(this.enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }    
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true); // for running the right sprite (ghost's eyes be looking up)
        this.ghost.movement.rb.isKinematic = true; // to ignroe colliders
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        // firstly move to inside position (in front of the gate) 
        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed+= Time.deltaTime;
            yield return null; // to update elapsed after a single frame (animation purposes)
        }

        elapsed = 0.0f;
        
        // then move to outside transform position from the gate
        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed+= Time.deltaTime;
            yield return null; // to update elapsed after a single frame (animation purposes)
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), false); // go left/right 50:50 chance
        this.ghost.movement.rb.isKinematic = false;
        this.ghost.movement.enabled = true;

    }
}

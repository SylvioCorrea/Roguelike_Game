using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public void TurnIfNeeded() {
        float vecX = rigidBody.velocity.x;
        if(vecX != 0 && Mathf.Sign(vecX)!=Mathf.Sign(transform.localScale.x)) {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }
    }

    public abstract void PatrolPointReached();
}

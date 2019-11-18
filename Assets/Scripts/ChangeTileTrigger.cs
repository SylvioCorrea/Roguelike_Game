using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTileTrigger : MonoBehaviour
{
    public Transform[] locationsToBlock;
    public Tilemap tilemap;
    public Tile wall;
    // Start is called before the first frame update
    
    public void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Player")) {
            foreach(Transform t in locationsToBlock) {
                Vector3Int tileLocation = tilemap.WorldToCell(t.position);
                tilemap.SetTile(tileLocation, wall);
            }
            
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTileTrigger : MonoBehaviour, IListener
{
    public Transform listeningTo;
    //WARNING: This transform should implement IDeathNotifier.


    public bool tileChangeTriggered;
    public Transform[] locationsToBlock;
    public List<TileBase> originalTiles;
    public Tilemap tilemap;
    public Tile wall;
    // Start is called before the first frame update
    
    void Awake()
    {
        originalTiles = new List<TileBase>();
    }
    void Start()
    {
        if(listeningTo != null) {
            listeningTo.GetComponent<IDeathNotifier>().SubscribeListener(this);
        }

        //Save the original tiles for rebuilding later
        for(int i=0; i<locationsToBlock.Length; i++) {
            TileBase tile = tilemap.GetTile(tilemap.WorldToCell(locationsToBlock[i].position));
            originalTiles.Add(tile);
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {
        //Close the path with walls once the player has stepped inside the trigger zone
        if(!tileChangeTriggered && col.gameObject.CompareTag("Player")) {
            tileChangeTriggered = true;
            foreach(Transform t in locationsToBlock) {
                Vector3Int tileLocation = tilemap.WorldToCell(t.position);
                tilemap.SetTile(tileLocation, wall);
            }
        }
    }

    public void Notify()
    {
        //Remove the walls by placing back the original tiles
        for(int i=0; i<locationsToBlock.Length; i++) {
            Vector3Int tileLocation = tilemap.WorldToCell(locationsToBlock[i].position);
            tilemap.SetTile(tileLocation, originalTiles[i]);
        }
    }
}

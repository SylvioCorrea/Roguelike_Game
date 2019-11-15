using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbersScript : MonoBehaviour
{
    public float yTranslate;
    public float destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyTimer > 0) {
            transform.position += new Vector3(0, yTranslate, 0) * Time.deltaTime;
            destroyTimer -= Time.deltaTime;
        } else Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteFx_afterClear : MonoBehaviour
{
    void Start()
    {
        
    }
    private float deleteTime = 0.33f;
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if(deleteTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

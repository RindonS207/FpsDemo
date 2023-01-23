using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public Transform camera;
    public float move_speed = 3;
    public float gravity = 2;
    public static float heart = 100;
    public static bool isAlive = true;
    public static double score = 0;
    public static int ammoAmount = 100;
    public float height = 1.4f;
    public float damage = 4;
    public LayerMask layer;
    public Transform xiaoGuo;
    public Transform gun;
    public AudioClip clip;
    Vector3 camRot;
    Vector3 playerRot;
    Transform trans;
    AudioSource ad;
    float shotTimer = 0;
    CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        player = GetComponent<CharacterController>();
        camera.position = trans.TransformPoint(0, height, 0);
        camera.rotation = trans.rotation;
        camRot = camera.eulerAngles;
        playerRot = Vector3.zero;
        ad = GetComponent<AudioSource>();
        //Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(heart <= 0)
        {
            isAlive = false;
        }
        if(isAlive)
        {
            moveMetHod();
        }
        if(shotTimer >= -4)
        {
            shotTimer -= Time.deltaTime;
        }
        if(Input.GetMouseButton(0) && shotTimer <= 0)
        {
            shotTimer = 0.1f;
            ad.PlayOneShot(clip);
            ammoAmount -= 1;
            RaycastHit info;
            bool isHit = Physics.Raycast(gun.position, camera.TransformDirection(Vector3.forward), out info, 100, layer);
            if(isHit)
            {
                if(info.transform.tag == "diren")
                {
                    enemy en = info.transform.GetComponent<enemy>();
                    en.getHurt(damage);
                }
                Instantiate(xiaoGuo, info.point, info.transform.rotation);
            }
            
        }
    }
    private void moveMetHod()
    {
        Vector3 mov = Vector3.zero;
        mov.x = Input.GetAxis("Horizontal") * move_speed * Time.deltaTime;
        mov.z = Input.GetAxis("Vertical") * move_speed * Time.deltaTime;
        mov.y -= gravity * Time.deltaTime;
        player.Move(trans.TransformDirection(mov));
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");
        playerRot.y = rh;
        float value = camRot.x;
        if((value - rv) > 90)
        {
            camRot.x = 90;
        }
        else if((value - rv) < -90)
        {
            camRot.x = -90;
        }
        else
        {
            camRot.x -= rv;
        }
        //camRot.x -= rv;
        
        camRot.y += rh;
        camera.eulerAngles = camRot;
        trans.Rotate(playerRot);
        camera.position = trans.TransformPoint(0, height, 0);
    }
}

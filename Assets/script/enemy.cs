using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("diren")]
public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = move_speed;
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        gameInfo.nowEnemy += 1;
    }
    public GameObject player;
    public float move_speed = 2.5f;
    public float rotateSpeed = 5.0f;
    public float time_TIME = 1;
    public float heart = 20;
    public float damage = 5;
    private bool isAlive = true;
    Transform trans;
    NavMeshAgent agent;
    Animator anim;
    // Update is called once per frame
    void Update()
    {
        if (this.heart <= 0)
        {
            anim.Play("death");
            gameInfo.nowEnemy -= 1;
            CapsuleCollider cap= this.GetComponent<CapsuleCollider>();
            cap.enabled = false;
            this.isAlive = false;
        }
        if (playerControl.heart <= 0 && this.isAlive)
        {
            return;
        }
        time_TIME -= Time.deltaTime;
        AnimatorStateInfo stateINFO = anim.GetCurrentAnimatorStateInfo(0);
        float jvLi = Vector3.Distance(player.transform.position, trans.position);
        if (stateINFO.IsName("idle") && anim.IsInTransition(0) == false && isAlive)
        {
            if(time_TIME <= 0)
            {
                anim.SetBool("wait",false);
                anim.SetBool("walk", true);
                agent.SetDestination(player.transform.position);
                time_TIME = 1;
            }
            else
            {
                return ;
            }
        }
        if(jvLi <= 1.5f && isAlive)
        {
            anim.Play("attack");
            time_TIME = 1;
            agent.ResetPath();
            turnAround();
        }
        else if(stateINFO.IsName("run") && anim.IsInTransition(0) == false && isAlive)
        {
            if(time_TIME <= 0)
            {
                agent.SetDestination(player.transform.position);
                time_TIME = 1;
                return ;
            }
            else if(jvLi <= 1.5f)
            {
                anim.Play("attack");
                
                time_TIME = 1;
                agent.ResetPath();
                turnAround();
            }
        }
        if (stateINFO.IsName("attack") && anim.IsInTransition(0) == false && isAlive)
        {
            if (stateINFO.normalizedTime >= 1.0f)
            {
                anim.SetBool("wait", true);
                anim.SetBool("walk", false);
                anim.Play("idle");
                playerControl.heart -= damage;
                time_TIME = 1;
                return;
            }
        }
        if(stateINFO.IsName("death") && anim.IsInTransition(0) == false && isAlive == false)
        {
            if (stateINFO.normalizedTime >= 1.0f)
            {
                var p = PathologicalGames.PoolManager.Pools["mypool"];
                playerControl.score += 100;
                if (p.IsSpawned(trans))
                {
                    p.Despawn(trans);
                }
                else
                {
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
    private void turnAround()
    {
        Vector3 dir = player.transform.position - trans.position;
        Vector3 newDir = Vector3.RotateTowards(trans.forward, dir, rotateSpeed * Time.deltaTime, 0.0f);
        trans.rotation = Quaternion.LookRotation(newDir);
    }
    public void getHurt(float damage)
    {
        this.heart -= damage;
    }
    void OnEnable()
    {
        this.heart = 20;
        CapsuleCollider cap=this.GetComponent<CapsuleCollider>();
        cap.enabled = true;
        this.isAlive = true;
    }
}

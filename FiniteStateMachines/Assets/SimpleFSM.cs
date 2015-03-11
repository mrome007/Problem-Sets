using UnityEngine;
using System.Collections;

public class SimpleFSM : FSM 
{
    public enum FSMState
    {
        NONE,
        PATROL,
        CHASE,
        ATTACK,
        DEAD
    }
    public GameObject TankEnemy;
    private Transform tankPosOrig;
    //current state that the NPC is reaching.
    public FSMState CurState;

    //speed of the tank
    private float curSpeed;

    //tank rotation speed
    private float curRotSpeed;

    //Bullet
    public GameObject Bullet;

    //whether the NPC is destroyed or not
    private bool bDead;
    private int health;

    protected override void Initialize()
    {
        tankPosOrig = gameObject.transform;
        CurState = FSMState.PATROL;
        //Debug.Log(CurState);
        curSpeed = 4.0f;
        curRotSpeed = 2.0f;

        bDead = false;
        elapsedTime = 0.0f;
        shootRate = 3.0f;
        health = 100;

        //get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoints");

        FindNextPoint();

        //get the target enemy player
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

        playerTransform = objPlayer.transform;
        
        if(!playerTransform)
        {
            //Debug.Log("player doesn't exist");
            return;
        }
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;
    }

    protected override void FSMUpdate()
    {
        switch(CurState)
        {
            case FSMState.PATROL:
                UpdatePatrolState();
                break;
            case FSMState.CHASE:
                UpdateChaseState();
                break;
            case FSMState.ATTACK:
                UpdateAttackState();
                break;
            case FSMState.DEAD:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;
        if (health <= 0)
            CurState = FSMState.DEAD;
    }

    protected void UpdatePatrolState()
    {
        //Debug.Log(CurState);
        destPos = playerTransform.position;
        //Find another random patrol point if the current point is reached.
        if(Vector3.Distance(transform.position,destPos) <= 0.5f)
        {
            FindNextPoint();
        }
        else if(Vector3.Distance(transform.position, playerTransform.position) <= 8.0f)
        {
            CurState = FSMState.CHASE;
        }

        //rotate to target point.
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        //go forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void FindNextPoint()
    {
        int rndIndex = Random.Range(0, pointList.Length);
        float rndRadius = 10.0f;
        Vector3 rndPosition = Vector3.zero;
        destPos = pointList[rndIndex].transform.position + rndPosition;


        //check range to decide the random point as the same as before
        if(!IsInCurrentRange(destPos))
        {
            rndPosition = new Vector3(Random.Range(-rndRadius, rndRadius),
                0.0f, Random.Range(-rndRadius, rndRadius));
            destPos = pointList[rndIndex].transform.position + rndPosition;
        }
    }

    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xpos = Mathf.Abs(pos.x - transform.position.x);
        float zpos = Mathf.Abs(pos.z - transform.position.z);

        if(xpos <= 1.5f && zpos <= 1.5f)
        {
            return true;
        }
        return false;
    }

    protected void UpdateChaseState()
    {
        //Debug.Log(CurState);
        //set the target position as the player position.
        destPos = playerTransform.position;

        //check the distance with player tank when the distance is near,
        //transition to attack state.
        float dis = Vector3.Distance(transform.position, playerTransform.position);
        if(dis <= 7.5f)
        {
            CurState = FSMState.ATTACK;
        }
        else if(dis >= 8.0f)
        {
            CurState = FSMState.PATROL;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateAttackState()
    {
        //Debug.Log(CurState);
        destPos = playerTransform.position;
        float dis = Vector3.Distance(transform.position, playerTransform.position);

        if(dis >= 4.0f && dis < 8.0f)
        {
            //rotate to the target point.
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
            CurState = FSMState.ATTACK;
        }
        else if(dis >= 9.0f)
        {
            CurState = FSMState.PATROL;
        }

        //turn the turret towards the player.
        Quaternion turretRotation = Quaternion.LookRotation(destPos - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * curRotSpeed);
        ShootBullet();
    }

    private void ShootBullet()
    {
        if(elapsedTime >= shootRate)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
    }

    protected void UpdateDeadState()
    {
        if(!bDead)
        {
            bDead = true;
            int num = Random.Range(1, 5);
            for (int i = 0; i < num; i++ )
                Instantiate(TankEnemy, new Vector3(Random.Range(-18f,18f), 0.0f, Random.Range(-20f,20f)), Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hello there");
            health -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }
}

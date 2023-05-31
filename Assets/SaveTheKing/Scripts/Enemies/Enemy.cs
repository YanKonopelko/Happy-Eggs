using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoCache
{
    private const float speed = 7;
    private int priority = 0;
    [SerializeField] private float force = 10;
    private Rigidbody2D rb;

    private Transform myTransform;
    
    
    public float random_point_radius = 1; 

    Vector3 random_point;
    public Transform player;
    public GameObject near_player_prefab; 

    Transform near_player;
    Transform my_transform;
    Transform target;
    NavMeshAgent agent;
    NavMeshPath nav_mesh_path;

    private float detectionDistance = 1.5f;

    public bool isBacking = false;

    private SpriteRenderer graphic;
    void Start()
    {
        graphic = transform.GetChild(0).GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        my_transform = GetComponent<Transform>();
        nav_mesh_path = new NavMeshPath();
        target = player;
        near_player = Instantiate(near_player_prefab, transform.position, Quaternion.identity).transform;
    }
    protected override void OnEnabled()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        myTransform = transform;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.EagleSound);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
        if (col.gameObject.GetComponent<DrowLine>() != null)
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(force*rb.velocity.normalized,ForceMode2D.Impulse);
            rb.AddForce(-1*(force/2*rb.velocity.normalized),ForceMode2D.Impulse);
        }
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }

    protected override void Run()
    {
        
        Vector3 diference = target.position - myTransform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        myTransform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        graphic.flipY = rotateZ < -89;
        
    }

    public void SetTarget(Transform transform)
    {
        player = transform;
    }
    void Go_to_near_random_point() // 

     {
          bool get_correct_point = false;
          int i = 0;
          while (i < 5)
          {
               NavMeshHit navmesh_hit;
               NavMesh.SamplePosition(Random.insideUnitCircle * random_point_radius + (Vector2)player.position,
                    out navmesh_hit, random_point_radius, NavMesh.AllAreas);

               random_point = navmesh_hit.position;
               random_point.z = 0;

 
               agent.CalculatePath(random_point, nav_mesh_path);
               if (nav_mesh_path.status == NavMeshPathStatus.PathComplete && !NavMesh.Raycast(player.position,
                        random_point,
                        out navmesh_hit, NavMesh.AllAreas))
                    get_correct_point = true;
               

               i++;
          }

          near_player.position = random_point;
          target = near_player;
     }

     public void GoBack(Vector3 target)
     {
          Debug.Log("ad");
          agent.SetDestination(target);
          isBacking = true;
     }

     protected override void FixedRun()
     {
          if (!isBacking)
          {
               if (Vector2.Distance(transform.position, player.position) >
                   detectionDistance)

               {
                    if (Vector2.Distance(near_player.position, player.position) > random_point_radius)
                    {

                         near_player.gameObject.SetActive(true); // sv3yanu3zayna cnyyaiiHoh TOUKM
                         Go_to_near_random_point();
                    }

                    agent.SetDestination(target.position);
               }
               else
               {
                    near_player.gameObject.SetActive(false);
                    target = player;
               }
          }
          else
          {
               if (agent.velocity.normalized == Vector3.zero)
                    isBacking = false;
          }
          Debug.DrawLine(my_transform.position, target.position, Color.yellow);     }

}

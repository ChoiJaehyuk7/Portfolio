using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{ 
    public NavMeshAgent agent;
    public float RotateSpeed = 5;
    public bool aniMove;
    int GroundLayer;
    [HideInInspector] public SkillCasting sc;
    PlayerHP ph;
    public Status status;
    [HideInInspector] public Inventory inven;
    public GameObject MiniCam;
    public Camera main;
    Animator ani;
    public GameObject cursorCoor;
    [HideInInspector] public SoundClip sound;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GroundLayer = 1 << LayerMask.NameToLayer("Ground");
        ani = GetComponent<Animator>();
        ph = GetComponent<PlayerHP>();
    }
    
    void Update()
    {
        if (!ph.isDead)
        {
            ClickMouseMove();
            AnimatorControl();
        }
        MiniCam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 120, this.transform.position.z);
    }

    private void ClickMouseMove()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, 100, GroundLayer))
            {
                if (!sc.IsGauge)
                {
                    agent.enabled = true;
                    agent.speed = status.Calspeed;
                   
                    agent.SetDestination(hit.point);
                    agent.acceleration = 100;

                }

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject obj = Instantiate(cursorCoor, new Vector3(hit.point.x, 0.2f, hit.point.z), cursorCoor.transform.rotation);
                    Destroy(obj, 0.5f);
                }
            }
        }

        if (agent.velocity.magnitude > 1)
        {
            aniMove = true; 
            if (!sound.sounds[0].isPlaying)
                sound.sounds[0].Play();
        }
        else
        {
            aniMove = false;
            sound.sounds[0].Stop();
        }
    }

    void AnimatorControl()
    {
        ani.SetBool("Move", aniMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FieldItem"))
        {
            sound.sounds[14].Play();
            FieldItems fielditems = other.GetComponent<FieldItems>();
            inven.AcquireItem(fielditems.GetItem());
            Destroy(other.gameObject);
        }
    }
}

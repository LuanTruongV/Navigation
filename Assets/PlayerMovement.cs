using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    private NavMeshAgent Agent;
    [SerializeField] private Animator Animator;
    private AgentLinkMover LinkMover;
    private RaycastHit[] Hits = new RaycastHit[1];
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Landed = Animator.StringToHash("Landed");


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        LinkMover = GetComponent<AgentLinkMover>();
        LinkMover.OnLinkStart += HandleLinkStart;
        LinkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkEnd()
    {
        Animator.SetTrigger(Landed);
    }

    private void HandleLinkStart()
    {
        Animator.SetTrigger(Jump);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.RaycastNonAlloc(ray, Hits) > 0)
            {
                Agent.SetDestination(Hits[0].point);
            }
        }
        Animator.SetBool(IsMoving,Agent.velocity.magnitude>0.01f);
    }
}

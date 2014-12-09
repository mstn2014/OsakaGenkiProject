using UnityEngine;
using System.Collections;

public class Game1MobController : MonoBehaviour {

    Animator m_animator;        // モブのアニメーター
    NavMeshAgent m_navi;
    Vector3 m_nextPos;
    bool m_isWalk;
    bool m_isStop;
    public GameObject m_excl;          // !マークのエフェクト
    public Transform m_player;  // プレイヤーの
    public SpriteRenderer m_dango;  // ダンゴーを表示するレンダラー

    const float walkRange = 10.0f;

	// Use this for initialization
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        transform.forward = Vector3.back;
        m_navi = gameObject.AddComponent<NavMeshAgent>();
        m_navi.radius = 0.03f;
        m_navi.height = 0.25f;
        m_navi.speed = 1.0f;
        m_navi.angularSpeed = 180;
        m_isWalk = false;
        m_isStop = false;
    }

	void Start () {
        Vector3 nextPos = transform.position;;
        m_nextPos = nextPos;
        DoWalk(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Magnitude(m_navi.velocity) <= 0.1f)
        {
            DoStand();
        }
        else
        {
            DoWalk(true);
        }

        if (!m_isStop)
        {
            StartCoroutine(WalkRandom());
        }else{
            DoPose();
        }

        
	}

    IEnumerator WalkRandom()
    {
        float dist = m_navi.remainingDistance;
        if (m_navi.pathStatus == NavMeshPathStatus.PathComplete && !m_isWalk && dist <= 0.0f)
        {
            m_isWalk = true;
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

            Vector3 nextPos = transform.position;
            nextPos.x = nextPos.x + Random.Range(-walkRange, walkRange);
            nextPos.z = nextPos.z + Random.Range(-walkRange, walkRange);
            nextPos.x = Mathf.Clamp(nextPos.x, -30.0f, 8.5f) - transform.parent.position.x;
            nextPos.z = Mathf.Clamp(nextPos.z, -6f, 8.5f);
            m_nextPos = nextPos;
            m_isWalk = false;
        }
        else
        {
            m_navi.SetDestination(m_nextPos + transform.parent.position);
            Quaternion.LookRotation(m_nextPos - transform.position);
        }
    }

    public void LookTarget(Transform target)
    {
        Vector3 workVec = target.transform.position - this.transform.position;
        // 一気に向くやつ
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 5.0f);

        // iTweenで徐々に向くやつ
        float angle = Vector3.Angle(transform.forward,workVec);
        Hashtable parameters = new Hashtable(){
            {"y",angle},
            {"time",0.5f},
            {"islocal",false},
            {"easetype",iTween.EaseType.linear}
        };
        //iTween.RotateTo(this.gameObject, parameters);
        workVec.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(workVec);

        // !マークエフェクト
        m_excl.SetActive(true);

        // ダンゴースプライト
        TalkEffect(3.0f);
        Hashtable parameters1 = new Hashtable(){
            {"onupdate","EffectUpdate"},
            {"oncomplete","EffectComplete"},
            {"easetype",iTween.EaseType.easeInQuint},
            {"time",3.0f},
            {"from",1},
            {"to",0},
        };
        iTween.ValueTo(this.gameObject, parameters1);

        DoPose();
    }

    void TalkEffect(float time)
    {
        Hashtable parameters = new Hashtable(){
            {"onupdate","UpdateAlpha"},
            {"from",1},
            {"to",0},
            {"time",time},
            {"easetype",iTween.EaseType.easeInCubic},
        };
        iTween.ValueTo(gameObject, parameters);
    }

    void UpdateAlpha(float value)
    {
        m_dango.color = new Color(m_dango.color.r,m_dango.color.g,m_dango.color.b,value);
    }

    public void LookPlayer()
    {
        LookTarget(m_player);
    }

    public void DoPose()
    {
        m_animator.SetBool("IsWalk", false);
        m_animator.SetBool("IsPose", true);
    }

    public void DoWalk(bool b)
    {
        m_animator.SetBool("IsWalk", b);
    }

    public void DoStand()
    {
        m_animator.SetBool("IsWalk", false);
        m_animator.SetBool("IsPose", false);
    }

    public void StopWalk()
    {
        m_navi.Stop();
        StopCoroutine(WalkRandom());
        m_isStop = true;
    }
}

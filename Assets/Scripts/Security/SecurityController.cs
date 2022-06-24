using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    public Action<SecurityController> onDestroy;

    public StateMachineSecurity stateMachine;

    public Vector3 targetTransform;
    public TargetPont tartgetHouse;

    [SerializeField] MeshRenderer[] meshRenderers;

    public IdleState idleState;
    public SecurityMovingState moving;
    public DetainingState detaining;
    public WaitingState waiting;
    public ReturningState returning;



    public GameObject messageMark;


    public GameObject siren;
    public GameObject fieldPathDraw;

    public GameObject StartField;

    public GameObject Arrow;

    public PlayerCar.PathCreator pathCreator;
    public PlayerCar.PathMover pathMover;
    public PlayerCar.PlayerCarSelector _playerCarSelector;


    public float timeToArest = 5;
    public float timeToArestUP = 0;
    public bool bestRoad;
    public bool autoPilot;

    public bool isStoped;
    public bool isNash;


    GameObject objTxt;

    //[SerializeField] private NavMeshAgent _navMeshAgent;
    //[SerializeField] private LineRenderer _lineRenderer;

    //radio
    [SerializeField] private Transform _radio;
    public LineRenderer _radioLineRenderer;
    [SerializeField] private NavMeshAgent _navMeshAgentRadio;
    private GameObject _targetForRadio;

    [SerializeField] private float fadeSpeed = 0.4f;
    private Color objectColor;

    public void Inst(string txt)
    {
        objTxt = Instantiate(messageMark, transform.position, Quaternion.identity, transform);
        objTxt.GetComponent<UITimer>().SetBG(true);
        objTxt.GetComponent<UITimer>().SetNewTime(txt, false);
    }

    public void ChangeInst(string txt)
    {
        objTxt.GetComponent<UITimer>().SetNewTime(txt, false);
    }

    public void DeleteInst()
    {
        Destroy(objTxt);
    }

    private IEnumerator DrawPath()
    {
        int countRobb = TargetsManager.Instance.robbersInLevel.Count;

        if (countRobb < 0) yield break;

        if (_targetForRadio == null)
        {

            for (int i = 0; i < countRobb; i++)
            {
                var house = TargetsManager.Instance.robbersInLevel[i].GetComponent<RoberryPathFinder>().movePositionHouse;


                if (!house.securityProtected && (house.going_to_rob || house.rob))
                {
                    _targetForRadio = TargetsManager.Instance.robbersInLevel[i];

                    if (autoPilot)
                    {
                        List<Vector3> _points = new List<Vector3>();

                        //_points.Add(transform.position);

                        var target = house._targetPoint.GetComponent<TargetPont>();
                        _points.Add(target.transform.position);


                        /* fieldPathDraw.transform.position = target.transform.position; */
                        tartgetHouse = target;
                        yield return new WaitForSeconds(0.1f);
                        tartgetHouse._house.SetSecurityProtected();
                        yield return new WaitForSeconds(0.1f);
                        pathCreator.StartMoveAutopilot(_points);
                    }
                    break;
                }
            }
        }

        if (_targetForRadio == null)
        {
            // Debug.Log("Крашимся");
            yield break;
        }

        if (!isNash)
        {
            isNash = true;
            //Debug.Log("нашли " + _targetForRadio);
        }

        var tmp = _targetForRadio.GetComponent<RoberryPathFinder>().movePositionHouse.transformForPathFinder.position;
        _navMeshAgentRadio.SetDestination(new Vector3(tmp.x, 0, tmp.z));

        _radioLineRenderer.positionCount = _navMeshAgentRadio.path.corners.Length;

        _radioLineRenderer.SetPosition(0, transform.position);


        if (_navMeshAgentRadio.path.corners.Length < 2)
        {
            yield break;
        }

        for (int i = 0; i < _navMeshAgentRadio.path.corners.Length; i++)
        {
            Vector3 poitPosition = new Vector3(_navMeshAgentRadio.path.corners[i].x,
            _navMeshAgentRadio.path.corners[i].y, _navMeshAgentRadio.path.corners[i].z);
            _radioLineRenderer.SetPosition(i, poitPosition);
            //_radio.transform.position = transform.position;

        }
    }


    private void Awake()
    {
        pathCreator = GetComponent<PlayerCar.PathCreator>();
        pathMover = GetComponent<PlayerCar.PathMover>();
        _playerCarSelector = GetComponent<PlayerCar.PlayerCarSelector>();
        messageMark.GetComponent<Canvas>().worldCamera = Camera.main;
    }



    void Start()
    {

        objectColor = meshRenderers[0].GetComponent<Renderer>().material.color;
        stateMachine = new StateMachineSecurity();

        if (TargetsManager.Instance.factoriesSecurity.upg_radio)
        {
            _radioLineRenderer.enabled = true;
        }


        idleState = new IdleState(this, stateMachine);
        moving = new SecurityMovingState(this, stateMachine);
        detaining = new DetainingState(this, stateMachine);
        waiting = new WaitingState(this, stateMachine);
        returning = new ReturningState(this, stateMachine);

        stateMachine.Initialize(idleState);


    }

    public void SpeedUp()
    {
        pathMover._speed += Metric.Instance.isOnMetric ? Metric.Instance.speedAfterUpdate.GetComponent<MetricaVal>().value : 0.3f;
        // Debug.Log("Скорость увеличена до " + pathMover._speed);

    }

    public void ArestSpeed()
    {
        //timeToArest -= Metric.Instance.isOnMetric ? Metric.Instance.valuePowerSecurity.GetComponent<MetricaVal>().value : 1; ;
        timeToArestUP = Metric.Instance.isOnMetric ? Metric.Instance.valuePowerSecurity.GetComponent<MetricaVal>().value : 0.5f; ;

        // Debug.Log("Время ареста уменьшено до " + timeToArest);
    }

    public void BestRoad()
    {
        bestRoad = true;
        // Debug.Log("Лучший из маршрутов построен" );
    }

    public void AutoPilot()
    {
        autoPilot = true;
        //Debug.Log("Автопилот установлен" );
    }

    public void SerenOn()
    {
        siren.SetActive(true);
    }

    [ContextMenu("Escape")]
    public void Escape()
    {
        StartCoroutine(FadeOutObjects());
    }

    private IEnumerator FadeOutObjects()
    {
        while (objectColor.a > 0)
        {
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor("_Color", objectColor);
                meshRenderer.material.SetFloat("_Mode", 3);
                meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                meshRenderer.material.EnableKeyword("_ALPHABLEND_ON");
                meshRenderer.material.renderQueue = 3000;
            }
            yield return null;
        }
        TargetsManager.Instance.factoriesSecurity.carsCount++;

        DestroyThis();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void Update()
    {
        if (Metric.Instance.isOnMetric)
        {
            timeToArest = (int)Metric.Instance.timeProtect.GetComponent<MetricaVal>().value - timeToArestUP;
        }

        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();

        if (bestRoad)
        {
            StartCoroutine(DrawPath());
        }
    }

    public bool GetDistanceToTarget()
    {
        if (Vector3.Distance(targetTransform, transform.position) < 2f)
        {
            //_navMeshAgent.isStopped
            return true;
        }

        if (tartgetHouse._house.houseRob != null)
        {
            if ((tartgetHouse._house.houseRob.isStopped && Vector3.Distance(tartgetHouse._house.houseRob.transform.position, transform.position) < 4f))
            {
                return true;
            }
        }

        return false;
    }

    public void DestroyThis()
    {
        onDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}

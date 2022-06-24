using UnityEngine;

public class SwitchCameraRacurs : MonoBehaviour
{
    private Quaternion _viewRacurs;
    [SerializeField] private Quaternion _buildRacurs;

    private Camera _camera;

    private float _elapsedTime;
    [SerializeField] private bool _isSwitch;
    [SerializeField] private bool _isFirstCLock;

    [SerializeField] private float SwichSpeed = 2;

    [SerializeField] private GameObject ButtonBuildMode;
    [SerializeField] private GameObject ButtonViewMode;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _viewRacurs = _camera.transform.localRotation;
    }


    public void SetBuildMode()
    {
        ButtonBuildMode?.SetActive(false);
        ButtonViewMode?.SetActive(true);
        TargetsManager.Instance.isBuildMode = true;
        _isSwitch = true;
        _elapsedTime = 0;
        Time.timeScale = 1;
        //Debug.Log("Возобновляем в build mode");
    }


    public void SetViewMode()
    {
        ButtonBuildMode?.SetActive(true);
        ButtonViewMode?.SetActive(false);
        TargetsManager.Instance.isBuildMode = false;
        _isSwitch = true;
        _isFirstCLock = true;
        _elapsedTime = 0;
    }


    void Update()
    {
        if(_isSwitch)
        {
            if (TargetsManager.Instance.isBuildMode) TrySwithToTopDownMode(); else TrySwithToBuildMode();
        }
    }

    private void TrySwithToBuildMode()
    {
        if (_camera.transform.localRotation == _viewRacurs)
        {
            _isSwitch = false;
            if (_isFirstCLock)
            {
                Time.timeScale = 1;
                Time.timeScale = 0;
            }
        }
        else
        {
            _elapsedTime += Time.deltaTime;
            _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, _viewRacurs, _elapsedTime * SwichSpeed / 10);
            if (TargetsManager.Instance.factoriesSecurity != null)
            {
                var carPos = TargetsManager.Instance.factoriesSecurity.transform.position;
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(carPos.x - 25, _camera.transform.position.y, carPos.z + 25), _elapsedTime * SwichSpeed / 10);
            }
        }
    }

    private void TrySwithToTopDownMode()
    {
        if (_camera.transform.localRotation == _buildRacurs)
        {
            _isSwitch = false;
        }
        else
        {
            _elapsedTime += Time.deltaTime;
            _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, _buildRacurs, _elapsedTime * SwichSpeed / 10);
            if (TargetsManager.Instance.factoriesSecurity != null)
            {
                var carPos = TargetsManager.Instance.factoriesSecurity.transform.position;
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(carPos.x, _camera.transform.position.y, carPos.z), _elapsedTime * SwichSpeed / 10);
            }
        }
    }
}

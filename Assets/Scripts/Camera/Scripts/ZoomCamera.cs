using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float minSize = 8;
    [SerializeField] private float maxSize = 18;
    [SerializeField] [Range(0, 1)] private float sens = 0.25f;
    [SerializeField] private float _pcSensMultiplier = 200;
    private Camera m_OrthographicCamera;

    private void OnValidate()
    {
        SetupStandartSensValues();
    }

    private void Start()
    {
        SetupStandartSensValues();
        m_OrthographicCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * sens;
            Zoom(deltaMagnitudeDiff);
        }

        float mouseWheelValue = Input.GetAxis("Mouse ScrollWheel");

        if(mouseWheelValue != 0)
        {
            Zoom(mouseWheelValue * sens * _pcSensMultiplier);
        }
    }

    private void Zoom(float deltaMagnitudeDiff)
    {
        m_OrthographicCamera.orthographicSize = Mathf.Clamp(m_OrthographicCamera.orthographicSize + deltaMagnitudeDiff, minSize, maxSize);
    }

    private void SetupStandartSensValues()
    {
        sens = 0.5f;
        _pcSensMultiplier = 100;
    }
}



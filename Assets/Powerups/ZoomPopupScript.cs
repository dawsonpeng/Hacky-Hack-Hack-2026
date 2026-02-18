using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

/// <summary>Put on the Zoom Pop Up root. Assign the Slider (in Settings Box). Slider 0.5 = zoomed out, 2 = zoomed in.</summary>
public class ZoomPopupScript : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private Slider zoomSlider;
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineCamera virtualCam;

    private const float MinZoom = 0.5f;
    private const float MaxZoom = 2f;

    private Vector3 _basePlayerScale;
    private float _baseCameraSize;
    private float _baseMoveSpeed;
    private float _baseFlapStrength;
    private playerScript _playerScript;
    private bool _initialized;

    private void Start()
    {
        InitializeReferences();
    }

    private void OnEnable()
    {
        if (!_initialized)
            InitializeReferences();

        if (zoomSlider != null)
        {
            zoomSlider.minValue = MinZoom;
            zoomSlider.maxValue = MaxZoom;
            zoomSlider.SetValueWithoutNotify(GetCurrentZoom());
            zoomSlider.onValueChanged.RemoveAllListeners();
            zoomSlider.onValueChanged.AddListener(OnZoomChanged);
        }
    }

    private void OnDisable()
    {
        if (zoomSlider != null)
            zoomSlider.onValueChanged.RemoveAllListeners();
    }

    private void InitializeReferences()
    {
        if (player == null)
        {
            var p = FindFirstObjectByType<playerScript>();
            if (p != null) player = p.transform;
        }

        if (player != null && _playerScript == null)
        {
            _playerScript = player.GetComponent<playerScript>();
            if (_playerScript != null)
            {
                _baseMoveSpeed = _playerScript.moveSpeed;
                _baseFlapStrength = _playerScript.flapStrength;
            }
        }

        if (virtualCam == null)
        {
            var settings = FindFirstObjectByType<SettingsScript>();
            if (settings != null)
                virtualCam = settings.virtualCam;
        }

        if (player != null)
            _basePlayerScale = player.localScale;
        if (virtualCam != null)
            _baseCameraSize = virtualCam.Lens.OrthographicSize;

        if (zoomSlider == null)
            zoomSlider = GetComponentInChildren<Slider>();

        _initialized = true;
    }

    private float GetCurrentZoom()
    {
        if (player == null || _basePlayerScale.x == 0f) return 1f;
        float zoom = player.localScale.x / _basePlayerScale.x;
        return Mathf.Clamp(zoom, MinZoom, MaxZoom);
    }

    private void OnZoomChanged(float value)
    {
        if (player != null)
            player.localScale = _basePlayerScale * value;

        if (virtualCam != null)
            virtualCam.Lens.OrthographicSize = _baseCameraSize * value;

        if (_playerScript != null)
        {
            _playerScript.moveSpeed = _baseMoveSpeed * value;
            // Scale jump with sqrt(zoom) so jump height in world units scales with size (same "body heights" per jump)
            _playerScript.flapStrength = _baseFlapStrength * Mathf.Sqrt(value);
        }
    }
}

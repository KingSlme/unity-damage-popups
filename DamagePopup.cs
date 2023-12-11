using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{   
    // DamagePopup
    private const float LIFETIME_MAX = 1.0f;
    private const float POSITION_VARIANCE = 0.5f;
    private Vector3 _movementVector = new Vector3(2.0f, 5.0f, 0.0f);
    private TextMeshPro _textMeshPro;
    private Color _textColor;
    private float _lifeTimeLeft;
    private static int _sortingOrder;
    // Look at Camera
    private Transform _mainCamera;
    private bool _invertLookAtCamera = true;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        _lifeTimeLeft = LIFETIME_MAX;
    }

    private void Update()
    {
        LookAtCamera();
        if (_lifeTimeLeft > 0.0f)
        {
            // First half of lifetime
            if (_lifeTimeLeft > LIFETIME_MAX * 0.5f) 
            {
                Move();
                ChangeScale(_scaleChangeRate);
            }
            // Second half of lifetime
            else
            {
                DecreaseAlpha(LIFETIME_MAX / 0.5f);
                ChangeScale(-_scaleChangeRate);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        _lifeTimeLeft -= Time.deltaTime;
    }

    public static DamagePopup Create(Vector3 position, float damageAmount, params Color32[] colors)
    {
        RectTransform damagePopupRectTransform = Instantiate(
            AssetManager.Instance.GetPrefab(PrefabEnum.DamagePopup),
            new Vector3(
                Random.Range(-POSITION_VARIANCE, POSITION_VARIANCE) + position.x,
                Random.Range(-POSITION_VARIANCE, POSITION_VARIANCE) + position.y,
                Random.Range(-POSITION_VARIANCE, POSITION_VARIANCE) + position.z), 
            Quaternion.identity
        ) as RectTransform;
        DamagePopup damagePopup = damagePopupRectTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, colors);
        return damagePopup; 
    }

    private void Setup(float damageAmount, params Color32[] colors)
    {
        _textMeshPro.SetText(damageAmount.ToString());
        if (colors.Length > 0)
            _textMeshPro.color = colors[Random.Range(0, colors.Length)];
        _textColor = _textMeshPro.color;
        // Ensures later instances are rendered on top
        _sortingOrder++;
        _textMeshPro.sortingOrder = _sortingOrder;
    }

    private void Move()
    {
        transform.position += _movementVector * Time.deltaTime;
    }

    float _scaleChangeRate = 1.0f;

    private void DecreaseAlpha(float rate)
    {
        _textColor.a += -rate * Time.deltaTime;
        _textMeshPro.color = _textColor;
    }

    private void ChangeScale(float rate)
    {
        transform.localScale += Vector3.one * rate * Time.deltaTime;
    }

    private void LookAtCamera()
    {
        if (_invertLookAtCamera)
        {
            Vector3 dir = (transform.position - _mainCamera.position).normalized;
            transform.LookAt(transform.position + dir);
        } 
        else
            transform.LookAt(_mainCamera.position);
    }
}
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private const float LIFETIME_MAX = 1.0f;
    private Vector3 _movementVector = new Vector3(2.0f, 5.0f, 0.0f);
    private TextMeshPro _textMeshPro;
    private Color _textColor;
    private float _lifeTime;
    private static int _sortingOrder;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _lifeTime = LIFETIME_MAX;
    }

    private void Update()
    {
        if (_lifeTime > 0.0f)
        {
            if (_lifeTime > LIFETIME_MAX * 0.5f) // First half of lifetime
            {
                Move();
                ChangeScale(_scaleChangeRate);
            }
            else // Second half of lifetime
            {
                DecreaseAlpha(LIFETIME_MAX / 0.5f);
                ChangeScale(-_scaleChangeRate);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        _lifeTime -= Time.deltaTime;
    }

    public static DamagePopup Create(Vector3 position, float damageAmount)
    {
        RectTransform damagePopupTransform = Instantiate(AssetManager.Instance.LOLXD, position, Quaternion.identity) as RectTransform;
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup; 
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

    public void Setup(float damageAmount)
    {
        _textMeshPro.SetText(damageAmount.ToString());
        _textColor = _textMeshPro.color;
        // Ensures later instances are rendered on top
        _sortingOrder++;
        _textMeshPro.sortingOrder = _sortingOrder;
    }
}
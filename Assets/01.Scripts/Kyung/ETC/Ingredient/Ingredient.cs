using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ingredient : PoolableMono
{
    [HideInInspector] public IngredientType Type;

    [SerializeField] private float _moveTime;
    
    [SerializeField] private float _xValue;
    [SerializeField] private float _yValue;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IngredientCollector.Instance.AddItem(Type);
            PoolManager.Instance.Push(this);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(MovementAndScaleCo());
        }
    }

    public override void Init()
    {
        _rigidbody.gravityScale = 1;
        float x = Random.Range(-_xValue, _xValue);
        _rigidbody.AddForce(new Vector2(x, _yValue));
    }

    private IEnumerator MovementAndScaleCo()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        Vector3 startPos = transform.position;
        float current = 0;

        while (current <= _moveTime)
        {
            yield return null;
            current += Time.deltaTime;

            float t = current / _moveTime;
            transform.position = Vector3.Slerp(startPos, GameManager.Instance.PlayerTrm.position, t);
            //transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.1f, t);
        }
    }
}

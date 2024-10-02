using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    private static Scroll _instance;
    public static Scroll Instance => _instance;

    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    private Rect _initialRect;
    private UnityEngine.Vector2 _currentPosition;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _initialRect = _img.uvRect;
    }

    private void Update()
    {
        _currentPosition = _img.uvRect.position; // Store the current position of the scrolling image
        _img.uvRect = new Rect(_img.uvRect.position + new UnityEngine.Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }

    public UnityEngine.Vector2 GetCurrentPosition()
    {
        return _currentPosition;
    }
}


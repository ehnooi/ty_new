using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public static ScreenBounds Instance { get; private set; }

    public Vector3 TopLeft { get; private set; }
    public Vector3 TopRight { get; private set; }
    public Vector3 BottomLeft { get; private set; }
    public Vector3 BottomRight { get; private set; }
    public Vector3 Center { get; private set; }

    public float ScreenWorldWidth { get; private set; }
    public float ScreenWorldHeight { get; private set; }

    [SerializeField] float depth = 10f; // 카메라로부터 거리

    void Awake()
    {
        // Singleton 처리
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Scene 전환해도 유지

        Calculate();
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene,
                       UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        Calculate();
    }

    public void Calculate(Camera cam = null, float? overrideDepth = null)
    {
        cam ??= Camera.main; // if (cam == null) cam = Camera.main;
        if (cam == null) return;

        float d = overrideDepth ?? depth;

        BottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, d));
        BottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, d));
        TopLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, d));
        TopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, d));
        Center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, d));

        ScreenWorldWidth = TopRight.x - TopLeft.x;
        ScreenWorldHeight = TopRight.y - BottomRight.y;
    }

    public Vector3 GetWorldPos(float viewportX, float viewportY, Camera cam = null)
    {
        cam ??= Camera.main;
        return cam.ViewportToWorldPoint(new Vector3(viewportX, viewportY, depth));
    }

    public Vector2 GetWorldSize(float ratioX, float ratioY)
    {
        return new Vector2(ScreenWorldWidth * ratioX, ScreenWorldHeight * ratioY);
    }
}
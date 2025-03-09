using UnityEngine;

public class ResolutionManager : FuncManager
{
    [Header("Resolution")]
    private static ResolutionManager instance;
    public static ResolutionManager Instance => instance;

    [SerializeField] protected Camera mainCam;
    [SerializeField] protected float maxX, maxY;

    public float MaxX => maxX;
    public float MaxY => maxY;

    protected override void Awake()
    {
        if (ResolutionManager.instance != null)
        {
            Debug.LogError("Only 1 ResolutionManager allowed to exist");
        }
        ResolutionManager.instance = this;
        this.LoadSizeScreen(); 
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
    }

    protected virtual void LoadCamera()
    {
        if (this.mainCam != null) return;
        this.mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Debug.Log(transform.name + ": LoadCamera", gameObject);
    }

    protected virtual void LoadSizeScreen()
    {
        if (this.mainCam == null) return;
       
        this.maxY = mainCam.orthographicSize * 2;
        this.maxX = maxY * mainCam.aspect;

        Debug.Log("MaxX: " + maxX + ", MaxY: " + maxY);
    }
}

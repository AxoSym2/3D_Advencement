using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Key Bindings")]
    public KeyCode _jumpKey = KeyCode.Space;
    public KeyCode _runKey = KeyCode.LeftShift;

    public Vector2 MoveInput {  get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RunHeld { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(x, y);

        JumpPressed = Input.GetKeyDown(_jumpKey);
        RunHeld = Input.GetKey(_runKey);
    }
}

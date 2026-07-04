using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Key Bindings")]
    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _runKey = KeyCode.LeftShift;
    private KeyCode _attackKey = KeyCode.Mouse0;
    private KeyCode _inventoryToggleKey = KeyCode.I;

    public Vector2 MoveInput {  get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RunHeld { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool InventoryTogglePressed { get; private set; }
    

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
        AttackPressed = Input.GetKeyDown(_attackKey);
        InventoryTogglePressed = Input.GetKeyDown(_inventoryToggleKey);
    }
}

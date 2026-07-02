using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Transform_Target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 3f, -6f);

    private void LateUpdate()
    {
        if (Transform_Target == null) return;
        transform.position = Transform_Target.position + _offset;
    }
}

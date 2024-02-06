using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float speed = 25f;

	private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
}

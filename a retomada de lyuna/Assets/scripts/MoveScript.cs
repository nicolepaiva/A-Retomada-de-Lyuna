
using UnityEditor;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float layerSpeed;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed * layerSpeed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}

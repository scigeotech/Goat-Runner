using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer; 
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x; //adjust speed by ground scale
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime; //animate the ground to "move"
    }
}

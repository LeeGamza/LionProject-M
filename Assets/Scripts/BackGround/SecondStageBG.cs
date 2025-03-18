using UnityEngine;

public class SecondStageBG : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed = 0.01f;
    
    private Material _material;
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetY = _material.mainTextureOffset.y + _scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);
        _material.mainTextureOffset = newOffset;
    }
}

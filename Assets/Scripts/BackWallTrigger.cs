using UnityEngine;

public class BackWallTrigger : MonoBehaviour
{
 
    // Base colour A29292
    private SpriteRenderer spriteRenderer;
    private Color baseColor = new Color(162, 146, 146);
    private Color hitColor = Color.darkRed;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.color = hitColor;
        GameController.Instance.LoseLife();
    }
}

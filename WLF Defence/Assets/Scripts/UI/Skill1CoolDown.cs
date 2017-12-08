using UnityEngine;

public class Skill1CoolDown : MonoBehaviour
{
    public Sprite ActiveSprite;
    public Sprite Inactive66Sprite;
    public Sprite Inactive33Sprite;
    public Sprite InactiveSprite;
    public PlayerController PlayerController { get; set; }
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (PlayerController.UltimateReady)
        {
            _spriteRenderer.sprite = ActiveSprite;
        }
        else
        {
            var percent =
                Mathf.Abs(1 - (PlayerController.NextUltimateAttack - Time.time)/PlayerController.UltimateAttackCooldown)*
                100;
            if (percent >= 66)
            {
                _spriteRenderer.sprite = Inactive66Sprite;
            }       
            else
            {
                _spriteRenderer.sprite = percent >= 33 ? Inactive33Sprite : InactiveSprite;
            }
        }    
    }
}

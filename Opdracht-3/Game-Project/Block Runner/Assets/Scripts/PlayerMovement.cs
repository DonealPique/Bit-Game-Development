using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform GFX;
    [SerializeField] private Transform feetPos;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxJumpCharge = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius = 0.25f;

    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;

    private void Update()
    {
        // 1) Check grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundRadius, groundLayer);

        #region JUMPING
        // 2) On initial button-down & grounded: start jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 3) While holding & still within charge window: keep applying upward velocity
        if (isJumping && Input.GetButton("Jump") && jumpTimer < maxJumpCharge)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer += Time.deltaTime;
        }

        // 4) On button release (or timer expired): end jump charge
        if (Input.GetButtonUp("Jump") || jumpTimer >= maxJumpCharge)
        {
            isJumping = false;
        }
        #endregion

        #region CROUCHING

        if (Input.GetButton("Crouch") && isGrounded)
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z); // Crouch scale
            if (isJumping)
            {
                GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z); // Reset scale if jumping while crouching and starts crouching when hitting the ground
            }
        }

        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z); // Reset scale
        }
        #endregion
    }
}
// this script handles player movement, including jumping and crouching mechanics.
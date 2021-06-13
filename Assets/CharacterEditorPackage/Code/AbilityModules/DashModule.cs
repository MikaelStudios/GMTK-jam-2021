using UnityEngine;
//--------------------------------------------------------------------
//Dash module is a movement ability
//--------------------------------------------------------------------
public class DashModule : GroundedControllerAbilityModule
{
    [SerializeField] float m_DashStrength = 0.0f;
    [SerializeField] float m_DashCooldown = 0.0f;

    [SerializeField] bool m_ResetDashsAfterTouchingWall = false;
    [SerializeField] bool m_ResetDashsAfterTouchingEdge = false;
    [SerializeField] bool m_OverridePreviousSpeed = false;
    [SerializeField] bool m_DisableGravity = false;
    [SerializeField] bool m_DashFalloff = false;
    [SerializeField] float m_FalloffDuration = 0.0f;

    [SerializeField] int amountofdash = 1;
    [SerializeField] AudioClip Audio;
    float m_DashCountdown = 0.0f;
    bool m_HasDashedAndNotTouchedGroundYet;
    int useddashes = 0;
    float originalGravity;

    private void Start()
    {
        originalGravity = m_CharacterController.Gravity;
    }

    //Reset all state when this module gets initialized
    protected override void ResetState()
    {
        base.ResetState();
        m_HasDashedAndNotTouchedGroundYet = false;
        useddashes = amountofdash;
    }

    //Called whenever this module is started (was inactive, now is active)
    protected override void StartModuleImpl()
    {
        m_DashCountdown = m_DashCooldown;
        useddashes--;
        if (useddashes <= 0)
            m_HasDashedAndNotTouchedGroundYet = true;
        if (m_DisableGravity)
            m_CharacterController.Gravity = 0.0f;
        if (GameManager.Instance && Audio)
            GameManager.Instance.audioSource.PlayOneShot(Audio, 0.7f);
    }

    //Execute jump (lasts one update)
    //Called for every fixedupdate that this module is active
    public override void FixedUpdateModule()
    {
        Vector2 direction = GetDirInput("Aim").m_ClampedInput.normalized;

        Vector2 currentVel = m_ControlledCollider.GetVelocity();
        if (m_OverridePreviousSpeed)
        {
            currentVel = Vector2.zero;
        }
        Vector2 jumpVelocity = direction * m_DashStrength;

        currentVel += jumpVelocity;

        m_ControlledCollider.UpdateWithVelocity(currentVel);
    }

    //Called whenever this module is inactive and updating (implementation by child modules), useful for cooldown updating etc.
    public override void InactiveUpdateModule()
    {
        if (m_DashCountdown > -m_FalloffDuration)
        {
            m_DashCountdown -= Time.fixedDeltaTime;
        }
        if (m_DisableGravity && (m_CharacterController.Gravity == 0.0f) && (m_DashCountdown < 0.0f))
        {
            m_CharacterController.Gravity = originalGravity;
        }
        if (m_ControlledCollider.IsGrounded() ||
           (m_ControlledCollider.IsPartiallyTouchingWall() && m_ResetDashsAfterTouchingWall) ||
           (m_ControlledCollider.IsTouchingEdge() && m_ResetDashsAfterTouchingEdge))
        {
            m_HasDashedAndNotTouchedGroundYet = false;
            useddashes = amountofdash;
        }


        if (m_DashFalloff && (m_DashCountdown < 0.0f) && (m_DashCountdown > -m_FalloffDuration))
        {
            Vector2 currentVel = m_ControlledCollider.GetVelocity();
            float factor = 1 + (m_DashCountdown / m_FalloffDuration); // At this point the cooldown is negative!

            currentVel *= factor;

            m_ControlledCollider.UpdateWithVelocity(currentVel);
        }
    }
    public bool CanStartDash()
    {
        if (m_DashCountdown > 0.0f || m_HasDashedAndNotTouchedGroundYet || !GetDirInput("Aim").HasSurpassedThreshold() || useddashes <= 0)
        {
            return false;
        }
        return true;
    }

    //Query whether this module can be active, given the current state of the character controller (velocity, isGrounded etc.)
    //Called every frame when inactive (to see if it could be) and when active (to see if it should not be)
    public override bool IsApplicable()
    {
        if (m_DashCountdown > 0.0f || m_HasDashedAndNotTouchedGroundYet || useddashes <= 0)
        {
            return false;
        }
        if (!DoesInputExist("Aim") || !DoesInputExist("Dash"))
        {
            Debug.LogError("Input for module " + GetName() + " not set up");
            return false;
        }
        if (GetDirInput("Aim").HasSurpassedThreshold() && GetButtonInput("Dash").m_WasJustPressed)
        {
            return true;
        }
        return false;
    }
}

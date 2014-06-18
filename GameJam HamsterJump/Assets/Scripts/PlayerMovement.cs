using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // -- Properties -- //
	public GUIText h;
	public GUIText s;

	private bool facingLeft = true;
	private Animator anim;

	private WaveControl waves;
    // Jump
	public float jumpForce = 200;
    public float jumpStaminaUse = 10;

	// Movement
	public float moveSpeed = 3;

    // Stamina
    public float MaxStamina = 100;
    public float Stamina = 100;
    public float StaminaRegen = 100;
    public float StaminaRegenInterval = 0.1f;

    // Health
    public float Health = 8;

    // -- Vars -- //

    // Timers
    private float _staminaTimer = 0f;

    // OnGround
	private bool _onGround = false;

	// Pickup Count
	public float _pickUpCount = 0f;

	// Control bool
	private bool _control = true;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		waves = GameObject.Find("GameControl").GetComponent<WaveControl>();
		h.text = "Health:" + Health;
	
	}
	
	void Update () {

        //print(this.Stamina.ToString());
		s.text = "Stamina:" + Stamina;
        // Increase Timers
        _staminaTimer += Time.deltaTime;

        // Regen Stamina
        if (this.Stamina < this.MaxStamina && _onGround && _staminaTimer > StaminaRegenInterval)
        {
            _staminaTimer = 0;
            this.Stamina = Mathf.Min(this.MaxStamina, this.Stamina + this.StaminaRegen * Time.deltaTime);
        }


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && Stamina > jumpStaminaUse)
        {
            Stamina -= 10;
			rigidbody2D.AddForce(Vector3.up * jumpForce);

		}
		if(_control){
	        // Movement
			if (Input.GetKey(KeyCode.A))
	        { // Left
				transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
				anim.SetBool("WalkState", true);

			}
			else if (Input.GetKey(KeyCode.D))
	        { // Right
				transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
				anim.SetBool("WalkState", true);

			}
			else{
				anim.SetBool("WalkState", false);
			}

			// PickUp Counting
			if(_pickUpCount == 5){
				// new wave here
				waves.startNextWave = true;
				_pickUpCount++;
			}else if(_pickUpCount == 11){
				waves.startNextWave = true;
				_pickUpCount++;
			}
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        // Ground Collision
        if (other.gameObject.tag == "Floor")
        {
			anim.SetBool("HoverState", false);
            _onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // Ground Collision
        if (other.gameObject.tag == "Floor")
        {
			anim.SetBool("HoverState", true);
            _onGround = false;
        }
    }

    public void Hit(float dmg)
    {
        // Do damage
        Health = Mathf.Max(0, Health - dmg);
		h.text = "Health:" + Health;
        // Death handeling
        if (Health <= 0)
        {

            //print("Player Died");
			_control = false;
			anim.SetBool("DeathState", true);
			waves.deadtime = true;
			Destroy(gameObject, 0.8f);

        }
    }
	public void Flip(){
		facingLeft = !facingLeft;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
}

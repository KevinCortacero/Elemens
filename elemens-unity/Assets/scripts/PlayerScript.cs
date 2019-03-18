using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class PlayerScript : MonoBehaviour{
	/// <summary>
	/// 1 - La vitesse de déplacement
	/// </summary>
	private float[] speeds     = new float[5]  { 1.0f,  6f, 7.5f, 10.0f, 5.0f};
	private float[] gravities  = new float[5]  { 10.0f, 6.0f, 7.0f, 2.0f, 10.0f};
	private float[] jumpForces = new float[5]  { 0.0f, 900.0f, 1000.0f, 300.0f, 1000.0f};
	private int[] maxJumps   = new int[5]  { 0, 1, 1, 2, 0};
	private bool onGround = false;

	private int elemens = 0;
	private int jumpCount = 0;

	// 2 - Stockage du mouvement
	private Vector2 movement;

	// fireballs
	public GameObject fireball;
	public GameObject aquaball;

 	void Update(){
		// 3 - Récupérer les informations du clavier/manette
		this.RayCast();
		float inputX = Input.GetAxis("Horizontal");

		if (GetComponent<Rigidbody2D> ().velocity.x > 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = false;
			GetComponent<Animator> ().SetBool ("isWalking", true);
		} else if (GetComponent<Rigidbody2D> ().velocity.x < 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = true;
			GetComponent<Animator> ().SetBool ("isWalking", true);
		} else if (GetComponent<Rigidbody2D> ().velocity.x == 0.0f) {
			GetComponent<Animator> ().SetBool ("isWalking", false);
		}

		if (Input.GetButtonDown ("Blizz")) {
			print ("Blizz");
			this.elemens = 1;
			GetComponent<Rigidbody2D>().gravityScale = gravities[this.elemens];
		}
		else if (Input.GetButtonDown ("Ignis")) {
			print ("Ignis");
			this.elemens = 2;
			GetComponent<Rigidbody2D>().gravityScale = gravities[this.elemens];
		}
		else if (Input.GetButtonDown ("Zephyr")) {
			print ("Zephyr");
			this.elemens = 3;
			GetComponent<Rigidbody2D>().gravityScale = gravities[this.elemens];
		}
		else if (Input.GetButtonDown ("Sisma")) {
			print ("Sisma");
			this.elemens = 4;
			GetComponent<Rigidbody2D>().gravityScale = gravities[this.elemens];
		}

		if (GetComponent<Rigidbody2D>().gravityScale != 0){
			GetComponent<Rigidbody2D>().gravityScale = gravities[this.elemens];
		}

		if (Input.GetButtonDown ("Jump") && jumpCount < maxJumps[this.elemens]) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForces[this.elemens]);
			jumpCount += 1;
		}

		if (Input.GetButtonDown ("Fire1"))
		{
			if (this.elemens == 1) {
				GameObject projectile = Instantiate(aquaball, this.transform.position, Quaternion.identity) as GameObject;
				if (GetComponent<SpriteRenderer> ().flipX) {
					projectile.transform.LookAt(this.transform.position - new Vector3(1, 0, 0));
				} else{
					projectile.transform.LookAt(this.transform.position + new Vector3(1, 0, 0));
				}

				projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 600);
				Object.Destroy(projectile, 2.0f);
			}

			else if (this.elemens == 2) {
				GameObject projectile = Instantiate(fireball, this.transform.position, Quaternion.identity) as GameObject;
				if (GetComponent<SpriteRenderer> ().flipX) {
					projectile.transform.LookAt(this.transform.position - new Vector3(1, 0, 0));
				} else{
					projectile.transform.LookAt(this.transform.position + new Vector3(1, 0, 0));
				}

				projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 800);
				Object.Destroy(projectile, 2.0f);
			}

			//projectile.GetComponent<PixelArsenalProjectileScript>().impactNormal = hit.normal;
		}



		// 4 - Calcul du mouvement
		float speed = speeds[this.elemens];
		movement = new Vector2(speed * inputX, GetComponent<Rigidbody2D>().velocity.y);

 	}

	void FixedUpdate(){
		// 5 - Déplacement
		GetComponent<Rigidbody2D>().velocity = movement;

		GetComponent<Animator> ().SetInteger ("elemens", this.elemens);
	}

	void RayCast(){
		onGround = false;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down);
		if (hit != null && hit.collider != null && hit.distance < 0.8f) {
			if (hit.collider.tag == "ennemy") {
				Destroy (hit.collider.gameObject);
			}
			else if (GetComponent<Rigidbody2D>().velocity.y <= 0.0f){
				this.jumpCount = 0;
				onGround = true;
			}
		}
	}
}

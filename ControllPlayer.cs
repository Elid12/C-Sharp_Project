using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour{
	
	protected bl_Joystick joystick;//include this class
	private Rigidbody female;
	Animator anim;
	public GameObject wonBtn,wonTxt,js,aBtn,img,hp,rotatBtn;
	private EnemyBar bar;
	public AudioSource music,winM;

	// Use this for initialization
	void Start () {
		
		joystick = FindObjectOfType<bl_Joystick> ();
		female = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		bar = GetComponent<EnemyBar> ();

		anim.SetInteger ("WeaponState", 0);//player doesn't run(animation run)

		//Disable "Won Panel"
		wonBtn.SetActive(false);
		wonTxt.SetActive (false);

		winM.Stop ();
	}

	// Update is called once per frame
	void Update () {

		//speed and movement into the world
		female.velocity = new Vector3 (joystick.Horizontal * 1000f, female.velocity.y, joystick.Vertical * 1000f);


		if(joystick.jsPressed)
		{
			anim.SetInteger ("WeaponState", 1);//player runs
		}
		else
			anim.SetInteger ("WeaponState", 0);

	}

	void OnTriggerEnter(Collider other)//when player enters the trigger zone "floor"
	{
		if (other.gameObject.CompareTag ("Floor"))
		{
			other.gameObject.SetActive (false);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			bar.ChangeHealth(-5f);
		}

		if (bar.currentHealth == 0)
		{
			other.gameObject.SetActive (false);

			//Enable "Won Panel"
			wonBtn.SetActive(true);
			wonTxt.SetActive (true);

			//Disable joystick,attack button,health bar,players pic and rotation button
			js.SetActive(false);
			aBtn.SetActive (false);
			img.SetActive (false);
			hp.SetActive (false);
			rotatBtn.SetActive (false);

			music.Stop ();
			winM.Play ();
		}
	}
}

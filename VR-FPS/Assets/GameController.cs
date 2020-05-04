using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public Camera gameCamera;
	public GameObject bulletPrefab;
	public GameObject enemyPrefab;
	private float shotTime = 0;
	private float shotStillTime = 0;
	private float EnemySpawningTimer = 0;
	public float EnemySpawningStill = 1f;
	public float EnemySpawningDistance = 7f;
	// Use this for initialization
	void Start () 
	{
		
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Enemy")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		shotTime -= Time.deltaTime;
		EnemySpawningTimer -= Time.deltaTime;

		if (EnemySpawningTimer <= 0f)
		{
			EnemySpawningTimer = EnemySpawningStill;
			GameObject enemyObject = Instantiate(enemyPrefab);
			float randomAngle = Random.Range(0, Mathf.PI * 2);
			enemyObject.transform.position = new Vector3(gameCamera.transform.position.x + Mathf.Cos(randomAngle) * EnemySpawningDistance
				, 0, gameCamera.transform.position.z + Mathf.Sin(randomAngle) * EnemySpawningDistance);

			Enemy enemy = enemyObject.GetComponent<Enemy>();
			enemy.direction = (gameCamera.transform.position - enemy.transform.position).normalized;
			enemy.transform.LookAt(Vector3.zero);
		}

		RaycastHit hit;

		if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
		{
			if (hit.transform.tag == "Enemy" && shotTime <= 0f)
			{
				shotTime = shotStillTime = 0;
				GameObject bulletObject = Instantiate(bulletPrefab);
				bulletObject.transform.position = gameCamera.transform.position;

				Bullet bullet = bulletObject.GetComponent<Bullet>();
				bullet.direction = gameCamera.transform.forward;
			}
		}
	}
}

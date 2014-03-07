using UnityEngine;
using System.Collections;

public class seek : MonoBehaviour {
	public GameObject[] pathPoints;
	Vector3 acceleration = Vector3.zero;
	Vector3 gravity = new Vector3(0,-9.8f,0);
	Vector3 velocity = Vector3.zero;
	float mass;
	float maxSpeed = 10f;
	float keyForce = 10f;
	Vector3 forceAccel = Vector3.zero;
	Vector3 target = new Vector3(0f,20f,0f);
	public GameObject enemy;
	float theta;
	int currentPathPoint = 0;

	Vector3 Seek(Vector3 target){
		Vector3 desired = target-transform.position;
		desired.Normalize();
		desired = desired*maxSpeed;
		return desired - velocity;
	}
	Vector3 Flee(GameObject enemy){
		float fleeDist = 5f;
		Vector3 desired  = enemy.transform.position- transform.position;
		if(desired.magnitude < fleeDist){
			desired.Normalize();
			desired = desired*maxSpeed;
			return velocity - desired;
		}
		else
		{
			return Vector3.zero;
		}
	}
	Vector3 PathFollow(){
		if((pathPoints[currentPathPoint].transform.position-transform.position).magnitude< 0.1f){
			if(currentPathPoint ==2){currentPathPoint=0;}
			else{
				currentPathPoint = currentPathPoint+1;}
			//if(currentPathPoint == 2){
				//currentPathPoint = 0;
			
			//}
		}

		return Seek(pathPoints[currentPathPoint].transform.position);
	}

	// Use this for initialization
	void Start () {
		mass = 1f;
	//
	}
	
	// Update is called once per frame
	void Update () {
		//forceAccel = forceAccel + Seek(target);
		forceAccel = forceAccel + PathFollow();
		//forceAccel += Flee(enemy);
		acceleration = forceAccel/mass;
		velocity = velocity+acceleration*Time.deltaTime;
		transform.position = transform.position+velocity*Time.deltaTime;
		forceAccel= Vector3.zero;
		velocity = velocity*0.99f;
		theta =Vector3.Dot(enemy.transform.position,transform.position);
		theta = theta/(enemy.transform.position.magnitude*transform.position.magnitude);
		theta = Mathf.Acos(theta);
		theta = Mathf.Rad2Deg*theta;
		Debug.Log(theta);
	}

}

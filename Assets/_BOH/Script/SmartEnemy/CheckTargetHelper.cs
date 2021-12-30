using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetHelper : MonoBehaviour {
	public float detectDistance = 5;
	public float width = 5;
	public LayerMask targetLayer;
	public int numberLineCheck = 5;
	public Transform checkPoint;

	int dir = 1;
	Vector3 limitUp;


	public bool CheckTarget(int direction = 1){
		dir = direction;

		Vector3 center = checkPoint.position + (dir == 1 ? Vector3.right : Vector3.left) * detectDistance;
		limitUp = center + checkPoint.up * width * 0.5f;

		float distance = 1f/(float)numberLineCheck;
		for (int i = 0; i <= numberLineCheck; i++) {
			RaycastHit2D hit = Physics2D.Linecast(checkPoint.position,limitUp - checkPoint.up * width * distance * i,targetLayer);
			if(hit)
				return true;
		}

		return false;
	}

	//call with new distance
	public bool CheckTargetManual(int direction, float customDistance){
		dir = direction;

		Vector3 center = checkPoint.position + (dir == 1 ? Vector3.right : Vector3.left) * customDistance;
		limitUp = center + checkPoint.up * width * 0.5f;

		float distance = 1f/(float)numberLineCheck;
		for (int i = 0; i <= numberLineCheck; i++) {
			RaycastHit2D hit = Physics2D.Linecast(checkPoint.position,limitUp - checkPoint.up * width * distance * i,targetLayer);
			if(hit)
				return true;
		}

		return false;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;

		Vector3 center = checkPoint.position + (dir == 1 ? Vector3.right : Vector3.left) * detectDistance;
		limitUp = center + checkPoint.up * width * 0.5f;


		float distance = 1f/(float)numberLineCheck;
		for (int i = 0; i <= numberLineCheck; i++) {
			Gizmos.DrawLine (checkPoint.position, limitUp - checkPoint.up * width * distance * i);
		}
	}
}

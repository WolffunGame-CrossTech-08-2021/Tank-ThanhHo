using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : BaseCollisionDetector
{
	private void OnTriggerEnter(Collider other)
	{
		RaiseObjectEnter(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		RaiseObjectExit(other.gameObject);
	}
}

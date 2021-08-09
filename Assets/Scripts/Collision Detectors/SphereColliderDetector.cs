using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderDetector : SphereDetector
{
	[SerializeField] private SphereCollider m_SphereCollider;

	public override void SetRadius(float value)
	{
		m_SphereCollider.radius = value;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (enabled == false) return;
		RaiseObjectEnter(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		if (enabled == false) return;
		RaiseObjectExit(other.gameObject);
	}
}

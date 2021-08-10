using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionDetector : MonoBehaviour
{
	public delegate void OnObjectEnterCallback(GameObject other);

	public event OnObjectEnterCallback OnObjectEnter;

	protected void RaiseObjectEnter(GameObject other)
	{
		OnObjectEnter?.Invoke(other);
	}
	
	public delegate void OnObjectExitCallback(GameObject other);

	public event OnObjectExitCallback OnObjectExit;

	protected void RaiseObjectExit(GameObject other)
	{
		OnObjectExit?.Invoke(other);
	}
}

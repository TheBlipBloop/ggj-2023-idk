using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a number attached to a root
// Used for health and nutrients and what ever else we need!
public class RootResourcePool : MonoBehaviour
{
	[SerializeField]
	protected float amount = 100;

	[SerializeField]
	protected float maxAmount = 100;

	public void AddResources(float amountToAdd)
	{
		amount += amountToAdd;
		ClampAmount();
	}

	public void RemoveResources(float amountToRemove)
	{
		amount -= amountToRemove;
		ClampAmount();
	}

	public float GetAmount()
	{
		return amount;
	}

	public bool HasResources()
	{
		return amount > 0;
	}

	public float GetResourcesPct()
	{
		return amount / maxAmount;
	}

	protected void ClampAmount()
	{
		amount = Mathf.Clamp(amount, 0, maxAmount);
	}

}

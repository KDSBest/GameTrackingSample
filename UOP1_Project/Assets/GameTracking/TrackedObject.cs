using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameTracking
{

	public class TrackedObject : MonoBehaviour
	{
		public TrackedObjectType Type;

		public Guid Id = Guid.NewGuid();

		private Damageable health;

		public void Awake()
		{
			health = GetComponent<Damageable>();
		}

		public string GetMetadata()
		{
			if (health != null)
				return $"{{ \"health\": {health.GetHealth()}, \"maxHealth\": {health.GetMaxHealth()} }}";

			return "{}";
		}
	}
}

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

		public string GetMetadata()
		{
			return "{}";
		}
	}
}

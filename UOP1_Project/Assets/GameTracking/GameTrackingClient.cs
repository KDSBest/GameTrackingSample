using System;
using System.Collections;
using UnityEngine;
using SK.ClientLibrary;
using SK.ClientLibrary.Model;
using System.Net;

namespace Assets.GameTracking
{
	public class GameTrackingClient : MonoBehaviour
	{
		private int timestamp = 0;
		private SKRestClient restClient;
		private Snapshot snapshot = new Snapshot();

		public void Awake()
		{
			restClient = new SKRestClient();	
		}

		public void OnEnable()
		{
			this.StartCoroutine(SendCoRoutine());
		}

		private IEnumerator SendCoRoutine()
		{
			while(this.isActiveAndEnabled)
			{
				yield return new WaitForSeconds(1);

				try
				{
					snapshot.Timestamp = timestamp;
					snapshot.Lobby = "test";
					timestamp += 1000;
					snapshot.Id = $"{snapshot.Lobby}_{snapshot.Timestamp}";
					snapshot.GameObjects.Clear();
					var trackedObjects = GameObject.FindObjectsOfType<TrackedObject>();
					for (int i = 0; i < trackedObjects.Length; i++)
					{
						var trackedObject = trackedObjects[i];
						var pos = trackedObject.transform.position;
						snapshot.GameObjects.Add(trackedObject.Id, new SnapshotGameObject()
						{
							Id = trackedObject.Id,
							Type = trackedObject.Type.ToString(),
							Position = new SK.ClientLibrary.Model.Vector3(pos.x, pos.y, pos.z),
							MetadataJson = trackedObject.GetMetadata()
						});
					}
					var resp = restClient.PostSnapshot(snapshot);
					Debug.Log($"Game Tracking ({snapshot.Timestamp}): {resp}");
				}
				catch(Exception ex)
				{
					Debug.LogError(ex);
					this.gameObject.SetActive(false);
					break;
				}
			}
		}
	}
}

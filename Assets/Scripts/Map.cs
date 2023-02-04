using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
	[System.Serializable]
	protected struct TileConfiguration
	{
		public string name;

		public List<TileBase> tiles;

		public float speedScalar;

		public float damagePerSecond;
	}

	// THERE CAN BE ONLY ONNNNNNE
	private static Map mapInstance;

	[SerializeField]
	private Tilemap tilemap;

	[SerializeField]
	private TileConfiguration[] tileConfigs;

	// private Dictionary

	void Awake()
	{
		mapInstance = this;
	}

	public static float GetSpeedScalar(Vector3 atPosition)
	{
		if (!mapInstance || !mapInstance.tilemap)
		{
			return 1f;
		}


		Vector3Int positionInt = Vector3Int.FloorToInt(atPosition);
		TileConfiguration? config = GetConfigurationForTile(mapInstance.tilemap.GetTile(positionInt));

		return config.HasValue ? config.Value.speedScalar : 1f;
	}

	protected static TileConfiguration? GetConfigurationForTile(TileBase tile)
	{
		for (int i = 0; i < mapInstance.tileConfigs.Length; i++)
		{
			if (mapInstance.tileConfigs[i].tiles.Contains(tile))
			{
				return mapInstance.tileConfigs[i];
			}
		}

		return null;
	}
}

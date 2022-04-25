using PathCreation;
using PathCreation.Examples;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    [SerializeField] List<GenerationMap> maps = new List<GenerationMap>();
    [SerializeField] LayerMask interactableMask;
    [SerializeField] PathCreator pathCreator;
    VertexPath path;

    private void Start()
    {
        path = pathCreator.path;
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            var map = maps[i];
            var amountToPlace = Mathf.FloorToInt((path.length - map.initialOffset) / map.placementInterval);
            GameObject parent = new GameObject(map.mapName);
            parent.transform.SetParent(pathCreator.transform);

            for (int j = 0; j < amountToPlace; j++)
            {
                var randXOffset = Random.Range(-pathCreator.GetComponent<RoadMeshCreator>().roadWidth + 1, pathCreator.GetComponent<RoadMeshCreator>().roadWidth - 1);

                var randInteractable = Random.Range(0, maps[i].prefabs.Count);
                var distance = map.initialOffset + j * map.placementInterval;
                if (Physics.OverlapSphere(path.GetPointAtDistance(distance, EndOfPathInstruction.Stop), 10, interactableMask, QueryTriggerInteraction.Collide).Length > 0)
                {
                    continue;
                }
                var spawn = Instantiate(maps[i].prefabs[randInteractable], parent.transform);
                spawn.transform.rotation = path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
                spawn.transform.position = map.useRandomX ?
                path.GetPointAtDistance(distance, EndOfPathInstruction.Stop)
                + spawn.transform.right * randXOffset
                : path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
                spawn.transform.eulerAngles = new Vector3(spawn.transform.eulerAngles.x, spawn.transform.eulerAngles.y, 0f);
            }
        }
    }
}

[System.Serializable]
public struct GenerationMap
{
    public string mapName;
    public List<GameObject> prefabs;
    public int initialOffset;
    public int placementInterval;
    public bool useRandomX;
}

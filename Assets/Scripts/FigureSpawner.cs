using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    

    public List<string> shapes = new() { "Circle", "Square", "Triangle" };
    public List<Color> borderColors = new() { Color.red, Color.green, Color.blue };
    public List<Sprite> animalSprites;

    [SerializeField]private List<Transform> SpawnPoints = new List<Transform>();

    public GameObject figurePrefab;

    private List<GameObject> SpawnedFigures = new List<GameObject>();

    [SerializeField]private float Delay = 0.25f;
    public float figuresAmount = 10;

    private Coroutine currentCoroutine;

    void Start()
    {
        SpawnFiguresMethod();
    }

    public IEnumerator SpawnFiguresCoroutine(float AmountOItems) 
    {
        if (SpawnedFigures.Count > 0) 
        {
            foreach (var spawnedObj in SpawnedFigures) 
            {
                Destroy(spawnedObj);
            }
        }

        for (int i = 0; i < AmountOItems; i++)
        {
            var data = new FigureData
            {
                shape = shapes[Random.Range(0, shapes.Count)],
                borderColor = borderColors[Random.Range(0, borderColors.Count)],
                animalSprite = animalSprites[Random.Range(0, animalSprites.Count)]
            };

            GameObject figure = Instantiate(figurePrefab, SpawnPoints[Random.Range(0, SpawnPoints.Count)]);
            figure.GetComponent<FigureBehavior>().Setup(data);
            SpawnedFigures.Add(figure); 
            yield return new WaitForSeconds(Delay);
        }

    }

    public void SpawnFiguresMethod() 
    {
        if (currentCoroutine != null) 
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SpawnFiguresCoroutine(figuresAmount));
    }

}

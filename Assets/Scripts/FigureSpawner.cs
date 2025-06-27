using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    
    //Базовые настройки
    public List<string> shapes = new() { "Circle", "Square", "Triangle" };
    public List<Color> borderColors = new() { Color.red, Color.green, Color.blue };
    public List<Sprite> animalSprites;

    [SerializeField]private List<Transform> SpawnPoints = new List<Transform>();

    public GameObject figurePrefab;

    private List<GameObject> SpawnedFigures = new List<GameObject>();

    private float Delay;
    private float figuresAmount;

    private Coroutine currentCoroutine;
    public event Action OnSpawnFigures;

    void Start()
    {
        figuresAmount = GameManager.instance.levelSetting_SO.AmountToSpawn;
        Delay = GameManager.instance.levelSetting_SO.Delay;
        SpawnFiguresMethod();
    }

    private IEnumerator SpawnFiguresCoroutine(float AmountOItems, float Delay) 
    {
        OnSpawnFigures?.Invoke();
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
                shape = shapes[UnityEngine.Random.Range(0, shapes.Count)],
                borderColor = borderColors[UnityEngine.Random.Range(0, borderColors.Count)],
                animalSprite = animalSprites[UnityEngine.Random.Range(0, animalSprites.Count)]
            };

            GameObject figure = Instantiate(figurePrefab, SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)]);
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
        currentCoroutine = StartCoroutine(SpawnFiguresCoroutine(figuresAmount, Delay));
    }

}

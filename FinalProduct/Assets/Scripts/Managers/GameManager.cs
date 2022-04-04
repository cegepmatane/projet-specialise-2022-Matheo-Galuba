using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Map
{
    Dungeon,
    Desert,
    Forest
}

public class GameManager : MonoBehaviour
{
    // Create a static instance of the GameManager class
    public static GameManager Instance { get; private set; }

    // Map type to load
    private Map mapType = Map.Dungeon;

    private int population = 10;

    public void Awake()
    {
        // If the static instance is null, set it to this
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If the static instance is not null, destroy this
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to change the scene
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void LoadGame()
    {
        if (mapType == Map.Dungeon)
        {
            SceneManager.LoadScene("Dungeon");
        }
        else if (mapType == Map.Desert)
        {
            SceneManager.LoadScene("Desert");
        }
        else if (mapType == Map.Forest)
        {
            SceneManager.LoadScene("Forest");
        }
    }

    public void SetMapType(Map mapToSet)
    {
        mapType = mapToSet;
    }

    public void SetPopulation(int populationToSet)
    {
        population = populationToSet;
    }

    public int GetPopulation()
    {
        return population;
    }
}

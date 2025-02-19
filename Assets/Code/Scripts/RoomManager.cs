using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public Transform player;

    public Transform northSpawn;
    public Transform eastSpawn;
    public Transform southSpawn;
    public Transform westSpawn;

    public int[,] spawnPointGrid =
    {
        //Columns: Current Room
        //Rows: Previous Room
        //Values: Cardinal directions (0 is null)
        { 0, 0, 2, 1, 1 }, //Room1 -- FOOD COURT
        { 0, 0, 3, 3, 3 }, //Room2
        { 4, 2, 0, 4, 0 }, //Room3
        { 1, 3, 4, 0, 2 }, //Room4
        { 2, 1, 0, 2, 0 }, //Room5

    };

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        ItemCheck();
        SetSpawnPoint();
    }

    public void ItemCheck()
    {
        GameObject check;

        //Check if an item has already been picked up.
        //If so, do not have it spawn in the room.
        if (GameManager.instance.ItemsPickedUp.Count > 0)
        {
            foreach (Item item in GameManager.instance.ItemsPickedUp)
            {
                check = GameObject.Find(item.itemname);
                if (check != null)
                {
                    Destroy(check);
                }
            }
        }
    }

    public void SetSpawnPoint()
    {
        //Figure out what the current room number is and convert to an int
        string currentSceneName = SceneManager.GetActiveScene().name;
        string currentRoomNumberString = currentSceneName.Substring(currentSceneName.Length - 1);
        int currentRoomNumber;
        currentRoomNumber = int.Parse(currentRoomNumberString);

        //Figure out what the previous room number is and convert to an int
        string lastSceneName = SceneManager.GetActiveScene().name;
        string lastRoomNumberString = lastSceneName.Substring(lastSceneName.Length - 1);
        int lastRoomNumber;
        lastRoomNumber = int.Parse(lastRoomNumberString);

        //Check the array; Determine where to spawn
        int direction = spawnPointGrid[lastRoomNumber - 1, currentRoomNumber - 1];

        //Update player's transform to the spawn pos
        switch(direction)
        {
            case 0:
                break;
            case 1:
                player.position = northSpawn.position; break;
            case 2:
                player.position = eastSpawn.position; break;
            case 3:
                player.position = southSpawn.position; break;
            case 4:
                player.position = westSpawn.position; break;
        }
    }
}

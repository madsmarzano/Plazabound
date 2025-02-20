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
            // 0 -- NULL
            // 1 -- NORTH
            // 2 -- EAST
            // 3 -- SOUTH
            // 4 -- WEST
        { 0, 0, 2, 1, 1 }, //Room1 -- FOOD COURT
        { 0, 0, 3, 3, 3 }, //Room2
        { 4, 2, 0, 4, 0 }, //Room3
        { 1, 3, 4, 0, 2 }, //Room4
        { 2, 1, 0, 2, 0 }, //Room5
        { 3, 0, 0, 0, 0 }, //Room6

    };

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        ItemCheck();
        if (GameManager.instance.previousScene.Length > 0)
        {
            SetSpawnPoint();
        }
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
        Debug.Log("CURRENT ROOM FOR SPAWN: " + currentRoomNumber);

        //Figure out what the previous room number is and convert to an int
        string lastSceneName = GameManager.instance.previousScene;
        string lastRoomNumberString = lastSceneName.Substring(lastSceneName.Length - 1);
        int lastRoomNumber;
        lastRoomNumber = int.Parse(lastRoomNumberString);
        Debug.Log("PREV ROOM FOR SPAWN: " + lastRoomNumber);

        //Check the array; Determine where to spawn
        int direction = spawnPointGrid[lastRoomNumber - 1, currentRoomNumber - 1];

        //Update player's transform to the spawn pos and make sure they are facing the right way.
        switch(direction)
        {
            case 0:
                break;
            case 1:
                player.position = northSpawn.position;
                player.forward = -northSpawn.forward;
                break;
            case 2:
                player.position = eastSpawn.position;
                player.forward = -eastSpawn.forward;
                break;
            case 3:
                player.position = southSpawn.position;
                player.forward = -southSpawn.forward;
                break;
            case 4:
                player.position = westSpawn.position;
                player.forward = -westSpawn.forward;
                break;
        }
    }
}

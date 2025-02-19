using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// By Mads:
/// 
/// This script is attached to every vent that functions as a door to another room.
/// 
/// 
/// </summary>

public class Doors : MonoBehaviour
{

    public int[,] roomGridArray =
    {
        //North, East, South, West
        { 4, 5, 6, 3 }, //Room1 -- FOOD COURT
        { 5, 3, 4, 0 }, //Room2 -- CLOTHING STORE
        { 0, 1, 2, 4 }, //Room3 -- MUSIC STORE
        { 1, 5, 2, 3 }, //Room4 -- VENT MAZE
        { 1, 4, 2, 3 }, //Room5 -- PLAYPLACE
        { 0, 0, 1, 0 }  //Room6 -- ARCADE

    };

    private void OnTriggerEnter(Collider other)
    {

        //figure out scene we are currently in
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("The active scene name is " + currentSceneName);

        //convert scene name into an integer we can use
        //gets this number from the last character of the scene name
        string roomNumberString = currentSceneName.Substring(currentSceneName.Length - 1);
        Debug.Log("The active scene number is " + roomNumberString);

        //convert roomNumberString to an int
        int roomNumber;
        roomNumber = int.Parse(roomNumberString);

        //figure out where door leads
        int destination = 0;
        switch (this.name)
        {
            case "DoorN":
                destination = roomGridArray[roomNumber - 1, 0];
                break;
            case "DoorE":
                destination = roomGridArray[roomNumber - 1, 1];
                break;
            case "DoorS":
                destination = roomGridArray[roomNumber - 1, 2];
                break;
            case "DoorW":
                destination = roomGridArray[roomNumber - 1, 3];
                break;
        }

        if (destination == 0)
        {
            Debug.Log("No destination set.");
            return;
        }

        //convert the destination number to a room name
        string destinationString = destination.ToString();
        string nextScene = "Room" + destinationString;
        Debug.Log("Traveling to " + nextScene);

        //store the name of the current scene in the previousScene variable within GameManager
        GameManager.instance.previousScene = currentSceneName;

        //go to destination
        SceneManager.LoadScene(nextScene);
    }

}

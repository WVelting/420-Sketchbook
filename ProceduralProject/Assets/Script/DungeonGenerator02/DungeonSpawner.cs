using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{

    public Room prefabRoom;
    
    [Range(4, 15)]
    public int dungeonSize = 10;

    [Range(1, 10)]
    public int spaceBetweenRooms = 5;

    void Start()
    {
        //spawn a DungeonLayout
        DungeonLayout dungeon = new DungeonLayout();
        
        dungeon.Generate(dungeonSize);

        int [,] rooms = dungeon.GetRooms();

        //loop through rooms, spawn prefabs
        for(int x = 0; x < rooms.GetLength(0); x++){
            for(int y = 0; y < rooms.GetLength(1); y++){

                if(rooms[x,y] == 0) continue; //skips

                Vector3 pos = new Vector3(x, 0, y) * spaceBetweenRooms;

                Room newRoom = Instantiate(prefabRoom, pos, Quaternion.identity);
                newRoom.InitRoom((RoomType)rooms[x,y]);
            }
        }
    }//ends start

}//ends DungeonSpawner

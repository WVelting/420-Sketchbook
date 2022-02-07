using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Void, //0
    Normal, //1
    RandomPOI, //2
    Merchant, //3
    Shrine, //4
    QuestGiver, //5
    Loot, //6
    FloorEnter, //7
    FloorExit //8
}


public class DungeonLayout
{

    private int lilPerBig = 5;
    private int res = 10;
    private int hiRes = 0;
    private int[,] rooms;
    private int[,] bigRooms;

    public void Generate(int size)
    {
        res = size;
        hiRes = size * lilPerBig;

        bigRooms = new int[res, res];
        rooms = new int[hiRes, hiRes];

        WalkRooms(RoomType.FloorEnter, RoomType.FloorExit);
        WalkRooms(RoomType.RandomPOI, RoomType.RandomPOI);
        WalkRooms(RoomType.RandomPOI, RoomType.RandomPOI);
        WalkRooms(RoomType.RandomPOI, RoomType.RandomPOI);


        MakeBigRooms();

        //PunchHoles();
    }

    private void WalkRooms(RoomType a, RoomType b)
    {

        //starting room:
        int x = Random.Range(0, hiRes);
        int y = Random.Range(0, hiRes);

        int half = hiRes / 2;

        //ending room:
        int tx = Random.Range(0, half);
        int ty = Random.Range(0, half);

        if (x < half) tx += half;
        if (y < half) ty += half;

        //insert two rooms into dungeon:
        SetLilRoom(x, y, (int)a);
        SetLilRoom(tx, ty, (int)b);

        //walk to target room:
        while (x != tx || y != ty)
        {

            int dir = Random.Range(0, 4); //0 - 3
            int dis = Random.Range(2, 6); //2 - 5

            //get distance to target
            int dx = tx - x;
            int dy = ty - y;

            //sometimes, DON'T randomize the direction
            if(Random.Range(0, 100) < 50){

                if(Mathf.Abs(dx) > Mathf.Abs(dy)){
                    dir = (dx > 0) ? 3:2;
                }
                else{
                    dir = (dy > 0) ?  1:0;
                }
            }

            //step into the next room:
            for (int i = 0; i < dis; i++)
            {

                if (dir == 0) y--;
                if (dir == 1) y++;
                if (dir == 2) x--;
                if (dir == 3) x++;

                x = Mathf.Clamp(x, 0, hiRes - 1);
                y = Mathf.Clamp(y, 0, hiRes - 1);

                if (GetLilRoom(x, y) == 0)
                {

                    SetLilRoom(x, y, 1);
                }


            }//ends for

        }//ends while
    }//ends WalkRooms()

    private void MakeBigRooms()
    {
        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                
                int val = GetLilRoom(x, y);

                int xb = x / lilPerBig;
                int yb = y / lilPerBig;

                //if bigroom val < lilroom val
                if(GetBigRoom(xb, yb) < val){
                    SetBigRoom(xb, yb, val); //set bigroom = lilroom
                }
            }
        }
    }

    public int[,] GetRooms()
    {

        if (bigRooms == null)
        {
            Debug.LogError("Dungeon Layout: Big Rooms is a null array. Call Generate() before GetRooms()");
            return new int[0, 0];

        }
        //make an empty array, same size:
        int[,] copy = new int[bigRooms.GetLength(0), bigRooms.GetLength(1)];

        //make a copy:
        System.Array.Copy(bigRooms, 0, copy, 0, bigRooms.Length);

        //return the copy:
        return copy;
    }

    private int GetLilRoom(int x, int y)
    {

        if (rooms == null) return 0;

        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= rooms.GetLength(0)) return 0;
        if (y >= rooms.GetLength(1)) return 0;

        return rooms[x, y];
    }

    private void SetLilRoom(int x, int y, int val)
    {

        if (rooms == null) return;

        if (x < 0) return;
        if (y < 0) return;
        if (x >= rooms.GetLength(0)) return;
        if (y >= rooms.GetLength(1)) return;

        rooms[x, y] = val;
    }

    public int GetBigRoom(int x, int y)
    {

        if (bigRooms == null) return 0;

        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= bigRooms.GetLength(0)) return 0;
        if (y >= bigRooms.GetLength(1)) return 0;

        return bigRooms[x, y];
    }

    private void SetBigRoom(int x, int y, int val)
    {

        if (bigRooms == null) return;

        if (x < 0) return;
        if (y < 0) return;
        if (x >= bigRooms.GetLength(0)) return;
        if (y >= bigRooms.GetLength(1)) return;

        bigRooms[x, y] = val;
    }

}



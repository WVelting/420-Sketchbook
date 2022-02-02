using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    int roomSize = 1;
    int res = 10;
    int[,] rooms;

    int lilPerBig = 5;

    bool drawLilRooms = false;

    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;


    int lowRes()
    {
        return res / lilPerBig;
    }

    int[,] bigRooms;


    void Start()
    {
        generate();

    }

    void setRoom(int x, int y, int type)
    {
        if (x < 0) return;
        if (y < 0) return;
        if (x >= res) return;
        if (y >= res) return;

        int temp = getRoom(x, y);

        if (temp < type) rooms[x, y] = type;
    }

    int getRoom(int x, int y)
    {
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= res) return 0;
        if (y >= res) return 0;

        return rooms[x, y];
    }

    void generate()
    {
        rooms = new int[50,50];

        walkRooms(3,4);
        walkRooms(2,2);
        walkRooms(2,2);
        walkRooms(2,2);

        makeBigRooms();
        
        punchHoles();

        print("done generating");
    }

    void setBigRoom(int x, int y, int type)
    {
        if (x < 0) return;
        if (y < 0) return;
        if (x >= bigRooms.Length) return;
        if (y >= bigRooms.Length) return;

        int temp = getBigRoom(x, y);

        if (temp < type) bigRooms[x, y] = type;
    }

    int getBigRoom(int x, int y)
    {
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= bigRooms.Length) return 0;
        if (y >= bigRooms.Length) return 0;

        return bigRooms[x, y];
    }

    void punchHoles()
    {
        for (int x = 0; x < res; x++)
        {
            for (int y = 0; y < res; y++)
            {

                int val = getBigRoom(x, y);
                if (val != 1) continue;

                if (Random.Range(0, 100) < 25)
                {

                    int[] neighbors = new int[8];

                    neighbors[0] = getBigRoom(x, y - 1);
                    neighbors[1] = getBigRoom(x + 1, y - 1);
                    neighbors[2] = getBigRoom(x + 1, y);
                    neighbors[3] = getBigRoom(x + 1, y + 1);
                    neighbors[4] = getBigRoom(x, y + 1);
                    neighbors[5] = getBigRoom(x - 1, y + 1);
                    neighbors[6] = getBigRoom(x - 1, y);
                    neighbors[7] = getBigRoom(x - 1, y - 1);

                    bool isSolid = neighbors[7] > 0;
                    int tally = 0;
                    foreach (int n in neighbors)
                    {
                        bool s = n > 0;

                        if (s != isSolid) tally++;

                        isSolid = s;

                    }

                    if (tally <= 2)
                    {
                        setBigRoom(x, y, 0);
                    }
                }

            }

        }
    }

    void makeBigRooms()
    {
        int r = lowRes();
        bigRooms = new int[r, r];

        for (int x = 0; x < res; x++)
        {
            for (int y = 0; y < res; y++)
            {
                int val = getRoom(x, y);

                int val2 = bigRooms[x / lilPerBig, y / lilPerBig];

                if (val > val2)
                {
                    bigRooms[x / lilPerBig, y / lilPerBig] = val;
                }
            }
        }
    }

    void walkRooms(int type1, int type2)
    {

        int halfW = rooms.Length / 2;
        int halfH = rooms.Length / 2;

        int x = (int)Random.Range(0, rooms.Length);
        int y = (int)Random.Range(0, rooms.Length);

        int tx = (int)Random.Range(0, halfW);
        int ty = (int)Random.Range(0, halfH);

        if (x < halfW) tx += halfW;
        if (y < halfH) ty += halfH;

        setRoom(x, y, type1);
        setRoom(tx, ty, type2);

        while (x != tx || y != ty)
        {
            int dir = (int)Random.Range(0, 4);
            int dis = (int)Random.Range(1, 4);

            if (Random.Range(0, 100) > 50)
            {
                int dx = tx - x;
                int dy = ty - y;

                if (Mathf.Abs(dx) < Mathf.Abs(dy))
                {
                    dir = (dy < 0) ? 0 : 1;
                }
                else dir = (dx < 0) ? 3 : 2;
            }
            for (int i = 0; i < dis; i++)
            {
                switch (dir)
                {
                    case 0:
                        y--;
                        break;
                    case 1:
                        y++;
                        break;
                    case 2:
                        x++;
                        break;
                    case 3:
                        x--;
                        break;

                }

                x = Mathf.Clamp(x, 0, res - 1);
                y = Mathf.Clamp(y, 0, res - 1);

                setRoom(x, y, 1);
            }
        }

    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) generate();
        float px = roomSize;

        for(int x = 0; x<res; x++){
            for(int y = 0; y<res; y++){
                int val = rooms[x,y];
                if (val> 0){
                    switch(val){
                        case 1:
                            Instantiate(t1, new Vector3(x, 0, y)*px, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(t2, new Vector3(x, 0, y)*px, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(t3, new Vector3(x, 0, y)*px, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(t4, new Vector3(x, 0, y)*px, Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

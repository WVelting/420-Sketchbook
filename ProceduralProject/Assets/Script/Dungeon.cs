using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int roomSize = 1;
    public int res = 10;
    int[,] rooms;

    int lilPerBig = 5;

    bool drawLilRooms = false;

    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;
    public Transform parent;


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
        print("setting");
        if (x < 0) return;
        if (y < 0) return;
        if (x >= rooms.GetLength(0)) return;
        if (y >= rooms.GetLength(1)) return;
        int temp = getRoom(x, y);

        if (temp < type) rooms[x, y] = type;
        print("done setting");
    }

    int getRoom(int x, int y)
    {
        print("getting");
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= rooms.GetLength(0)) return 0;
        if (y >= rooms.GetLength(1)) return 0;
        print("done getting");

        return rooms[x, y];
    }

    void generate()
    {

        
        rooms = new int[res, res];

        walkRooms(3, 4);
        walkRooms(2, 2);
        walkRooms(2, 2);
        walkRooms(2, 2);

        makeBigRooms();

        punchHoles();

        print("done generating");
    }

    void setBigRoom(int x, int y, int type)
    {
        print("setting");
        if (x < 0) return;
        if (y < 0) return;
        if (x >= bigRooms.GetLength(0)) return;
        if (y >= bigRooms.GetLength(1)) return;

        int temp = getBigRoom(x, y);

        if (temp < type) bigRooms[x, y] = type;
        print("done setting");
    }

    int getBigRoom(int x, int y)
    {
        print("getting");
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= bigRooms.GetLength(0)) return 0;
        if (y >= bigRooms.GetLength(1)) return 0;

        print("done getting");
        return bigRooms[x, y];
    }

    void punchHoles()
    {
        print("punching holes");
        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {

                int val = getBigRoom(x, y);
                if (val != 1) continue;

                if (UnityEngine.Random.Range(0, 100) < 25)
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

        print("done punching holes");
    }

    void makeBigRooms()
    {
        int r = lowRes();
        bigRooms = new int[r, r];

        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
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
        print("walking");

        int halfW = rooms.GetLength(0) / 2;
        int halfH = rooms.GetLength(1) / 2;

        int x = (int)UnityEngine.Random.Range(0, rooms.GetLength(0));
        int y = (int)UnityEngine.Random.Range(0, rooms.GetLength(1));

        int tx = (int)UnityEngine.Random.Range(0, halfW);
        int ty = (int)UnityEngine.Random.Range(0, halfH);

        if (x < halfW) tx += halfW;
        if (y < halfH) ty += halfH;

        setRoom(x, y, type1);
        setRoom(tx, ty, type2);

        while (x != tx || y != ty)
        {
            int dir = (int)UnityEngine.Random.Range(0, 4);
            int dis = (int)UnityEngine.Random.Range(1, 4);

            if (UnityEngine.Random.Range(0, 100) > 50)
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

        print("done walking");
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            
            foreach(Transform child in transform) Destroy(child.gameObject);
            generate();
        
        }
        if (Input.GetKeyDown(KeyCode.L)){
            if(drawLilRooms){
                drawLilRooms = false;
                foreach(Transform child in transform) Destroy(child.gameObject);
                
            } 

            else drawLilRooms = true;
        }
        float px = roomSize;

        if (drawLilRooms)
        {

            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    int val = rooms[x, y];
                    if (val > 0)
                    {
                        switch (val)
                        {
                            case 1:
                                GameObject obj1 = Instantiate(t1, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                break;
                            case 2:
                                GameObject obj2 = Instantiate(t2, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                break;
                            case 3:
                                GameObject obj3 = Instantiate(t3, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                break;
                            case 4:
                                GameObject obj4 = Instantiate(t4, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        px = roomSize * lilPerBig;

        for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    int val = rooms[x, y];
                    if (val > 0)
                    {
                        switch (val)
                        {
                            case 1:
                                GameObject obj1 = Instantiate(t1, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                obj1.transform.localScale *= lilPerBig; 
                                break;
                            case 2:
                                GameObject obj2 = Instantiate(t2, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                obj2.transform.localScale *= lilPerBig;
                                break;
                            case 3:
                                GameObject obj3 = Instantiate(t3, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                obj3.transform.localScale *= lilPerBig;
                                break;
                            case 4:
                                GameObject obj4 = Instantiate(t4, new Vector3(x, 0, y) * px, Quaternion.identity, parent);
                                obj4.transform.localScale *= lilPerBig;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }


    }
}

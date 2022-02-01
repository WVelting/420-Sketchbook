class Dungeon {

  int roomSize = 10;
  int res = 50;
  int[][] rooms;

  int lilPerBig = 5;
  
  boolean drawLilRooms = false;

  int lowRes() { 
    return res/lilPerBig;
  }

  int[][] bigRooms;

  Dungeon() {
    generate();
  }

  //set room type -------------------------
  void setRoom(int x, int y, int type) {
    if (x<0) return;
    if (y<0) return;
    if (x>=rooms.length) return;
    if (y>=rooms[x].length) return;

    int temp = getRoom(x, y);

    if (temp < type) rooms[x][y] = type;
  }

  //check room if room on map ------------------
  int getRoom(int x, int y) {
    //check for errors
    if (x<0) return 0;
    if (y<0) return 0;
    if (x>=rooms.length) return 0;
    if (y>=rooms[x].length) return 0;

    return rooms[x][y];
  }

  //generate the dungeon ----------------------------
  void generate() {
    rooms = new int[res][res];

    //TODO: fill with data

    //randomness---------------------------------
    //for(int x = 0; x < rooms.length; x++){
    //  for(int y = 0; y< rooms.length; y++){
    //    rooms[x][y] = (int)random(0,5);
    //  }
    //}

    //walking algos-------------------------------
    walkRooms(3, 4);
    walkRooms(2, 2);
    walkRooms(2, 2);
    walkRooms(2, 2);

    makeBigRooms();

    //punch holes ----------------------------------
    punchHoles();

    //check for islands...

    println("done generating");
  }

  void setBigRoom(int x, int y, int type) {
    if (x<0) return;
    if (y<0) return;
    if (x>=bigRooms.length) return;
    if (y>=bigRooms[x].length) return;

    bigRooms[x][y] = type;
  }

  //check room if room on map ------------------
  int getBigRoom(int x, int y) {
    //check for errors
    if (x<0) return 0;
    if (y<0) return 0;
    if (x>=bigRooms.length) return 0;
    if (y>=bigRooms[x].length) return 0;

    return bigRooms[x][y];
  }

  void punchHoles() {

    for (int x = 0; x < bigRooms.length; x++) {
      for (int y = 0; y<bigRooms[x].length; y++) {

        int val = getBigRoom(x, y);
        if (val != 1) continue;

        if (random(0, 100) < 25)
        {

          int[] neighbors = new int[8];

          neighbors[0] = getBigRoom(x, y-1);//above
          neighbors[1] = getBigRoom(x+1, y-1);
          neighbors[2] = getBigRoom(x+1, y);//right
          neighbors[3] = getBigRoom(x+1, y+1);
          neighbors[4] = getBigRoom(x, y+1);//below
          neighbors[5] = getBigRoom(x-1, y+1);
          neighbors[6] = getBigRoom(x-1, y);//left
          neighbors[7] = getBigRoom(x-1, y-1);

          boolean isSolid = neighbors[7] > 0;
          int tally = 0;
          for (int n : neighbors) {
            boolean s = n > 0;

            if ( s != isSolid) tally ++;

            isSolid = s;
          }

          if (tally<= 2) { //safe to punch hole
            setBigRoom(x, y, 0);
          }
        }
      }
    }
  }

  void makeBigRooms() {
    int r = lowRes();
    bigRooms = new int[r][r];

    for (int x = 0; x < rooms.length; x++) {
      for (int y = 0; y<rooms[x].length; y++) {
        int val = getRoom(x, y);

        int val2 = bigRooms[x/lilPerBig][y/lilPerBig];

        if (val > val2) {
          bigRooms[x/lilPerBig][y/lilPerBig] = val;
        }
      }
    }
  }

  //decide dungeon path --------------------
  void walkRooms(int type1, int type2) {

    int halfW = rooms.length/2;
    int halfH = rooms[0].length/2;

    //start point
    int startX = (int)random(0, rooms.length);
    int startY = (int)random(0, rooms[startX].length);
    //end point
    int endX = (int)random(0, halfW);
    int endY = (int)random(0, halfH);

    //if starting coordinate is in 1st Quadrant of dungeon, move
    if (startX < halfW) endX += halfW;
    if (startY < halfH) endY += halfH;

    setRoom(startX, startY, type1);
    setRoom(endX, endY, type2);


    //walk algo
    while (startX != endX || startY != endY) {



      int dir = (int)random(0, 4); // 0 to 3 (n/s/e/w)
      int dis = (int)random(1, 4); //1 to 3 (distance)

      if (random(0, 100) > 50) {
        int dx = endX - startX;
        int dy = endY - startY;

        if (abs(dx) < abs(dy)) { //are we closer on X than Y
          dir = (dy < 0) ? 0:1;
        } else dir = (dx<0) ? 3:2;
      }

      for (int i = 0; i<dis; i++) {
        switch(dir) {
        case 0:
          startY--;//move north
          break;
        case 1:
          startY++;//move south
          break;
        case 2:
          startX++;//move east
          break;
        case 3:
          startX--;//move west
          break;
        }
        startX = constrain(startX, 0, res - 1);
        startY = constrain(startY, 0, res - 1);

        setRoom(startX, startY, 1);
      }
    }
  }

  //draw the dungeon ---------------------------------

  void draw() {
    noStroke();
    
    

    float px = roomSize;

    for (int x = 0; x < rooms.length; x++) {
      for (int y = 0; y < rooms[x].length; y++) {
        int val = rooms[x][y];
        if (val > 0) {
          switch(val) {
          case 1:
            fill(255);
            break;
          case 2:
            fill(0, 255, 0);
            break;
          case 3:
            fill(255, 0, 0);
            break;
          case 4:
            fill(0, 0, 255);
            break;
          default: 
            fill(0);
          }
          //rect(x * px, y * px, px, px);
        }
      }
    }

    //draw big Rooms
    px = roomSize * lilPerBig;
    for (int x = 0; x < bigRooms.length; x++) {
      for (int y = 0; y < bigRooms[x].length; y++) {
        int val = bigRooms[x][y];
        if (val > 0) {
          switch(val) {
          case 1:
            fill(255);
            break;
          case 2:
            fill(0, 255, 0);
            break;
          case 3:
            fill(255, 0, 0);
            break;
          case 4:
            fill(0, 0, 255);
            break;
          default: 
            fill(0);
          }
          rect(x * px, y * px, px, px);
        }
      }
    }
  }
}

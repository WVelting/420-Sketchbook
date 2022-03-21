class FlowfieldGrid {

  //properties:
  float zoom = 10;
  int res = 10;
  boolean isHidden = false;

  //cached data:
  float[][] data;


  FlowfieldGrid() {

    build();
  }

  void build() {
    data = new float[res][res];
    float w = getCellWidth();
    float h = getCellHeight();

    int thresh = 3;

    for (int x = 0; x < data.length; x++) {
      for (int y = 0; y < data[x].length; y++) {
        float val = noise(x/zoom, y/zoom);
        val = map(val, 0, 1, -PI * 2, PI * 2);

        // detect if cell is near side of screen
        //if it is...use atan2() to find angle to center of screen

        if (x < thresh || y < thresh || x >= data.length - thresh || y >= data[0].length - thresh) {
          float dy = (height/(float)2) - (y * h + h/2);
          float dx = (width/(float)2) - (x * w + w/2);

          val = atan2(dy, dx);
        }




        data[x][y] = val;
      }
    }
  }

  void update() {

    boolean rebuild = false;

    if (Keys.onDown(32)) {
      isHidden = !isHidden;
      rebuild = true;
    }


    if (Keys.onDown(37)) {
      res--;
      rebuild = true;
    }

    if (Keys.onDown(39)) {
      res++;
      rebuild = true;
    }

    if (Keys.onDown(38)) {
      zoom++;
      rebuild = true;
    }

    if (Keys.onDown(40)) {
      zoom--;
      rebuild = true;
    }

    res = constrain(res, 4, 100);
    zoom = constrain(zoom, 5, 50);

    if (rebuild) build();
  }
  void draw() {

    if (isHidden) return;

    float w = getCellWidth();
    float h = getCellHeight();

    for (int x = 0; x < data.length; x++) {
      for (int y = 0; y< data[x].length; y++) {

        float val = data[x][y];

        float topLeftX = x*w;
        float topLeftY = y*h;

        pushMatrix();
        translate(topLeftX + w/2, topLeftY + h/2);
        rotate(val);

        float hue = map(val, -PI * 2, PI * 2, 0, 255);

        stroke(255);
        colorMode(HSB);
        fill(hue, 255, 255);
        ellipse(0, 0, 20, 20);

        line(0, 0, 30, 0);
        popMatrix();
      }//end y loop
    }//end x loop
  }// end draw()

  float getCellWidth() {

    return width/res;
  }

  float getCellHeight() {

    return height/res;
  }

  float getDirectionAt(PVector p) {
    return getDirectionAt(p.x, p.y);
  }
  float getDirectionAt(float x, float y) {

    int indexX = (int)(x/getCellWidth());
    int indexY = (int)(y/getCellHeight());

    if (indexX<0 || indexY<0 || indexX >= data.length || indexY >= data[0].length) {
      //invalidate coordinate...

      float dy = (height/(float)2) - y;
      float dx = (width/(float)2) - x;

      return atan2(dy, dx);
    }

    return data[indexX][indexY];
  }
}

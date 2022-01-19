
ArrayList<PVector> blox = new ArrayList<PVector>();

float threshold = .5;
float zoom = 10;

float sizeOfBlox = 10;
int dimOfBlox = 30;


void setup() {
  size(800, 500, P3D);
  noStroke();

  generateTerrainData();
}

void checkInput(){
  boolean shouldRegen = false;
  
  if(Keys.PLUS()){
     threshold += .01;
     shouldRegen = true;
  }
  
  if(Keys.MINUS()){
    threshold -= .01;
    shouldRegen = true;
  }
  
  if(Keys.BRACKET_LEFT()){
     zoom -= .1;
     shouldRegen = true;
  }
  
  if(Keys.BRACKET_RIGHT()){
    zoom += .1;
    shouldRegen = true;
  }
  
  if(shouldRegen){
    zoom = constrain(zoom, 1, 50);
    threshold = constrain(threshold, 0, 1);
    generateTerrainData();
  }
  
}

void generateTerrainData() {
  
  blox.clear();
  
  float[][][] data = new float[dimOfBlox][dimOfBlox][dimOfBlox];

  for (int x = 0; x<dimOfBlox; x++) {
    for (int y = 0; y<dimOfBlox; y++) {
      for (int z = 0; z<dimOfBlox; z++) {
        data[x][y][z] = noise(x/zoom, y/zoom, z/zoom) + y/100.0;
      }
    }
  }
  
  //check for occlusion...

  for (int x = 0; x<dimOfBlox; x++) {
    for (int y = 0; y<dimOfBlox; y++) {
      for (int z = 0; z<dimOfBlox; z++) {
        if (data[x][y][z] > threshold) {
          blox.add(new PVector(x, y, z));
        }
      }
    }
  }
}

void draw() {
  checkInput();
  background(0);
  
  lights();
  pushMatrix();
  
  translate(width/2, height/2);
  //rotateX(map(-mouseY, 0, height, -1, 1));
  rotateY(map(mouseX, 0, width, -PI, PI));
  
  translate(-dimOfBlox*sizeOfBlox/2, -dimOfBlox*sizeOfBlox/2);
    
  for (PVector pos : blox) {
    pushMatrix();
    translate(pos.x*sizeOfBlox, pos.y*sizeOfBlox, pos.z*sizeOfBlox);
    box(sizeOfBlox, sizeOfBlox, sizeOfBlox);
    popMatrix();
  }
  popMatrix();
}

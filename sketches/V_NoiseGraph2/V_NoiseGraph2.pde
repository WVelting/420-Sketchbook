//float [] valsWave;
//float [] valsNoise;
//float [] valsRand;

ArrayList<PVector> vals = new ArrayList<PVector>();

void setup() {
  size (500,600);
}

void draw(){
  background(0);
  
  // add new values to our array:
  float time = millis() / 1000.0;
  float valWave = map(sin(time), -1, 1, 0, 1);
  float valRand = random(0,1);
  float valNoise = noise(time);
  
  vals.add(0, new PVector(valWave, valRand, valNoise));
  
  //remove las item, if too many:
  if (vals.size() > width) vals.remove(vals.size() - 1);
  // draw three arrays to the screen:
  float third = height/3;
  
  for(int x=0; x< vals.size(); x++){
    
  };
  
  
  
}

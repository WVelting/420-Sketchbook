PImage img;
PShader shader;

void setup(){
  size(1440,980, P2D);
  
  img = loadImage("invertstuff.jpg");
  imageMode(CENTER);
  
  shader = loadShader("frag.glsl", "vert.glsl");
  
  
  
}

void draw(){
  //background(100);
  pushMatrix();
  translate(mouseX, mouseY);
  scale(.25);
  image(img, 0, 0);
  popMatrix();
  
  
  filter(shader);
  
}

PImage img;
PShader shader;

void setup(){
  size(1440,980, P2D);
  
  img = loadImage("invertstuff.jpg");
  imageMode(CENTER);
  
  shader = loadShader("frag.glsl", "vert.glsl");
  
}

void draw(){
  background(100);
  image(img, mouseX, mouseY);
  
  filter(shader);
  
}

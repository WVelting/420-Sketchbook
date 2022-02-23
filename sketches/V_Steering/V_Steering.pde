
ArrayList<Agent> agents = new ArrayList<Agent>();
float red;
float green;
float blue;
BigFish big;

void setup(){
   size(1000, 800); 
   background(0);
   noStroke();
   big = new BigFish();
}

void draw(){
  
  
  fill(0, 0, 0, 10);
  rect(0,0, width, height);
 
 
 if(mousePressed){
    agents.add(new Agent());
 }
 
 fill(255, 0, 0);
 
 big.update();
 big.draw();
 
 fill(red, green, blue);
 
 for(Agent a : agents){
   a.update();
   
   a.draw();
    
   }
   
   red += random(-25, 25);
   blue += random(-25, 25);
   green += random(-25, 25);
   
 
  
}

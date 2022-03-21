
ArrayList<Agent> agents = new ArrayList<Agent>();
float red;
float green;
float blue;

FlowfieldGrid grid;


void setup(){
   size(1000, 800); 
   
   grid = new FlowfieldGrid();
   
   background(0);
   noStroke();

}

void draw(){
  
  noStroke();
  //fill(0, 0, 0, 10);
  //rect(0,0, width, height);
  background(0);
 
 
 if(mousePressed){
    agents.add(new Agent());
 }
 
 
 
 fill(128);
 grid.update();
 grid.draw();
 
 fill(255, 0, 0);
 

 
 fill(red, green, blue);
 
 for(Agent a : agents){
   a.update();
   
   a.draw();
    
   }
   
   red += random(-25, 25);
   blue += random(-25, 25);
   green += random(-25, 25);
   
 Keys.update();
  
}

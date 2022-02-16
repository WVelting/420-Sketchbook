class Agent {

  color colour;
  
  PVector position = new PVector();
  PVector velocity = new PVector();
  PVector force = new PVector();


  float mass;
  float size;

  boolean hasUpdated = false;

  Agent(float massMin, float massMax) {
    position.x = random(0, width);
    position.y = random(0, height);

    mass = random(massMin, massMax);
    size = sqrt(mass);
    
    colorMode(HSB);
    colour = color(random(0,255), 255, 255);
  }

  void update() {

    for (Agent a : agents) {

      if (a == this) continue;
      if(a.hasUpdated) continue;

      PVector f = findGravityForce(a);
      force.add(f);
      

      //110
    }
    hasUpdated = true;

    PVector acceleration = PVector.div(force, mass); //a = f/m
    force.set(0, 0); //clear force

    velocity.add(acceleration); //v += a

    position.add(velocity); //p += velocity
  }

  void draw() {
    
    fill(colour);
    ellipse(position.x, position.y, size, size);

    if (position.x>width)position.x = 0;
    if (position.x<0)position.x = width;

    if (position.y>height)position.y =0;
    if (position.y<0)position.y = height;
  }

  PVector findGravityForce(Agent a) {

    PVector vToA = PVector.sub(a.position, position);
    float r = vToA.mag();

    float gravForce = G * (a.mass * mass) / (r*r);

    if (gravForce>maxForce) gravForce = maxForce;

    vToA.normalize();
    vToA.mult(gravForce);
    
    //add force to other object
    a.force.add(PVector.mult(vToA, -1));

    return vToA;
  }
}

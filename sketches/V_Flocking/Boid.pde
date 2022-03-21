
class Boid {

  int TYPE = 1;

  PVector position = new PVector();
  PVector velocity = new PVector();
  PVector force = new PVector();

  PVector _dir = new PVector();

  float mass = 1;
  float speed = 10;

  float radiusCohesion = 200;
  float radiusAlignment = 100;
  float radiusSeparation = 50;

  float forceCohesion = .5;
  float forceAlignment = .1;
  float forceSeparation = 10;

  Boid(float x, float y) {
    position.x = x;
    position.y = y;

    velocity.x = random(-3, 3);
    velocity.y = random(-3, 3);
  }

  void calcForces(Flock f) {

    //calculate forces!!

    //1. cohesion - pull towards group center
    //2. separation - pushes boids apart
    //3. alignment - turn boid to nearby avg direction

    // force that pushes boid to center
    // force that pushes boids away from sides
    // ...

    PVector centerOfGroup = new PVector();
    int numCohesion = 0;

    PVector dirOfGroup = new PVector();
    int numAlignment = 0;

    for (Boid b : f.boids) {

      if (b == this) continue; // if same boid, skip code

      float dx = b.position.x - position.x;
      float dy = b.position.y - position.y;
      float dis = sqrt(dx*dx + dy*dy); //pythagorean theorem


      
        if (dis < radiusCohesion) {
          centerOfGroup.add(b.position);
          numCohesion ++;
        }

        if (dis < radiusSeparation) {
          PVector awayFromB = new PVector(-dx/dis, -dy/dis);
          awayFromB.mult(forceSeparation / dis);

          force.add(awayFromB);
        }

        if (dis < radiusAlignment) {
          dirOfGroup.add(b._dir);
          numAlignment++;
        }
      } // end of for loop

      if (numCohesion > 0) {
        centerOfGroup.div(numCohesion);
        // todo: steer torwards centerOfGroup

        PVector dirToCenter = PVector.sub(centerOfGroup, position).setMag(speed);

        PVector cohesionForce = PVector.sub(dirToCenter, velocity);
        cohesionForce.limit(forceCohesion);
        force.add(cohesionForce);
      }

      if (numAlignment > 0) {

        dirOfGroup.div(numAlignment); //get avg direction
        dirOfGroup.mult(speed); // get desired vel = dir * max speed

        PVector alignmentForce = PVector.sub(dirOfGroup, velocity);

        alignmentForce.limit(forceAlignment);
        force.add(alignmentForce);
      }
   
  }

  void updateAndDraw() {

    //euler step:
    PVector acceleration = PVector.div(force, mass);
    velocity.add(acceleration);
    position.add(velocity);
    force = new PVector(0, 0, 0);

    // cache the direction vector:
    _dir = PVector.div(velocity, velocity.mag());

    if (position.x > width) position.x -= width;
    if (position.x < 0) position.x += width;

    if (position.y > height) position.y -= height;
    if (position.y < 0) position.y += height;

    ellipse(position.x, position.y, 10, 10);
  }
}

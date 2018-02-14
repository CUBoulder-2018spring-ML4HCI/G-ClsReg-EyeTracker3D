import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import oscP5.*; 
import netP5.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class Simple_Mouse_DraggedObject_2Inputs extends PApplet {

/**
* REALLY simple processing sketch that sends mouse x and y position of box to wekinator
* This sends 2 input values to port 6448 using message /wek/inputs
* Adapated from https://processing.org/examples/mousefunctions.html by Rebecca Fiebrink
**/




OscP5 oscP5;
NetAddress dest;
PFont f;

float bx;
float by;
int boxSize = 30;
boolean overBox = false;
boolean locked = false;
float xOffset = 0.0f; 
float yOffset = 0.0f;

public void setup() {
  f = createFont("Courier", 15);
  textFont(f);

  
  noStroke();
  
  
  bx = width/2.0f;
  by = height/2.0f;
  rectMode(RADIUS);  
  
  /* start oscP5, listening for incoming messages at port 12000 */
  oscP5 = new OscP5(this,9000);
  dest = new NetAddress("127.0.0.1",6448);
  
}

public void draw() {
  background(0);
  fill(255);
  /*ellipse(mouseX, mouseY, 10, 10);
  if(frameCount % 2 == 0) {
    sendOsc();
  } */
  text("Continuously sends box x and y position (2 inputs) to Wekinator\nUsing message /wek/inputs, to port 6448", 10, 30);
  text("x=" + bx + ", y=" + by, 10, 80);
  
  fill(0, 200, 0);

  // Test if the cursor is over the box 
  if (mouseX > bx-boxSize && mouseX < bx+boxSize && 
      mouseY > by-boxSize && mouseY < by+boxSize) {
    overBox = true;  
    if(!locked) { 
      stroke(0, 255, 0); 
      fill(0, 255, 0);
    } 
  } else {
    stroke(0, 255, 0);
    fill(0, 255, 0);
    overBox = false;
  }
  
  // Draw the box
  rect(bx, by, boxSize, boxSize);
  
  //Send the OSC message with box current position
  sendOsc();
}

public void mousePressed() {
  if(overBox) { 
    locked = true; 
    fill(0, 255, 0);
  } else {
    locked = false;
  }
  xOffset = mouseX-bx; 
  yOffset = mouseY-by; 

}

public void mouseDragged() {
  if(locked) {
    bx = mouseX-xOffset; 
    by = mouseY-yOffset; 
  }
}

public void mouseReleased() {
  locked = false;
}


public void sendOsc() {
  OscMessage msg = new OscMessage("/wek/inputs");
  msg.add((float)bx); 
  msg.add((float)by);
  oscP5.send(msg, dest);
}
  public void settings() {  size(640, 480, P2D);  smooth(); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "Simple_Mouse_DraggedObject_2Inputs" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}

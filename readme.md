# Feature Extraction Project - Week 5

Stacia Near
Jonathan Meade  
CSCI-4830  
Dr. Ben Shapiro  

## Project Goals

The goal of our project was to combine regression and classification algorithims into one package and utilize them in order to pinpoint the location of an individual's gaze along
with the general direction they are looking. We then take this data and pipe it into an output system we made using the 3D-modeling tool Blender in order to visualize the 
user's eyes and their movement. 

## Tools and Libraries Used
* [Tobii Eye Tracking](https://www.tobii.com) Bar and associated API - data gathering
* [SharpOSC](https://github.com/ValdemarOrn/SharpOSC) - communication
* [Blender](https://www.blender.org/) - modeling

## ML Choices

For the classification side of our project, we decided to use a KNN algorithim. We chose this due to the fact that the analysis of our data centers around 
the actual locations that the user is looking at on the "plane" of the flat screen. 

## Architecture

The Tobii Eye Tracking Bar provides a low-level API which we were able to interface with. We took the data from the streams that the Tobii API provided
and packaged it up into two seperate OSC messages, which were then sent to two seperate instances of Wekinator, one running a classification and one running a regression
algorithim. The regression algorithim then forwarded its data onto our Blender model, which used an OSC receiver package to know where to look. The classification data was sent to 
a seperate Python script, which displayed the general region that the user was looking at. 

## Accomplishments

## Technical and Creative Challenges

We faced several technical and creative challenges along this path. One of our first issues was figuring out how to interface with the Tobii hardware. Fortunately, the makers
of Tobii provide a conveinent, if low-level, API. 

## Demo

## What We Learned


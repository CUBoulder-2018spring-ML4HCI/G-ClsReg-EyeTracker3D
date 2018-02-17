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

For the classification side of our project, we decided to use a KNN algorithim with an N value of 2. We chose this due to the fact that the analysis of our data centers around 
the actual locations that the user is looking at on the "plane" of the flat screen. The two-dimensional feature space that the points construct lends itself 
greatly to the general theory behind the KNN algorithim. We chose our N value in order to smooth out erroneous data and noise which will naturally occur when using a tool
as finnicky as the Tobii machine. 

For the regression side of our data, we chose to incorporate a linear regression model so that we could construct an accurate function to represent 
where our user's eyes are looking without having to programatically write it ourselves. The axes had a maximum and a minimum which we could configure within
Wekinator.

## Architecture

The Tobii Eye Tracking Bar provides a low-level API which we were able to interface with. We took the data from the streams that the Tobii API provided
and packaged it up into two seperate OSC messages, which were then sent to two seperate instances of Wekinator, one running a classification and one running a regression
algorithim. The regression algorithim then forwarded its data onto our Blender model, which used an OSC receiver package to know where to look. The classification data was sent to 
a seperate Python script, which displayed the general region that the user was looking at. 

![Architecture diagram](https://raw.githubusercontent.com/nearsr/EyeTracker3D/master/readme-assets/model-diagram.png)

## Accomplishments

We accomplished several lower-level acheivements in order to constuct the overall high-level ML project we have today. Several are listed here.  
* Configure Blender to receive OSC messages.
* Construct an eye/face rig in Blender to model our output.
* Build a package that can take the low-level Tobii calls and send them to Wekinator as OSC output.
* Select and train a regression algorithim using real-time eye-tracking data to pass onto Blender.
* Select and train a classification algorithim to classify the various regions that the user is looking at.

## Technical and Creative Challenges

We faced several technical and creative challenges along this path. One of our first issues was figuring out how to interface with the Tobii hardware. Fortunately, the makers
of Tobii provide a conveinent, if low-level, API. We were able to access some of the sparse documentation that the company provides and incorporate their data streams into our 
final product, giving us an accurate way to input eye-tracking data. 

## Demo

You can find our demo video [here](https://www.youtube.com/watch?v=dQw4w9WgXcQ)

## What We Learned

We learned quite a bit about eye and facial tracking from the documentation we read and videos we watched online. We also learned some about implementing tools with which
we have little to no experience and documentation on. 

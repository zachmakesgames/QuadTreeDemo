# QuadTreeDemo
An experiment in spatial partitioning via quad tree

## Forest Fire Demo
This demo uses the scenario of a forest fire. Assuming this is a simple
world where there is no wind or rain and fire only spreads within a certain
radius, this demo provides a simple model of the scenario. In this simulation,
a Green point represents a normal tree, and an Orange point represent a tree
that is on fire.

To begin the demo, click on the Build Tree Button. Then click on the Pick Random
button at least once to set a random tree on fire. Each time the Pick Random
button is pressed, another random tree will be set on fire allowing the fire to
spread from multiple locations. Then click the Ignite Neighbors button to ignite
all trees within the selected radius of already on fire trees.

The radius at which an ignited tree will ignite a nearby tree can be changed by
modifying the Spread Radius slider. The blue and red circles show what the currently
selected radius looks like. The blue circle indicates the radius around the last
randomly ignited tree at which the fire will spread. The red circle indicates the
same radius around the last position the mouse has clicked.

Clicking on the image will extinguis all trees within the currently selected radius
of the point of the mouse click (this will be indicated by the red circle).

To see how the quad tree improves the processing time of the simulation, deselecting
the Use Quad Tree check box will cause the simulation to check all nodes each update
instead of using the quad tree to narrow down results. The time taken to process an
update can be seen in the bottom right corner of the window.

The boundaries of the quad tree can be removed from the preview image by deselecting
the Draw Quads check box.

At any point, a new simulation can be started by clicking on the Build Tree button
again. This will generate a new tree and reset the simulation.

The forest fire demo populates a tree that is 1000 units wide and tall with 10,000
trees.

## TimeAttack
To better demonstrate the benefit of QuadTrees on large data sets, a time attack
demo is also provided to compare the run times between using a quad tree to
search for nearby objects, and using a nested for loop for finding nearby objects.

This panel allows the user to tweak the parameters of the test to view how different
scenarios affect the results.

To open the time attack window, click on the Open Time Attack button in the
bottom right corner of the main window.

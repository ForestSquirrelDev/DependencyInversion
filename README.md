# GenericSpaceSim v2

This project uses the same logic  as [this one](https://github.com/ForestSquirrelDev/GenericSpaceSim), but this time i've made an attempt to make code architecture a bit more clean and scalable.

https://user-images.githubusercontent.com/82777171/127158500-a50c5fb4-cec7-43f0-b617-37cb4d0d0a69.mp4

### Ship
This part of code now follows Dependency inversion principle. Instead of all core ship systems being MonoBehaviours and having to find their dependencies on their own, we now only have one MonoBehaviour that represents player ship and acts as a controller for it. Other core systems are non-MonoBehaviour C# classes and their dependencies are getting filled in constructors.

### Camera management
Here i've used some sort of Strategy design pattern (or at least i hope so).

We have a base AbstractCamera class that defines: every class derived from it will be a scriptable object and will contain a method to follow player ship. AbstractCamera does not have any fields or properties because every type of camera needs (or does not need) a set of it's own settings. All camera types are derived from this abstract class.

Why scriptable objects over pure C# classes? They can store data and methods just like C# classes and, on top of that, scriptable objects can be simply dragged into array or collection in the inspector. It allows our CameraController easily iterate between cameras and makes camera implementations independent from one-another. Because of that, creating a new camera type becomes a pure joy code-wise. 

If we had everything in one monolithic MonoBehaviour, every time we'd want to change something or add a new camera, chances are we'd create some bugs caused by lots of connections in code.

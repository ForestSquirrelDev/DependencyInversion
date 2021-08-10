

# GenericSpaceSim v2

This project uses almost the same logic as [this one](https://github.com/ForestSquirrelDev/GenericSpaceSim), but this time i've made an attempt to make code architecture a bit more clean and scalable.

https://user-images.githubusercontent.com/82777171/127158500-a50c5fb4-cec7-43f0-b617-37cb4d0d0a69.mp4

### Ship
This part of code now follows Dependency inversion principle. Instead of all core ship systems being MonoBehaviours and having to find their dependencies on their own, we now only have one MonoBehaviour that represents player ship and acts as a controller for it. Other core systems are non-MonoBehaviour C# classes and their dependencies are getting filled in constructors.

### ScriptableObject variables
This part is based entirely on this amazing [talk](https://youtu.be/raQ3iHhE_Kk) from Unite Austin 2017.

When designing a project, probably  the most common question is how to reference things and not make a mess of your code, especially when connecting you game logic and UI. One of the elegant solutions in Unity is using primitive types, wrapped in ScriptableObjects.

The idea is simple: create a ScriptableObject with a single variable, and a Serializable C# class that can store this ScriptableObject and has a single getter to return its value. Game logic component will use the Variable itself for its needs (in our case it's ship speed), and whatever script needs to know the value of it (e.g. UI) can have an unmodifiable reference of this variable.

By such manipulations we: 
- keep our code modular
- do not break encapsulation
- avoid using rigid connections between scripts

![SO](https://user-images.githubusercontent.com/82777171/128940515-376306b0-459a-4875-a2c4-094045c57190.png)

To prove the last statement, in this project i've separated code parts into different assemblies: none of them references one-another except Variables assembly - Ship uses our SO variable as a speed, and UI needs a reference of its value.

### Camera management
Here i've used some sort of **Strategy** design pattern (or at least i hope so).

We have a base AbstractCamera class that defines: every class derived from it will be a scriptable object and will contain a method to follow player ship. AbstractCamera does not have any fields or properties because every type of camera needs (or does not need) a set of its own settings. All camera types are derived from this abstract class.

Then there is a MonoBehaviour called CameraController that can contain those cameras and iterate between them in game based on input.

Why scriptable objects over pure C# classes? They can store data and methods just like C# classes and, on top of that, scriptable objects can be simply dragged into array or collection in the inspector. It allows our CameraController easily iterate between cameras and makes camera implementations independent from one-another. Because of that, creating a new camera type becomes a pure joy code-wise. 

If we had everything in one monolithic MonoBehaviour, every time we'd want to change something or add a new camera, chances are we'd create some bugs caused by lots of connections in code.

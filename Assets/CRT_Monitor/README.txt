* Shader in [OldAsset] Folder does not required install any other package
* If you wanna to use the old one, you can skip the step below

Required Package:
1. Post Processing Stack Package

Please install Required Package before enter Example Scene, or just refresh the scene after install.
Be careful Post processing does not recommend apply Effect from "Default" or "Everything" Layer, , please create a new Layer for your own Project.
And also assign the PostProcessingVolume Object to new Layer, set the mask layer under the Camera Object in Post-process Layer Component.

About CRT Safe Area:
CRT Safe Area is a Component to align your Canvas UI to correct position, you can put everything under the Margin Object like Example Scene.

* If you are using SafeArea Helper with mobile device, please put CRT Safe Area as a child of SafeArea.
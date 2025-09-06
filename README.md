# csElectricFieldSimulator

![Gif showcasing the program](https://github.com/user-attachments/assets/0718bbc4-e1bd-47b5-ad11-239344e5ab9c)

Small application I made (with the help of [@BuzzZ80](https://github.com/BuzzZ80) !) to practice some things I learned in physics class! \
Made with [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)

## Controls
With the new GUI added in 1.3.0, the controls are a bit different!

### Tools

- <img src="https://github.com/user-attachments/assets/01e300be-c6ea-41a6-b3fe-c787f014288c" width="30" height="30" border="solid 10px black"> This is the **`Tools`** button.\
  Click on it to reveal/hide all of the tools you can use!
 - <img src="https://github.com/user-attachments/assets/2369651f-2dc9-4d75-ab11-4874da1624b8" width="30" height="30" border="solid 10px black"> This is the **`Add`** button. \
   Two more will appear after you click on it. The plus button lets you spawn positive charges with a left-click and negative charges with a right-click. The minus button does the opposite.\
   If you click on a charge while in add mode, you can drag it around!

 - <img src="https://github.com/user-attachments/assets/aff078f4-682a-4b68-8201-ebfb72ef5b11" width="30" height="30" border="solid 10px black"> This is the **`Erase`** button. \
   Another button will appear after you click on this one, called "Clear." You can use it to clear your work area of all charges. \
   If you don’t want to clear everything, you can click on individual charges to remove them.
 - <img src="https://github.com/user-attachments/assets/3fe40b86-be78-4d0d-a970-d5f750b24332" width="30" height="30" border="solid 10px black"> This is the **`Edit Charge`** button. \
   Another button will appear after you click on this one, called "Set 0". Once you click on it, any charge you click on will be turned into a neutral one. To disable it, click on the **`Edit Charge`** button again. \
   To edit a charge, hover your mouse over one, and hold left-click. Moving your mouse up will add to the charge, and moving it down will subtract.
   
 - <img src="https://github.com/user-attachments/assets/36e7afaf-5ba4-4ac5-b02e-12fa7b0ba03b" width="30" height="30" border="solid 10px black"> This is the **`Drag Move`** button. \
   As the name suggests, this button allows you to drag and move around your work area! This makes it a bit more intuitive than before, so it should be easier to use.
   
 - <img src="https://github.com/user-attachments/assets/ef2be2d4-70bf-4608-be4a-2eaed6f461b9" width="30" height="30" border="solid 10px black"> This is the **`Zoom`** button. \
   Similarly to the **`Edit Charge`** button, you can hold left-click anywhere that is not on a UI element, and move your mouse left and right. \
   Moving to the right makes you zoom out, and moving to the left makes you zoom in!

### Settings

<img src="https://github.com/user-attachments/assets/a8376a9b-aab6-4c09-9670-0d0ab876efce" width="30" height="30" border="solid 10px black"> This is the **`Settings`** button. \
Click on it to reveal the settings (wow).

The **LoD box** is a rectangle drawn around all of the charges that helps boost performance by decreasing the quality outside of it.

Here’s a list of all of them and what they control:
- **Quality**: This value controls the quality inside of the LoD box.
- **LoD Quality**: This value controls the quality outside of the LoD box.
- **Probe Radius**: This value controls the radius at which probes start out around each positive charge.
- **Probes Per Charge**: This value controls the number of probes that spawn around each positive charge.
- **UI Scale**: This value is used to make the UI bigger if your display requires it.
- **Field Line Thickness**: This value changes how wide/thick the field lines are, if needed. (Note: The field lines may appear to tear at widths larger than 10 due to rendering limitations.)
- **Show Field Direction**: This is a dropdown menu with three options; Static, Animated, and None.
  - **Static** shows a static visualization of the direction of the field, with 10 arrows on each field line.
  - **Animated** shows an animated visualization of the direction of the field, moving from positive charges to negative ones.
  - **None** shows no visualization.

## Installation
Make sure you have the [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed!
1. Go to the latest release on the [releases page](https://github.com/Nasko-5/csElectricFieldSimulator/releases), and download the file corresponding to your operating system.
3. Extract the contents of the `.zip` into a folder.
4. Run the `csElectricFieldSimulator` executable.

## Building
1. Clone the repository with `git clone https://github.com/Nasko-5/csElectricFieldSimulator`.
2. Navigate to the resulting folder.
3. Run the appropriate command for your operating system:
    - Windows: `dotnet publish -r win-x64 -o ./win-x64`
    - Linux: `dotnet publish -r linux-x64 -o ./linux-x64`
    - macOS: `dotnet publish -r osx-arm64 -o ./osx-arm64`
4. The build files will be created in a folder named after the platform you specified (e.g., `win-x64`, `linux-x64`, `osx-arm64`).

## Notes

You may need the .NET SDK to build the project. If you encounter any issues, feel free to open an issue on GitHub, and I’ll work on a fix! \
\
There is a bug that makes the program freeze if you try to exit the program with the close button on your OS, and i am going to work out a fix for this sooner or later! \
but i dont think it's serious enough to delay this release.\
For now, exit the program by either closing the accompanying command prompt, or by pressing `Esc` while focused on the window.



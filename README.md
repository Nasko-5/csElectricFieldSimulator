# csElectricFieldSimulator

![Gif showcasing the program](https://github.com/user-attachments/assets/0718bbc4-e1bd-47b5-ad11-239344e5ab9c)

Small application I made to practice some things I learnt in physics class! \
Made with [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)

## Controls
### Editing
- **Right-click** to add a positive charge.
- **Left-click** to add a negative charge.
- **Right-click** on a charge to remove it.
- **Left Shift + Middle-click** to clear all charges.
- **Hold Middle-click** on top of a charge and move the mouse up/down to change its strength.
- **Hold Middle-click** on top of a charge + **Right-click** to set the charge to 0.

### Movement
- **Hold Left-click** on a charge to move it around.
- **Hold Middle-click** in empty space and move the mouse to pan around the simulation.

## Installation
1. Go to the latest release on the [releases page](https://github.com/Nasko-5/csElectricFieldSimulator/releases), and download the file corresponding to your operating system
3. Extract the contents of the `.zip` into a folder
4. Run the csElectricFieldSimulator executable

## Building
1. Clone the repository with `git clone https://github.com/Nasko-5/csElectricFieldSimulator` 
2. Navigate to the resulting folder
3. Run the appropriate command for your operating system:
    - Windows: `dotnet publish -r win-x64 -o ./win-x64`
    - Linux: `dotnet publish -r linux-x64 -o ./linux-x64`
    - macOS: `dotnet publish -r osx-arm64 -o ./osx-arm64`
4. The build files will be created in a folder named after the platform you specified (e.g., `win-x64`, `linux-x64`, `osx-arm64`)

## Note
Make sure you have at least the [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed! \
Also, Visual Studio mentioned missing files when I created this repo. If you run into any issues, feel free to open an issue, and I'll fix it!

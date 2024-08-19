# Wayfinding Study Tool with the Dual Solution Paradigm

Made in `Unity 2022.3.30f1`

[![Project Screenshot](readme-res/image-4.png)](https://www.youtube.com/watch?v=utMFn1oeuIE)

## Overview

This Unity project is intended to be a tool for conducting wayfinding studies, with extinsibility in mind for future studies and parameter tweaks.

This project includes an implementation of the Dual Solution Paradigm (DSP), a framework commonly used in wayfinding studies to analyze how individuals navigate and find their way in different environments. Please see [Running a DSP Study](#running-a-dsp-study) for more info.

## Features

- Stat collection for individual trials and studies as a whole
- Extensibility for other studies and parameter tweaking.

## Set up

1. Clone the repository:
    ```sh
    git clone https://github.com/jamespilcher/wayfinding-study-tool
    ```
2. Open the project in `Unity 2022.3.30f1` or later.

## Running a DSP Study
The 'Dual Solution' refers to having a solution that either takes a shortcut or follows a known route, which highlights two key approaches to navigation. The DSP has been utilized in the following studies:

- 
- 
- 

### Set up:
1. If necessary, define any additional parameters in `StudyConfig.cs` and implement it's logic. Most often these additional parameters will be your **indepedent variables**. The parameters will then automatically be displayed in the `DSPMainMenu` scene.
3. If disabling shadows (as shadows grant more localisation information) is desired: Edit -> Project Settings -> Quality -> Shadows -> Disable Shadows.
2. In `Assets/Code/DSP` There is a scriptable object called DSPPathsData for managing the DSP trials. This will define 1. the player spawn coordinates (based on a 13x13 grid system), and 2. the target landmark to navigate to. Configure this Scriptable Object in order to include trials which will test navigational strategy. The current values in the scriptable object should be used as an example.

### Running The study

1. Run the `DSPMainMenu` Scene and configure parameters. The parameters are set to their default values, and therefore should be defined at runtime for each participant. 
2. For each participant:
    - Define their participant ID and other parameters
    - Select Begin. This will load into the controls Training Scene.
    - Use the following KeyBindings to navigate to the correct stage of the study: 8->Training, 9->Learning, 0->TestingPhase.
    - Once Participant is happy with the controls, change scene to the Learning phase by pressing the number 9.
    - Do a set number of laps (typically 5) following the red line. At each landmark, the participant should familiarise themself with it. Once completed, change scene to the Testing phase by pressing the number 0.
    - The parcipant will complete all the trials listed in the `DSPPathsData` scriptable object. Once completed, all statistics for that participant will be collated and appended to a csv named `trialData\<StudyName\>.csv`

3. Once study is completed you will have a CSV located at `Assets/Code/DSP` containing the following fields:
- **participantID** - unique identifier for the participant
- **trialNumber** - incremented per trial per participant
- **spawnPosition** - what coordinates did the candidate spawn at, as defined in `DSPPathsData`
- **targetLandmark** - what was the target landmark, as defined in `DSPPathsData`,
- **timeTaken** time taken to find the landmark,
- **granularDistanceTravelled** - the absolute, real-world geometric distance taken,
- **ManhattanRoute** - which was route taken according to the grid system
- **ManhattanRouteDistance** - total length of this route.

### DSPMainMenu and parameters
![DSP Main Menu](readme-res/image.png)

### Training Phase 
![Training Phase](readme-res/image-1.png)

### Learning Phase
![Learning Phase](readme-res/image-2.png)

### Testing Phase
![Testing Phase](readme-res/image-3.png)

## Special thanks
Special thanks to Cayley MacArthur, Mark Hancock, Dahzhuang Teng, and The Games Institute for facilitating this project as part of my Mitacs Globalink Research Internship.


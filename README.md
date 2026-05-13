# AR Dining Assistant

AR Dining Assistant is a Unity-based augmented reality restaurant experience built for an extended reality exam project. It combines a guided onboarding flow, allergy-aware menu filtering, and AR food visualization to simulate a safer and more interactive dining workflow.

## Overview

The application is designed around a simple idea: let users register, declare dietary restrictions, and browse a menu that adapts to their profile. Supported dishes can be previewed in AR using Vuforia image targets, while unsafe items are disabled in the interface.

## Key Features

- Registration and login flow with input validation
- Dietary questionnaire for gluten, lactose, and vegan preferences
- Dynamic filtering of menu items based on allergy choices
- AR food visualization anchored to tracked markers
- Rotating 3D dish preview for a more polished presentation
- Cart-style order summary and simulated checkout flow

## Tech Stack

- Unity 6.3.8f1
- C# scripts
- Vuforia Engine 11.4.4
- Unity UI (UGUI)
- Universal Render Pipeline (URP)

## Project Structure

- [Assets/ScenaMenù.unity](Assets/ScenaMenù.unity) - main scene
- [Assets/Script/Menu.cs](Assets/Script/Menu.cs) - UI flow, allergy filtering, menu and cart logic
- [Assets/Script/RotazionePiatto.cs](Assets/Script/RotazionePiatto.cs) - continuous 3D object rotation
- [Assets/Resources/VuforiaConfiguration.asset](Assets/Resources/VuforiaConfiguration.asset) - Vuforia configuration
- [Packages/manifest.json](Packages/manifest.json) - Unity package dependencies

## Requirements

- Unity 6.3.8f1 or compatible
- Android Build Support if you want to build for mobile
- Vuforia Engine package available to Unity

## Setup

1. Clone the repository.
2. Open the project in Unity.
3. Let Unity resolve the package dependencies from [Packages/manifest.json](Packages/manifest.json).
4. If the Vuforia package is missing locally, import the correct package version used by the project.
5. Open [Assets/ScenaMenù.unity](Assets/ScenaMenù.unity) and ensure the required scene references are assigned in the Inspector.

## Build

1. Open **File > Build Settings** in Unity.
2. Select **Android** as the target platform.
3. Add the main scene to the build list.
4. Configure your player settings and build the APK.

## Notes

- The project includes a generated Vuforia license helper in [Assets/VuforiaLicense.cs](Assets/VuforiaLicense.cs).
- The repository contains a large prebuilt APK named [ArDining.apk](ArDining.apk) that can be distributed through Git LFS or as a GitHub Release asset.
- Some asset-store content is included for the demo and may not be required for a minimal build.

## Academic Context

This project was created for the Technologies for Extended Reality course exam to demonstrate AR tracking, mobile UI state management, and interactive menu filtering.

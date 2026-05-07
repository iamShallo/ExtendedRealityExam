# AR Dining Assistant - Technologies for Extended Reality Exam 🍽️📱

This project was developed for the **Technologies for Extended Reality** course exam. It explores the integration of Augmented Reality (AR) within the Food & Beverage industry to enhance user safety and environmental sustainability.

---

## 🌟 Project Vision & Motivation

The **AR Dining Assistant** is designed to solve two major issues in modern catering:
1. **Environmental Impact**: Reducing the carbon footprint by eliminating physical paper menu reprints.
2. **Health & Safety**: Providing an interactive, foolproof system to prevent accidental allergen consumption through real-time digital filtering.

---

## 🛠️ Technical Stack

* **Engine**: Unity 6 (Newest Generation LTS)
* **AR Framework**: Vuforia Engine 10.22+
* **Scripting**: C# (.NET)
* **UI System**: Unity UI (UGUI) with dynamic state management
* **Graphics**: Optimized for Unity 6's Universal Render Pipeline (URP)

---

## ⚙️ Core Technical Implementation

### 1. Mandatory Allergy Logic (Safety Layer)
The application implements a strict onboarding flow. Upon registration, users must fill out a dietary profile.
* **C# Scripting**: The system parses the user's profile and interacts with the Menu Manager.
* **Dynamic Filtering**: Prohibited items are filtered out. Buttons for unsafe dishes are set to `interactable = false` and visual shaders are applied to "grey out" the UI element.

### 2. AR Tracking & Visualization
Using **Vuforia Image Targets**, the app recognizes physical markers placed on restaurant tables.
* **Spatial Anchoring**: 3D food models are instantiated relative to the marker's pose.
* **Performance**: Leveraging **Unity 6 optimization**, assets are decimated to ensure high frame rates and low battery consumption on mobile devices.

### 3. Navigation & State Machine
The app manages 4 primary states (Canvases):
* **Access/Auth**: Input validation for user credentials.
* **Profiling**: Allergy and dietary preference collection.
* **Interactive Menu**: The core filtered menu interface.
* **Cart/Checkout**: Order summary and total calculation logic.

---

## 🔄 Workflow Logic

1.  **User Profiling**: User selects "Lactose Intolerant" or "Gluten Free".
2.  **Safety Filter**: The logic engine loops through the database, disabling any non-compliant dishes.
3.  **AR Interaction**: Scanning the menu triggers 3D previews of *only* the safe dishes.
4.  **Transaction**: User adds items to the cart and completes the simulated order.

---

## 🚀 Deployment Instructions

1.  Clone this repository.
2.  Open in **Unity 6**.
3.  **Note**: The large Vuforia engine package (`.tgz`) is excluded from this repo for optimization; Unity will automatically resolve dependencies via `manifest.json`.
4.  Build settings: Target **Android** (API 24+) or **iOS** (ARKit compatible).

---

## 📝 Academic Information

* **Course**: Technologies for Extended Reality
* **Project Goal**: Demonstrate proficiency in AR tracking, mobile optimization, and interactive UX design using the latest industry standards.
* **Developer**: Francesco Caldarelli

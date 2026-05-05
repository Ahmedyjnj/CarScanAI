# 🚗 CarScanAI

**CarScanAI** is a Graduation Project designed to detect car damage using AI and provide detailed analysis including damage severity, estimated cost, and repair recommendations.

---

## 📌 Overview

CarScanAI is a smart system that allows users to upload car images and receive an AI-powered analysis of the damage. The system evaluates the severity of the damage and estimates repair costs, while also recommending nearby repair centers.

The project is built using a clean **Onion Architecture** and integrates a **Python AI model** with an **ASP.NET Core backend**.

---

## 🚀 Features

* 🔍 **AI Damage Detection**

  * Analyze car images using AI model
  * Detect damage severity (Minor / Moderate / Severe)
  * Provide confidence score

* 💰 **Cost Estimation**

  * Calculate repair cost based on damage severity

* 🧾 **User Reports**

  * View all previous analyses (reports)
  * Track user cars and history

* 🚗 **Car Management**

  * Add and manage user cars
  * View number of owned cars

* 🛠 **Repair Centers Recommendation**

  * Suggest repair centers based on car brand
  * Provide location, contact, and details

* 🌐 **Full Stack System**

  * Backend: ASP.NET Core Web API
  * AI: Python (Flask + TensorFlow)
  * Frontend Ready: Designed to work with Web & Mobile apps

---

## 🏗 Architecture

The project follows **Onion Architecture**:

* **Domain Layer** → Core entities & business rules
* **Application Layer (BLL)** → Services & business logic
* **Infrastructure Layer (DAL)** → Database & repositories
* **Presentation Layer (API)** → Controllers & endpoints

---

## 🤖 AI Integration

* Built using Python & Flask
* Model predicts:

  * Damage type
  * Severity
  * Confidence score
* Connected to .NET backend using HTTP requests

---

## 🛠 Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* AutoMapper
* Onion Architecture
* Python
* Flask
* TensorFlow / Keras
* REST APIs

---

## 📷 How It Works

1. User uploads car image
2. Image is stored on server
3. Sent to AI model for prediction
4. AI returns:

   * Severity
   * Confidence
5. System calculates cost
6. Results + repair centers are returned to user

---

## 📊 Example Output

```json
{
  "severity": "moderate",
  "estimatedCost": 7000,
  "confidence": 0.72
}
```

---

## 📱 Frontend & Mobile

The system is designed to be integrated with:

* Web frontend (React / Angular)
* Mobile apps (Flutter)

---

## 🎯 Project Goal

To build a smart and practical system that helps users:

* Understand car damage easily
* Estimate repair costs
* Find suitable repair centers

---

## 👨‍💻 Author

**Ahmed Mahmoud**

* GitHub: https://github.com/Ahmedyjnj
* LinkedIn: https://www.linkedin.com/in/ahmed-mahmoud-b4a400225/

---

## ⭐ Notes

This project is part of a **Graduation Project** and demonstrates real-world integration between AI and backend systems using clean architecture principles.

---

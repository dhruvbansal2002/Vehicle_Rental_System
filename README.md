🚗 Vehicle Rental System
The Vehicle Rental System is a C# (.NET) application designed to manage vehicle rentals efficiently. It follows a structured Object-Oriented Programming (OOP) approach and incorporates SOLID principles and design patterns to ensure scalability, maintainability, and flexibility.

🔹 Features
✅ Vehicle Management – Add, update, and remove vehicles dynamically.
✅ Customer Management – Store customer details and rental history.
✅ Rental Service – Process bookings, calculate rental durations, and apply charges.
✅ Payment System – Handle transactions using multiple payment methods.
✅ User-Friendly Interface – Designed for easy navigation and accessibility.

🔹 Technologies Used
C# (.NET) – Core programming language
OOP Concepts – Classes, interfaces, and inheritance for structured code
Visual Studio 2022 – IDE for development


🔹 SOLID Principles Applied
✔ S – Single Responsibility Principle (SRP):
Each class handles only one responsibility.
Example:

Vehicle.cs handles vehicle-related attributes and operations.
Payment.cs is solely responsible for managing payment transactions.
✔ O – Open/Closed Principle (OCP):
The system is designed to be extendable without modifying existing code.
Example:

Payment class allows for additional payment methods (e.g., CreditCardPayment, PayPalPayment) using polymorphism.
✔ L – Liskov Substitution Principle (LSP):
Child classes can replace parent classes without breaking functionality.
Example:

Car, Bike, and Truck inherit from Vehicle, ensuring each subclass can be used interchangeably.
✔ I – Interface Segregation Principle (ISP):
Interfaces are broken down to ensure clients don’t depend on unnecessary methods.
Example:

IPaymentProcessor only contains methods related to payment processing, while IRentable focuses on rental operations.
✔ D – Dependency Inversion Principle (DIP):
High-level modules do not depend on low-level modules but on abstractions.
Example:

Dependency Injection (DI) is used for PaymentService, allowing flexibility in selecting different payment strategies.

🔹 Future Enhancements
🔸 Database Integration – Implement persistent storage using MySQL or MongoDB.
🔸 Admin Dashboard – Add an admin panel for better system management.
🔸 Online Booking System – Provide a web-based UI for customer reservations.

# 🧪 Web Service Unit Testing Framework (ASP.NET Core | NUnit | Moq)

This repository demonstrates **industry-standard controller-level unit testing** and **CI-enabled automation** practices commonly used in enterprise ASP.NET Core applications.

It is intentionally designed as a **clean, interview-ready reference project** for Senior SDET / QA Automation / Backend testing roles.
---
## 📌 Purpose

The goal of this framework is to validate **Web API controller behavior** in isolation by:

* Verifying HTTP status codes
* Validating input handling
* Ensuring correct service interactions
* Avoiding real databases or external systems

This keeps unit tests:

* ⚡ Fast
* 🧱 Isolated
* 🔁 Reliable

---

## 🎯 What This Framework Covers

✔ Controller-level unit testing using **NUnit**
✔ Dependency isolation using **Moq**
✔ Verification of service interactions
✔ HTTP response validation (200, 201, 204, 400, 404)
✔ CI automation using **GitHub Actions**

❌ Authentication, authorization, infrastructure failures are intentionally excluded (covered in integration/system testing).

---

## 🏗️ Project Structure (Industry Standard)

```
UserApi
│
├── Controllers
│   ├── BookingController.cs
│   ├── OrdersController.cs
│   ├── PaymentController.cs
│   ├── UsersController.cs
│
├── Services
│   ├── IBookingService.cs
│   ├── IOrderService.cs
│   ├── IPaymentService.cs
│   ├── IUserService.cs
│
├── Tests
│   ├── BookingControllerTest.cs
│   ├── OrdersControllerTest.cs
│   ├── PaymentControllerTest.cs
│   ├── UsersControllerTest.cs
```

👉 **One test class per controller** — a real-world best practice for readability and scalability.

---

## 🧠 Key Technologies Used

| Tool             | Purpose                      |
| ---------------- | ---------------------------- |
| NUnit            | Unit testing framework       |
| Moq              | Mocking service dependencies |
| ASP.NET Core MVC | Web API framework            |
| Coverlet         | Code coverage                |
| .NET SDK         | Runtime & build              |
| GitHub Actions   | CI automation                |

---

## ✔️ What We Unit Test

* Input validation → **400 BadRequest**
* Successful execution → **200 OK / 201 Created / 204 NoContent**
* Missing data → **404 NotFound**
* Service interaction → **Verify() call count**

---

## ❌ What We Do NOT Unit Test

These belong to **integration or system testing**:

* Authentication (401)
* Authorization (403)
* Server / infrastructure failures (500+)
* Network or gateway errors

---

## 🔧 How Unit Tests Are Written (Step-by-Step)

1️⃣ **Mock the service dependency**

```csharp
_mockService = new Mock<IBookingService>();
```

2️⃣ **Inject mock into controller**

```csharp
_controller = new BookingController(_mockService.Object);
```

3️⃣ **Setup expected behavior**

```csharp
_mockService
    .Setup(s => s.Create(It.IsAny<Booking>()))
    .Returns(booking);
```

4️⃣ **Call controller action**

```csharp
var result = await _controller.CreateBooking(booking);
```

5️⃣ **Assert HTTP response**

```csharp
Assert.That(result, Is.InstanceOf<OkObjectResult>());
```

6️⃣ **Verify service interaction**

```csharp
_mockService.Verify(s => s.Create(It.IsAny<Booking>()), Times.Once);
```

---

## 🔄 CI/CD Integration (GitHub Actions)

This project includes a **GitHub Actions CI pipeline** that automatically:

* Builds the solution
* Executes all unit tests
* Runs on **push**, **pull requests**, and **daily scheduled execution**

### CI Triggers

* ✅ Push to `main`
* ✅ Pull request to `main`
* ✅ Daily scheduled run (regression safety net)

### Why CI Matters Here

* Prevents broken controller logic from merging
* Provides fast feedback to developers
* Ensures framework reliability over time

> This same CI design directly maps to **Azure DevOps pipelines** with minor syntax differences.

---

## 🎤 Interview-Ready Explanation (Short Answer)

> “I unit test only the controller logic by mocking service dependencies using Moq.
> This ensures fast, isolated, and reliable tests without external dependencies.
> These tests are executed automatically via CI on every push, PR, and daily scheduled run.”

---

## 🔥 Common Interview Questions & Answers

### Q: Why use Moq?

**A:** To isolate controller logic and avoid real database or API calls. Moq allows replacing real services with fake implementations.

---

### Q: Why do you use `Verify()`?

**A:** To ensure the controller invokes service methods exactly as expected.

---

### Q: Why one test class per controller?

**A:** It improves readability, maintainability, and aligns with real-world project structure.

---

### Q: What is isolation in unit testing?

**A:** Testing a component independently without its real dependencies.

---

### Q: Difference between unit testing and integration testing?

**A:** Unit tests validate logic in isolation; integration tests validate interaction with real components.

---

### Q: What is the purpose of `[SetUp]`?

**A:** To initialize common objects before each test and avoid duplication.

---

### Q: Why do you use `Times.Once`?

**A:** To ensure the service method is called exactly once for valid requests.

---

### Q: Why do you use `Times.Never` in some tests?

**A:** To ensure invalid inputs do not trigger service calls.

---

### Q: Why are your test methods async?

**A:** Because controller actions are asynchronous and return `Task<IActionResult>`.

---

### Q: Why return `Task` instead of `void` in tests?

**A:** `Task` allows NUnit to await execution and correctly capture exceptions.

---

### Q: What happens if you use `async void`?

**A:** NUnit cannot reliably catch exceptions, leading to false positives.

---

### Q: Can you mock private methods?

**A:** No. Moq supports mocking interfaces and virtual methods only.

---

### Q: How do unit tests fit into CI/CD?

**A:** Unit tests run as part of the CI pipeline. If any test fails, the build fails, blocking bad code from progressing.

---

### Q: How do you test async controller methods?

**A:** Test methods are written as `async Task` and awaited so NUnit correctly captures execution and exceptions.

---

## ✅ Summary

✔ Industry-aligned controller-level unit testing
✔ Clean separation of concerns
✔ CI-enabled automation
✔ Interview-ready structure and explanations

---

📌 **This repository is intentionally built as a reference implementation for enterprise-grade Web API unit testing and CI practices.**

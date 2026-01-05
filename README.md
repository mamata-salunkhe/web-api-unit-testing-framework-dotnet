🧪 Web Service Unit Testing Framework (NUnit + Moq)
📌 Purpose of This Project

This project demonstrates controller-level unit testing for REST APIs using NUnit and Moq in ASP.NET Core.

## The goal is to validate:
API behavior
HTTP status codes
Service interactions
without calling real databases or external services.

This is a controller-level unit testing framework using NUnit and Moq.
Controllers depend on service interfaces, which I mock to isolate logic.
I validate status codes like 200, 201, 204, 400, and 404, and verify service interactions using Moq Verify.
Authentication and infrastructure errors are intentionally excluded as they belong to integration testing.

🏗️ Project Structure (Industry Standard)
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

👉 Each controller has its own test class (real-world best practice)

## 🧠 Key Technologies Used
## Tool	      Purpose
NUnit	--> Test framework

Moq	--> Mocking dependencies

ASP.NET Core MVC-->	Web API framework

coverlet-->	Code coverage

.NET SDK	-->Runtime


## ✔️ What We Test (🧪 Testing Strategy Explained Simply)

Input validation → 400 BadRequest

Successful execution → 200 OK, 201 Created, 204 NoContent

Missing data → 404 NotFound

Service interaction → Verify() call count

## ❌ What We Don’t Unit Test (👉 These belong to integration or system testing)

Authentication (401)

Authorization (403)

Server failures (500+)

Network / Gateway errors


## 🔧 How Unit Tests Are Written

1️⃣ Mock the Service

_mockService = new Mock<IBookingService>();

2️⃣ Inject Mock into Controller

_controller = new BookingController(_mockService.Object);

3️⃣ Setup Expected Behavior

_mockService
  .Setup(s => s.Create(It.IsAny<Booking>()))
  .Returns(booking);

4️⃣ Call Controller Action

var result = _controller.CreateBooking(booking);

5️⃣ Assert HTTP Response

Assert.That(result, Is.InstanceOf<OkObjectResult>());

6️⃣ Verify Service Call

_mockService.Verify(s => s.Create(It.IsAny<Booking>()), Times.Once);


🎤 Interview-Ready Explanation (Short Answer)

“I unit test only the controller logic by mocking service dependencies using Moq.
This ensures fast, isolated, and reliable tests without external dependencies.”

## 🔥 Common Interview Questions & Answers

Q: Why use Moq?

A: To isolate controller logic and avoid database or API calls.Moq allows me to replace real services with fake objects so I can test controller behavior without hitting database or external APIs.

Q: Why Verify()?

A: To ensure the controller calls the service exactly as expected.

Q: Why one test class per controller?

A: Improves readability, maintenance, and scalability.

Q: What is isolation in unit testing?

A: Isolation means testing a component independently without its real dependencies.

Q: Difference between unit test and integration test?

A: Unit tests validate logic in isolation; integration tests validate interaction with real components.

Q: What is the purpose of [SetUp]?

A: To initialize common objects before each test to avoid duplication.

Q: Why do you use Times.Once?

A: To ensure the service method is invoked exactly once for valid requests.

Q: Why did you use Times.Never in some tests?

A: To ensure invalid inputs do not trigger service calls.

Q: Why are your test methods async?

A: Because the controller actions being tested are asynchronous and return Task.

Q: Why do you return Task instead of void?

A: Task allows NUnit to await execution and correctly capture exceptions.

Q: What happens if you write async void?

A: NUnit cannot catch exceptions properly, which may lead to false positives.

Q: Can you mock private methods?

A: No, Moq supports mocking interfaces and virtual methods only.

Q: how do unit tests fit into CI/CD?

A: Unit tests are executed as part of the build pipeline.
If any test fails, the build fails, ensuring only quality code moves forward.

Q: Your controller methods are async. How do you test them?

A: Since the controller actions return Task<IActionResult>, my test methods are also async Task.
I use await to ensure NUnit correctly captures execution and exceptions.
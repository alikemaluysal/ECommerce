<p align="center">
  <a href="https://github.com/alikemaluysal/ECommerce"><img src="./images/logo.jpeg" height="125"></a>
  <h3 align="center">ECommerce Project
</h3>
  <p align="center">
    Simle E-Commerce Application
    <br />
    <a href="https://github.com/alikemaluysal/ECommerce"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/alikemaluysal/ECommerce">View Demo</a>
    · 
    <a href="https://github.com/alikemaluysal/ECommerce/issues">Report Bug</a>
    ·
    <a href="https://github.com/alikemaluysal/ECommerce/discussions">Request Feature</a>
  </p>
</p>

## 💻 About The Project

Inspired by Clean Architecture, ECommerce is a monolith project that showcases advanced development techniques. The project includes Clean Architecture, CQRS, Advanced Repository, Dynamic Querying, JWT, OTP, Google & Microsoft Auth, Role-Based Management, Distributed Caching (Redis), Logging (Serilog), Elastic Search.

### Built With

[![](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/tr-tr/dotnet/welcome)

## ⚙️ Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

- .NET 8

### Installation

1. Clone the repo
   ```sh
   git clone --recurse-submodules https://github.com/alikemaluysal/ECommerce.git
   ```
2. Configure `appsettings.json` in WebAPI.
3. Run `Update-Database` command with Package Manager Console in WebAPI to create tables in sql server.

- Run the following command to update submodules
  ```sh
   git submodule update --remote
   ```

## 🚀 Usage

1. Run example WebAPI project `dotnet run --project src\ECommerce\WebAPI`

### Analysis

1. If not, Install dotnet tool `dotnet tool restore`.
2. Run anaylsis command `dotnet roslynator analyze`

### Format

1. If not, Install dotnet tool `dotnet tool restore`.
2. Run format command `dotnet csharpier .`


## 🤝 Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the project and clone your local machine
2. Create your Feature Branch (`git checkout -b <Feature>/<AmazingFeature>'`)
3. Develop
4. Commit your Changes (`git add . && git commit -m '<SemanticCommitType>(<Scope>): <AmazingFeature>'`)
   💡 Check [Semantic Commit Messages](./docs/Semantic%20Commit%20Messages.md)
5. Push to the Branch (`git push origin <Feature>/<AmazingFeature>`)
6. Open a Pull Request


## ⚖️ License

Distributed under the MIT License. See `LICENSE` for more information.

## 📧 Contact
**Mail**: alikemaluysal4235@gmail.com

<!-- ## 🙏 Acknowledgements
- []() -->
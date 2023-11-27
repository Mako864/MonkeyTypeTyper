# MonkeyTypeTyper

MonkeyTypeTyper is a .NET application that automates typing on the website [Monkeytype](https://monkeytype.com/). It's designed to simulate human typing at a specified words per minute (WPM) rate. This tool is particularly useful for testing typing speed and for automating typing tasks.

## Features

- **Customizable Typing Speed:** Users can set their desired words per minute (WPM), allowing for flexibility in typing speed.
- **Automated Navigation:** Automatically navigates to Monkeytype and handles initial setup like cookie consent.
- **Efficient Typing Automation:** Types out words from Monkeytype accurately and consistently at the set speed.

## Prerequisites

- [.NET](https://dotnet.microsoft.com/download): Ensure you have the .NET SDK installed.
- [Google Chrome](https://www.google.com/chrome/): Required for the ChromeDriver to control the browser.
- [ChromeDriver](https://sites.google.com/a/chromium.org/chromedriver/): Must be compatible with your installed version of Google Chrome.

## Installation

Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/MonkeyTypeTyper.git
cd MonkeyTypeTyper
```

## Usage

1. **Run the Application:** Start the application by running it in your .NET environment. This can typically be done through your IDE or via the command line:

   ```bash
   dotnet run
   ```

2. **Set Desired WPM:** When prompted, enter your desired words per minute.

3. **Automated Typing:** The application will then navigate to Monkeytype and begin typing at the set speed.

## How It Works

- The program starts by asking the user for their desired typing speed in WPM.
- It calculates the delay between keystrokes based on the given WPM.
- After navigating to Monkeytype, it handles cookie permissions and then starts typing the words displayed on the website.

## Contributing

Contributions are welcome! Please feel free to submit pull requests, report bugs, and suggest features.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Disclaimer

This tool is intended for educational and testing purposes only. Please ensure you adhere to Monkeytype's terms of service when using this automation tool.

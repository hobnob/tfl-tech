# TFL Tech Test
This is the response to the TFL technical test

## Configuration

Before publishing the application the configuration file can be found in `tfl-tech/App.config`.
After publication this file can be found in the Publish folder and is called `tfl-tech.dll.config`.

Replace `APP_ID` with your Application ID and replace `YOUR_DEVELOPER_KEY` with an Application Key.

## How to Build

The application can be built from Visual Studio using either the debugger or `Build` -> `Build Solution`.

## How to Run

Open `tfl-tech.sln` in Visual Studio 2017 and select `Build` -> `Publish tfl-tech` from the top menu.

Once the build is published you should find a new folder in the root of the project called 'Publish'.
In either the Command Prompt or Power Shell change to the Publish directory and run `tfl-tech.exe` with a road as a parameter.

For example:

```
PS C:\tfl-tech\Publish> .\tfl-tech.exe A2
The status of the A2 is as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays
PS C:\tfl-tech\Publish> echo $lastexitcode
0
PS C:\tfl-tech\Publish> .\tfl-tech.exe A233
A233 is not a valid road
PS C:\tfl-tech\Publish> echo $lastexitcode
1
```

## How to Test

Open `tfl-tech.sln` in Visual Studio 2017 and select `Test` -> `Run` -> `All Tests` from the top menu. All tests should run and pass.

## Assumptions

I've made the assumption there may be cause later either for different types of output (as denoted by Views) or different functionality altogether (as denoted by Controllers).
The existence of these two things would be overkill in a small application such as this if that weren't the case, as the main program could deal with them (at the expensive of that part not being easily testable).

For simplicity the App.config isn't a secure key store, although this would likely be the case in a normal working environment.

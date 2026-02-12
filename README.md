# MusicApp

A music application demonstrating seeded random number generation for reproducible data generation.

## Features

- Generate fake music data with reproducible results
- Seeded random number generators ensure consistent output
- ASP.NET Core Razor Pages application

## Running the Application

```bash
dotnet restore
dotnet run
```

Navigate to `https://localhost:5001` or `http://localhost:5000`

## Key Concept

The application demonstrates how to use seeded Random generators to create reproducible fake data. By using the same seed values, the generated songs and reviews remain consistent across runs, with new data only being added when parameters change.

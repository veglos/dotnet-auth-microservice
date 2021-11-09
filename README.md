# An Auth Microservice with Clean Architecture

Full theoretical description in my blog's article: https://veglos.github.io/posts/dotnet-auth-microservice-with-clean-architecture/

![auth-microservice](https://veglos.github.io/assets/img/2021-11-08-dotnet-auth-microservice-with-clean-architecture/auth-microservice.jpg)

_The Auth Microservice handles the authentication and authorization of the user/client_

![auth-sequence](https://veglos.github.io/assets/img/2021-11-08-dotnet-auth-microservice-with-clean-architecture/auth-sequence.jpg)

_Sequence diagram of the process of authorization by Access Token_

## Setup

1. Start up the project with Docker
    > docker-compose up
2. Run the script **Auth.Infrastructure/Repositories/MongoDB/Scripts/initialize.js** in MongoDB to initialize the database with sample data

## Auth Microservice requests examples

1. Login
    > POST http://localhost:5000/api/Auth/Login
    ```json
    {
        "Email":"freddie@queen.com",
        "Password":"p4$$w0rd"
    }
    ```

2. Refresh Token
    > POST http://localhost:5000/api/Auth/RefreshToken
    ```json
    {
        "accessToken": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZDZjMGMzNi1jNGJkLTRjOTUtYmEzZS01N2E0YTdiNWEwOTUiLCJzY29wZSI6ImNhbl9yZWFkX3dlYXRoZXIgY2FuX2NoYW5nZV91bml0IiwibmJmIjoxNjM2NDQxNDIyLCJleHAiOjE2MzY0NDE3MjIsImlhdCI6MTYzNjQ0MTQyMiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QvIn0.KCfNmLFZStrLDY8xctBwttdpGQyultYA8OcV2cE6Z3TdE8VYV3CC3wODoFGpVIGi8kSzPG839ezCqUE5BAbMiRKUz0w4mv-wpNIhK5sy-w2OccE1DhwRYKgQ4HOYKW3hvefw8sZyhs7fjy3TIe8RjlOe9yxCkHZag2LtIj-YLhGH3QhRfCYMI9bxt_kwQwzU9sPf7qFT4rfI6vjjqa5Ynx5AknEP_imQ94w0A1AhqDZGAQPEDwDw1dBOzHmOiZ9zO87PU-qiFYLr5dBJH7um-qohcUTPvhwpNHn36vZnvzHHGe9h1HptRVKyZARKHQHDrOpp1qiH1H91KejKAlb6dg",
        "refreshToken": "FBjA9DkPmpGx2UdktjNDz6R8XcwXKufBmfP/P26G6Tw0pl+Gm4Gou2pm+BH9318RIGQXb0Ug/90H9iqbROWyJQ=="
    }
    ```

3. Sign out
    > POST http://localhost:5000/api/Auth/SignOut
    ```json
    {
        "UserId": "fd6c0c36-c4bd-4c95-ba3e-57a4a7b5a095"
    }
    ```

## Weather Microservice request examples
1. Weather Forecast
    > GET http://localhost:5050/api/WeatherForecast
    
    Use the accessToken as authorization header
    > Header: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZDZjMGMzNi1jNGJkLTRjOTUtYmEzZS01N2E0YTdiNWEwOTUiLCJzY29wZSI6ImNhbl9yZWFkX3dlYXRoZXIgY2FuX2NoYW5nZV91bml0IiwibmJmIjoxNjM2NDQxNDIyLCJleHAiOjE2MzY0NDE3MjIsImlhdCI6MTYzNjQ0MTQyMiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QvIn0.KCfNmLFZStrLDY8xctBwttdpGQyultYA8OcV2cE6Z3TdE8VYV3CC3wODoFGpVIGi8kSzPG839ezCqUE5BAbMiRKUz0w4mv-wpNIhK5sy-w2OccE1DhwRYKgQ4HOYKW3hvefw8sZyhs7fjy3TIe8RjlOe9yxCkHZag2LtIj-YLhGH3QhRfCYMI9bxt_kwQwzU9sPf7qFT4rfI6vjjqa5Ynx5AknEP_imQ94w0A1AhqDZGAQPEDwDw1dBOzHmOiZ9zO87PU-qiFYLr5dBJH7um-qohcUTPvhwpNHn36vZnvzHHGe9h1HptRVKyZARKHQHDrOpp1qiH1H91KejKAlb6dg


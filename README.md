# Clean Architecture

## Exceptions

Why exceptions?

2 options to tell the controller about the status code:

- Dedicated Exception
- Result Object with Status

Result Object would be better, but I have seen projects where the result has been ignored. There are errors like unauthenticated or forbidden where we absolutely don't want that any further buisness logic gets executed and exeption are exactly for that purpose.

## Repositories

Do we need repositories? There is no overall answer. Repositories are patterns, evaluate what problems you need to solve and check whether that pattern is suited to solve it. Testability could be one of that reasons.

## Migrations

```sh
cd CleanArchitecture.Web
dotnet ef migrations add InitialCreate --project ../CleanArchitecture.Infrastructure
```

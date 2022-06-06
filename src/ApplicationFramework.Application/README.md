# Application Framework Application

![ApplicationFramework](../../logo.png)

This package is part of the Service Application Framework that consists of the following components:

* AppFramework.Domain
* **AppFramework.Application**
* AppFramework.Infrastructure
* AppFramework.Presentation

## Exceptions

You can use in you application layer the following exceptions provided by the ApplicationFramework.

- ArgumentValueException
- NotAllowedException
- NotFoundException
- ValidationException

If you need to create a CustomException just inherit from ApplicationException

## Behaviors

### Validation

Using a FluentValidator Validator class over a Command or Query by convention the ApplicationFramework will handle the error properly.

### Logging

Al queries and commands are logged using LoggingBehaviour.


---

<sub>[Hexagon Cog](https://thenounproject.com/icon/hexagon-cog-955835/) by [Tresnatiq](https://thenounproject.com/tresnatiq/) from [the Noun Project](https://thenounproject.com/) </sub>
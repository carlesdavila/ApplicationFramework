# Application Framework Infrastructure

![ApplicationFramework](../../logo.png)

This package is part of the Service Application Framework that consists of the following components:

* AppFramework.Domain
* AppFramework.Application
* **AppFramework.Infrastructure**
* AppFramework.Presentation

## HttpClient

In your HttpClient Typed Client registration you can take advantage of the following Polly policies:

- TimeoutPolicy
- CircuitBreakerPolicy
- RetryPolicy
- DefaultResilienceWrapPolicy: Policy Wrapper that wraps previous policies

So in your services configuration

```c#
// Register ISomeService as Typed Client with DefaultResilienceWrapPolicy
services.AddHttpClient<ISomeService, SomeService>()
.AddResiliencePoliciesHandler()

// Register ISomeService as Typed Client with TimeoutPolicy
services.AddHttpClient<ISomeService, SomeService>()
.AddTimeoutPolicyHandler()

// Register ISomeService as Typed Client with CircuitBreakerPolicy
services.AddHttpClient<ISomeService, SomeService>()
.AddCircuitBreakerPolicyHandler()

// Register ISomeService as Typed Client with RetryPolicy
services.AddHttpClient<ISomeService, SomeService>()
.AddRetryPolicyHandler()
```

## Data Encryption

Application Framework contains a set of EF Core extensions in order to encrypt string properties from entities.

### Services DI Configuration
EnableDbEncryption will add to DI Container an AESEncryptor that will be used to encrypt the data.

```c#
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.EnableDbEncryption("YourEncryptionKey");
}
```
> You can write your own provider that must derive from IEncryptionProvider and use it instead

### EntityConfiguration
Add IEncryptionProvider dependency to your DBContext

```c#
public class YourDbContext : IDbContext
{
    private readonly IEncryptionProvider _encryptionProvider;

    public YourDbContext(DbContextOptions<YourDbContext> options, IEncryptionProvider encryptionProvider = null)
    {
        _encryptionProvider = encryptionProvider;
    }
}
```

And use EntityTypeBuilder Extensions to configure with properties has to be encrypted

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.UseIdentityColumns();
    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    //Set Encryption to Entity property
    modelBuilder.Entity<User>().SetEncryption(u => u.Secret, _encryptionProvider);
    //Set Encryption to Owned property
    modelBuilder.Entity<User>().SetEncryption(o => o.OwnedProperty, x => x.Secret, _encryptionProvider);
}
```

---

<sub>[Hexagon Cog](https://thenounproject.com/icon/hexagon-cog-955835/) by [Tresnatiq](https://thenounproject.com/tresnatiq/) from [the Noun Project](https://thenounproject.com/) </sub>
using ServiceSample.Application.Interfaces;

namespace ServiceSample.Infrastructure.Services;

public class LicenseService: ILicenseService
{
    private readonly HttpClient _httpClient;

    public LicenseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string GetLicense()
    {
        throw new NotImplementedException();
    }
}
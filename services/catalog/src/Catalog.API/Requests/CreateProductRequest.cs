namespace Catalog.API.Requests
{
    public record CreateProductRequest(
    string Name,
    string Description,
    decimal Amount,
    string CurrencyCode,
    Guid CategoryId);

}

namespace Catalog.API.Requests
{
    public record UpdateProductPriceRequest(decimal NewAmount, string NewCurrencyCode);

}

namespace ProductsAPI.Core.Constants;

public static class Routes
{
    public static class Product
    {
        public const string Get = "GetProduct";
        public const string Create = "CreateProduct";
        public const string Update = "UpdateProduct";
        public const string Delete = "DeleteProduct";
        public const string Publish = "PublishProduct";
        public const string Hide = "HideProduct";
        public const string UpdatePriceAmount = "UpdateProductPriceAmount";
    }

    public static class Products
    {
        public const string GetAllPreviews = "GetAllProductPreviews";
        public const string GetMine = "GetMyProducts";
    }

    public static class Reviews
    {
        public const string GetByProduct = "GetProductReviews";
        public const string GetMine = "GetMyReviews";
        public const string Create = "CreateReview";
    }
}
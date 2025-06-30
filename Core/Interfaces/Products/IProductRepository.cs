using Core.Entities.Products;

namespace Core.Interfaces.Products
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();

        Task<IReadOnlyList<Product>> GetProductsAsync(string? Brand, string? Type, string? sort);
        Task<Product?> GetProductByIdAsync(int id);
        void AddProduct (Product product);
        void UpdateProduct (Product product);
        void DeleteProduct (Product product);
        bool ProductExists (int Id);
        Task<bool> SaveChangesAsync ();
    }
}

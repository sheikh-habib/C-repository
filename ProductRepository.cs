public class ProductRepository : IProductRepository {
    private List<Product> _products = new List<Product>();

    public IEnumerable<Product> GetAll() => _products;
    public void Add(Product product) => _products.Add(product);
}

public interface IProductRepository {
    IEnumerable<Product> GetAll();
    void Add(Product product);
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly string _connection = "Data Source=Data/TradeHub.db";

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        using var con = new SqliteConnection(_connection);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText =
        @"INSERT INTO Orders (BuyerName, Product, Quantity, Price, Status)
          VALUES ($buyer, $product, $qty, $price, 'Pending')";

        cmd.Parameters.AddWithValue("$buyer", order.BuyerName);
        cmd.Parameters.AddWithValue("$product", order.Product);
        cmd.Parameters.AddWithValue("$qty", order.Quantity);
        cmd.Parameters.AddWithValue("$price", order.Price);

        cmd.ExecuteNonQuery();
        return Ok("Order Saved");
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var list = new List<Order>();
        using var con = new SqliteConnection(_connection);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM Orders";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Order
            {
                Id = reader.GetInt32(0),
                BuyerName = reader.GetString(1),
                Product = reader.GetString(2),
                Quantity = reader.GetInt32(3),
                Price = reader.GetDecimal(4),
                Status = reader.GetString(5)
            });
        }
        return Ok(list);
    }
}

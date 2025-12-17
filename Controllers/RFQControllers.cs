[ApiController]
[Route("api/rfq")]
public class RFQController : ControllerBase
{
    private readonly string _connection = "Data Source=Data/TradeHub.db";

    [HttpPost]
    public IActionResult SubmitRFQ(RFQ rfq)
    {
        using var con = new SqliteConnection(_connection);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText =
        @"INSERT INTO RFQs (BuyerName, Product, Quantity, Message)
          VALUES ($buyer, $product, $qty, $msg)";

        cmd.Parameters.AddWithValue("$buyer", rfq.BuyerName);
        cmd.Parameters.AddWithValue("$product", rfq.Product);
        cmd.Parameters.AddWithValue("$qty", rfq.Quantity);
        cmd.Parameters.AddWithValue("$msg", rfq.Message);

        cmd.ExecuteNonQuery();
        return Ok("RFQ Submitted");
    }

    [HttpGet]
    public IActionResult GetRFQs()
    {
        var list = new List<RFQ>();
        using var con = new SqliteConnection(_connection);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM RFQs";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new RFQ
            {
                Id = reader.GetInt32(0),
                BuyerName = reader.GetString(1),
                Product = reader.GetString(2),
                Quantity = reader.GetInt32(3),
                Message = reader.GetString(4)
            });
        }
        return Ok(list);
    }
}

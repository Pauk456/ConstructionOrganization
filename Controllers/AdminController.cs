using ConstructionOrganizations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public record class SqlQueryRequest(string query);

    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteQuery([FromBody] SqlQueryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.query))
            return BadRequest("SQL запрос не указан");

        try
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = request.query;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();
            var result = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var value = reader.GetValue(i);
                    row[columnName] = value;
                }
                result.Add(row);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
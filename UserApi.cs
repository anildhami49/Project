// This would be in your Program.cs or a separate file in a .NET 7+ Minimal API project.
// Required using statements for a real project:
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Dapper; // A popular library for database interaction
// using System.Data.SqlClient; // Or Npgsql for PostgreSQL, etc.

var builder = WebApplication.CreateBuilder(args);

// --- Service Configuration ---

// Add services to the container.
// In a real app, you would configure your database connection here.
// For example: builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// --- Middleware Pipeline ---
app.UseCors();


// --- API Endpoints ---

// This is a sample in-memory list to simulate a database.
// In a real app, the methods below would query an actual database.
var sampleUsers = new List<User>
{
    new User(1, "Alice Smith", "alice@example.com"),
    new User(2, "Bob Johnson", "bob@example.com")
};
var nextId = 3;

// GET /users - Endpoint to retrieve all users
app.MapGet("/users", () => {
    // In a real app, this would be:
    // var users = await dbConnection.QueryAsync<User>("SELECT * FROM Users");
    // return Results.Ok(users);
    
    Console.WriteLine("GET /users request received.");
    return Results.Ok(sampleUsers);
});

// POST /users - Endpoint to create a new user
app.MapPost("/users", (User newUser) => {
    // In a real app, you would perform validation and then insert into the database:
    // var sql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email); SELECT CAST(SCOPE_IDENTITY() as int)";
    // var newId = await dbConnection.QuerySingleAsync<int>(sql, newUser);
    // newUser.Id = newId;
    // return Results.Created($"/users/{newUser.Id}", newUser);
    
    Console.WriteLine($"POST /users request received for Name: {newUser.Name}");
    newUser.Id = nextId++;
    sampleUsers.Add(newUser);
    return Results.Created($"/users/{newUser.Id}", newUser);
});

app.Run();


// --- Data Model ---

// This class represents the structure of a User.
// It's used for both receiving data from the frontend (JSON)
// and mapping data from the database.
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // Constructor for sample data
    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}

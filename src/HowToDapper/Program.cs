using Dapper;
using Npgsql;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("Dapper")
        .LeftJustified()
        .Color(Color.SpringGreen4));

var connStr = new NpgsqlConnectionStringBuilder
{
    Host = "localhost",
    Port = 5432,
    Username = "postgres",
    Password = "password",
}.ConnectionString;

await using var connection = new NpgsqlConnection(connStr);

// ------------
// CREATE TABLE
// ------------

const string command = """
    CREATE TABLE IF NOT EXISTS "Payments" (
        "Id" varchar(255) NOT NULL,
        "Amount" int NOT NULL,
        PRIMARY KEY ("Id")
    )
""";

AnsiConsole.Write(new Rule("[yellow]Creating payments table[/]"){ Justification = Justify.Left});

await connection.ExecuteAsync(command);

AnsiConsole.Write(new Table().AddColumn("Id").AddColumn("Amount").AddRow("...", "..."));

// ---------------
// INSERT PAYMENTS
// ---------------

const string insertCommand = """
    INSERT INTO "Payments" ("Id", "Amount")
    VALUES (@Id, @Amount)
""";

AnsiConsole.Write(new Rule("[yellow]Adding a couple of payments[/]"){ Justification = Justify.Left});

for (var i = 0; i < 2; i++)
{
    var payment = new Payment(Guid.NewGuid().ToString(), i * 10);
    Console.WriteLine(payment);
    await connection.ExecuteAsync(insertCommand, payment);
}

// -------------------
// SELECT ALL PAYMENTS
// -------------------

const string queryAll = """
    SELECT * FROM "Payments"
""";

AnsiConsole.Write(new Rule("[yellow]Fetching all payments[/]"){ Justification = Justify.Left});

var payments = (await connection.QueryAsync<Payment>(queryAll)).AsList();
foreach (var payment in payments)
{
    Console.WriteLine(payment);
}

// ----------------
// SELECT 1 PAYMENT
// ----------------

const string query = """
    SELECT * FROM "Payments" WHERE "Id" = @id
""";

var id = payments.First().Id;

AnsiConsole.Write(new Rule("[yellow]Fetching a single payment[/]"){ Justification = Justify.Left});
var singlePayment = await connection.QuerySingleOrDefaultAsync<Payment>(query, new { id });
AnsiConsole.MarkupLine($"{singlePayment}");

// -------------
// TABLE CLEANUP
// -------------
AnsiConsole.Write(new Rule("[yellow]Clean-up the mess[/]"){ Justification = Justify.Left});

const string deleteCommand = """
    DELETE FROM "Payments"
""";

if (AnsiConsole.Confirm("Delete all payments?"))
{
    AnsiConsole.MarkupLine("Deleting payments...");
    await connection.ExecuteAsync(deleteCommand);
}

AnsiConsole.MarkupLine("\nBye 👋");

internal record Payment(string Id, int Amount);

using Npgsql;

namespace ProjectOfDataBases
{

    class person {
        public int personId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int addressId { get; set; }
        public bool passenger { get; set; }
        public bool employee { get; set; }
        public int TelNo { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=DataBasesProject;";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Add Data");
                Console.WriteLine("2. Update Data");
                Console.WriteLine("3. Delete Data");
                Console.WriteLine("4. Show Data");
                Console.WriteLine("5. Exit");
                string choice = Console.ReadLine();

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open(); // Attempt to open the connection
                        Console.WriteLine("Connection successful.");

                        switch (choice)
                        {
                            case "1":
                                AddData(conn);
                                break;
                            case "2":
                                UpdateData(conn);
                                break;
                            case "3":
                                DeleteData(conn);
                                break;
                            case "4":
                                ShowData(conn);
                                break;
                            case "5":
                                return;
                            default:
                                Console.WriteLine("Invalid choice! Try again.");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection failed: {ex.Message}");
                }
                Console.ReadKey();
            }
        }

        static void AddData(NpgsqlConnection conn)
        {
            try
            {
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter surname:");
                string surname = Console.ReadLine();
                Console.WriteLine("Enter phone number (Tel no):");
                int telNo = Convert.ToInt32(Console.ReadLine());

                string query = "INSERT INTO person (\"name\", \"surname\", \"Tel no\") VALUES (@name, @surname, @telNo)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("surname", surname);
                    cmd.Parameters.AddWithValue("telNo", telNo);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateData(NpgsqlConnection conn)
        {
            try
            {
                Console.WriteLine("Enter person ID to update:");
                int personId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter new surname:");
                string surname = Console.ReadLine();

                string query = "UPDATE person SET \"name\" = @name, \"surname\" = @surname WHERE \"personId\" = @personId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("surname", surname);
                    cmd.Parameters.AddWithValue("personId", personId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Data updated successfully.");
                    else
                        Console.WriteLine("Person ID not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteData(NpgsqlConnection conn)
        {
            try
            {
                Console.WriteLine("Enter person ID to delete:");
                int personId = int.Parse(Console.ReadLine());

                string query = "DELETE FROM person WHERE \"personId\" = @personId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("personId", personId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Data deleted successfully.");
                    else
                        Console.WriteLine("Person ID not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ShowData(NpgsqlConnection conn)
        {
            try
            {
                string query = "SELECT * FROM person";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            person p = new person();
                            p.personId = reader.GetInt32(0);
                            p.name = reader.GetString(1);
                            p.surname = reader.GetString(2);
                            p.addressId = reader.GetInt32(3);
                            p.passenger = reader.GetBoolean(4);
                            p.employee = reader.GetBoolean(5);
                            p.TelNo = reader.GetInt32(6);

                            Console.WriteLine($"Person ID: {p.personId}, Name: {p.name} {p.surname}, " +
                                $"address: {p.addressId}, passenger: {p.passenger}, employee {p.employee}, Tel No: {p.TelNo}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

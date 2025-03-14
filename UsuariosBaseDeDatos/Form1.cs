using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace UsuariosBaseDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SQLitePCL.Batteries.Init();

            _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }

        private IConfiguration _config;

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // en https://www.connectionstrings.com pueden ver los distintos tipos de connection strings

            //string connectionString = "Data Source=SecretsDataBase.db;"; //este connection string hardcodeado es funcional pero no recomendado por ser objetivo de vulnerabilidades, en su lugar utilizamos el appsettings que contiene el connection string
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = $"SELECT * FROM USERS WHERE USERNAME = '{username}' AND USERPASS = '{password}'"; // 1 - Forma vulnerable
                //string query = "SELECT * FROM USERS WHERE USERNAME = @username AND USERPASS = @password";      // 2 - Usando parametros para evitar la inyeccion. Utilizando otras bases de datos se pueden implementar procedimientos almacenados como medida extra de seguridad, como lo vimos en el aula utilizando PostgreSQL.

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    //DESCOMENTAR PARA EL PUNTO 2
                    //command.Parameters.AddWithValue("@username", username);
                    //command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        connection.Open();
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            using (Result modal = new Result())
                            {
                                if (reader.Read())
                                {
                                    string retrievedUsername = reader.GetString(0);
                                    string retrievedPassword = reader.GetString(1);
                                    string retrievedSecret = reader.GetString(2);

                                    User user = new User(retrievedUsername, retrievedPassword, retrievedSecret);

                                    modal.labelUserName.Text = "Bienvenid@ " + user.GetUserName() + ". Su secreto bien guardado es:";
                                    modal.labelSecret.Text = user.GetSecret();
                                }
                                else
                                {
                                    modal.labelUserName.Text = "ERROR";
                                    modal.labelSecret.Text = "Usuario o password incorrecto.";
                                }
                                modal.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
                    }
                }
            }
        }



            private void Form1_Load(object sender, EventArgs e) // utilizado el load de la app para crear la bdd y agregar los usuarios
            {
            //string connectionString = "Data Source=SecretsDataBase.db;";               // 1 - Forma vulnerable
            string connectionString = _config.GetConnectionString("DefaultConnection"); // 2 - Usando appsettings
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS USERS (
                    USERNAME TEXT PRIMARY KEY,
                    USERPASS TEXT NOT NULL,
                    SECRET TEXT NOT NULL
                );";

                using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string countQuery = "SELECT COUNT(*) FROM USERS;";
                using (SqliteCommand countCommand = new SqliteCommand(countQuery, connection))
                {
                    int count = Convert.ToInt32(countCommand.ExecuteScalar());
                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO USERS (USERNAME, USERPASS, SECRET) VALUES 
                        ('pablo', 'pass123', 'Hace 7 años que estoy enganchado de la luz.'),
                        ('juan', 'javascript666', 'No me gusta programar, pero lo hago por la fama y gloria.'),
                        ('rosa', 'holasoyrosa', 'Le pongo azúcar al pastel de papa.');
                        ";

                        using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Base de datos creada y usuarios insertados.");
                    }
                }
            }
        }


    }
 }
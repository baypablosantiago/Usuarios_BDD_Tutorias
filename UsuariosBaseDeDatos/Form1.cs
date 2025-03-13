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

            //_configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.Build();
        }

        //private IConfiguration _configuration;

        private void Form1_Load(object sender, EventArgs e) //si no existe, se crea la bdd y se agregan los users
        {
            string connectionString = "Data Source=FormsDataBase.db;";
            
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
                        List<User> userList = new List<User>
                        {
                            new User("pablo", "pass123", "Hace 7 años que estoy enganchado de la luz."),
                            new User("juan", "password", "No me gusta programar, pero lo hago por la fama y gloria."),
                            new User("rosa", "12345", "Le pongo azúcar al pastel de papa.")
                        };

                        foreach (var user in userList)
                        {
                            string insertQuery = $"INSERT INTO USERS (USERNAME, USERPASS, SECRET) VALUES ('{user.GetUserName()}', '{user.GetPassword()}', '{user.GetSecret()}');";
                            using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
                            {
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Base de datos creada y usuarios insertados.");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////ACA SIN USAR LA BASE DE DATOS, SOLAMENTE HACIENDO QUERY EN LA LISTA HARDCODEADA
            //string name = textBox1.Text;
            //string pass = textBox2.Text;

            //User user = (from u in userList where u.GetUserName() == name select u).FirstOrDefault();

            //using (Result modal = new Result())
            //{
            //    if (user == null)
            //    {
            //        modal.labelUserName.Text = "ERROR";
            //        modal.labelSecret.Text = "Usuario inexistente.";
            //    }
            //    else if (!user.VerifyPassword(pass))
            //    {
            //        modal.labelUserName.Text = "ERROR";
            //        modal.labelSecret.Text = "Password incorrecta.";
            //    }
            //    else
            //    {
            //        modal.labelUserName.Text = "Bienvenid@ " + user.GetUserName() + ". Su secreto bien guardado es:";
            //        modal.labelSecret.Text = user.GetSecret();
            //    }
            //    modal.ShowDialog();
            //}


            //ACA CON BASE DE DATOS

            string username = textBox1.Text;
            string password = textBox2.Text;

            // en https://www.connectionstrings.com pueden ver los distintos tipos de connection strings
            string connectionString = "Data Source=FormsDataBase.db;";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                // Consulta vulnerable a SQL Injection
                string query = $"SELECT * FROM USERS WHERE USERNAME = '{username}' AND USERPASS = '{password}'";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
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
    }
}



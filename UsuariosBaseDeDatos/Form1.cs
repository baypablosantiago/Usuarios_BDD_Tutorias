using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Diagnostics.Eventing.Reader;
using System.IO;
namespace UsuariosBaseDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }

        private IConfiguration _configuration;

        List<User> userList = new List<User>
        {
            new User("pablo","pass123","estoy enganchado de la luz hace 7 años"),
            new User("juan","password","no me gusta programar, solamente quiero plata"),
            new User("rosa","12345","le pongo azucar al pastel de papa")
        };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string name = textBox1.Text;
            //string pass = textBox2.Text;

            //User user = (from u in userList where u.GetUserName() == name select u).FirstOrDefault();

            //using (Result modal = new Result())
            //{
            //    if (user == null)
            //    {
            //        modal.labelSecret.Text = "Usuario inexistente.";
            //    }
            //    else if (!user.VerifyPassword(pass))
            //    {
            //        modal.labelSecret.Text = "Password incorrecta.";
            //    }
            //    else
            //    {
            //        modal.labelSecret.Text = user.GetSecret();
            //    }
            //    modal.ShowDialog();
            //}


            //ACA CON BASE DE DATOS

            string username = textBox1.Text;
            string password = textBox2.Text;


            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = $"SELECT COUNT(*) FROM USERS WHERE USERNAME = '{username}' AND USERPASS = '{password}'";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@Username", username);
                    //command.Parameters.AddWithValue("@UserPass", password);

                    try
                    {
                        connection.Open();

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Login exitoso");
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos");
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



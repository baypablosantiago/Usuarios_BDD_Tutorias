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
            //_configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.Build();
        }

        private IConfiguration _configuration;

        List<User> userList = new List<User>
        {
            new User("pablo","pass123","Hace 7 años que estoy enganchado de la luz."),
            new User("juan","password","No me gusta programar, pero lo hago por la fama y gloria."),
            new User("rosa","12345","Le pongo azucar al pastel de papa.")
        };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string pass = textBox2.Text;

            User user = (from u in userList where u.GetUserName() == name select u).FirstOrDefault();

            using (Result modal = new Result())
            {
                if (user == null)
                {
                    modal.labelUserName.Text = "ERROR";
                    modal.labelSecret.Text = "Usuario inexistente.";
                }
                else if (!user.VerifyPassword(pass))
                {
                    modal.labelUserName.Text = "ERROR";
                    modal.labelSecret.Text = "Password incorrecta.";
                }
                else
                {
                    modal.labelUserName.Text = "Bienvenid@ " + user.GetUserName() + ". Su secreto bien guardado es:";
                    modal.labelSecret.Text = user.GetSecret();
                }
                modal.ShowDialog();
            }


            //ACA CON BASE DE DATOS

            //string username = textBox1.Text;
            //string password = textBox2.Text;

            ////string connectionString = "Host=localhost;Port=5432;Database=FormsDataBase;Username=postgres;Password=tup";
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");

            //using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            //{
            //    string query = $"SELECT * FROM USERS WHERE USERNAME = '{username}' AND USERPASS = '{password}'";
            //    //string query = $"SELECT * FROM authenticate_user('{username}', '{password}')";
            //    //string query = "SELECT * FROM authenticate_user(@username, @password)";
            //    //string query = "SELECT * FROM USERS WHERE USERNAME = @username AND USERPASS = @password";
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
                    
            //        try
            //        {
            //            connection.Open();

            //            using (NpgsqlDataReader reader = command.ExecuteReader())
            //            {
            //                using (Result modal = new Result())
            //                {
            //                    if (reader.Read())
            //                    {
            //                        string retrievedUsername = reader.GetString(0);
            //                        string retrievedPassword = reader.GetString(1);
            //                        string retrievedSecret = reader.GetString(2);

            //                        User user = new User(retrievedUsername, retrievedPassword, retrievedSecret);

            //                        modal.labelUserName.Text = "Bienvenid@ " + user.GetUserName() + ". Su secreto bien guardado es:";
            //                        modal.labelSecret.Text = user.GetSecret();
            //                    }
            //                    else
            //                    {
            //                        modal.labelUserName.Text = "ERROR";
            //                        modal.labelSecret.Text = "Usuario o password incorrecto.";
            //                    }
            //                    modal.ShowDialog();
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            //        }
            //    }
            //}





        }
    }
}



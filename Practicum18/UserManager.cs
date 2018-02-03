using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practicum18
{
    class UserManager
    {
        public UserManager()
        {
            Users = new List<User>();
            UserLoggedIn = false;
        }




        private const string userPath = "Users.Txt";
        public List<User> Users { get; set; }
        private bool UserLoggedIn { get; set; }

        public User Login(string login, string pw)
        {
            var okUser = Users.FirstOrDefault(u => u.Login == login && u.Paswoord == pw);
            UserLoggedIn = true;
            return okUser;
        }

        public void LogOut()
        {
            UserLoggedIn = false;
        }


        public UserState AddUser(User user)
        {

            if (HasSpecialCharacters(user.Login)) return UserState.IllegalLogin;


            if (!UserExists(user))
            {

                if (File.Exists(userPath))
                {
                    FileStream fs = new FileStream(userPath, FileMode.Append, FileAccess.Write);
                    Users.Add(user);

                    if (fs.CanWrite)
                    {
                        byte[] arrayToFile = Encoding.ASCII.GetBytes(Users.Last().ToString());
                        fs.Write(arrayToFile, 0, arrayToFile.Length);
                    }

                    fs.Flush();
                    fs.Close();

                }
                else
                {
                    FileStream fs = new FileStream(userPath, FileMode.Create, FileAccess.Write);
                    Users.Add(user);
                    if (fs.CanWrite)
                    {
                        byte[] arrayToFile = Encoding.ASCII.GetBytes(Users.Last().ToString());
                        fs.Write(arrayToFile, 0, arrayToFile.Length);
                    }
                    fs.Flush();
                    fs.Close();
                }
                return UserState.UserCreated;
            }
            else return UserState.UserAlreadyExists
        }

        public void ContactToevoegen(User user, Contact contact)
        {

            if (File.Exists(user.ContactFileLocation))
            {
                FileStream fs = new FileStream(user.ContactFileLocation, FileMode.Append, FileAccess.Write);
                user.Contacten.Add(contact);
                if (fs.CanWrite)
                {
                    byte[] arrayToFile = Encoding.ASCII.GetBytes(contact.ToString());
                    fs.Write(arrayToFile, 0, arrayToFile.Length);
                }
                fs.Flush();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(user.ContactFileLocation, FileMode.Create, FileAccess.Write);

                user.Contacten.Add(contact);
                if (fs.CanWrite)
                {
                    byte[] arrayToFile = Encoding.ASCII.GetBytes(contact.ToString());
                    fs.Write(arrayToFile, 0, arrayToFile.Length);
                }

                fs.Flush();
                fs.Close();
            }
        }

        public void UserContacten(User user)
        {

            if (!File.Exists(user.ContactFileLocation))
            {
                return;
            }
            else
            {
                string[] alleContacten = File.ReadAllLines(user.ContactFileLocation);
                user.Contacten.Clear();
                string naam = string.Empty;
                string telefoon = string.Empty;
                string email = string.Empty;

                foreach (var contact in alleContacten)
                {
                    string[] contactenArray = contact.Split('\t');
                    naam = contactenArray[0];
                    telefoon = contactenArray[1].Substring(11);
                    email = contactenArray[2].Substring(7);

                    user.Contacten.Add(new Contact { Naam = naam, TelefoonNummer = telefoon, EmailAdress = email });
                }
            }
        }


        public void LoadUsers()
        {
            if (!File.Exists(userPath)) return;
            if (new FileInfo(userPath).Length != 0)
            {
                string[] allUsers = File.ReadAllLines(userPath);
                Users.Clear();

                foreach (var user in allUsers)
                {
                    string[] userInfo = user.Split('/');

                    Users.Add(new User { Login = userInfo[0], Paswoord = userInfo[1], ContactFileLocation = userInfo[2] });
                }
            }
            else return;
        }

        public bool UserExists(User user)
        {
            //false user does not exist
            //true user already exists
            var userExist = Users.FirstOrDefault(u => u.Login == user.Login);
            if (userExist == null) return false;
            return true;
        }

        //Function to see if Pasword has special characters
        public bool HasSpecialCharacters(string pw)
        {
            if (pw.Any(ch => !Char.IsLetterOrDigit(ch))) return true;
            else return false;
        }
    }
}

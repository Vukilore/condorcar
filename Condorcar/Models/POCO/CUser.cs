using Condorcar.Models.DAL;using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Condorcar.Models.POCO
{
    [Table("T_CUser")]
    public abstract class CUser 
    {
        //
        // Attributs de chaques utilisateurs du site (admins, passagers, conducteurs)
        //
        public int Id { get; set; }

        [Required(ErrorMessage = "Le pseudo doit être saisi et de minimum 3 caractères !"), MinLength(3), MaxLength(15)]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être saisi et de minimum 6 caractères !"), Display(Name = "Mot de passe"), MinLength(6)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Le mot de passe de confirmation doit être saisi et de minimum 6 caractères !")]
        [CompareAttribute("Password", ErrorMessage = "Les deux mots de passe rentré ne correspondent pas !")]
        [Display(Name = "Mot de passe (confirmation)")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "Le mail est incorrect")]
        [Required(ErrorMessage = "L'email est requis")]
        public string Email { get; set; }

        //
        // Méthodes pour chaque utilisateurs
        //

        /////////////////////////////////////////////////////////////////////////////////
        ///                               ENCODEMD5                                   ///
        /////////////////////////////////////////////////////////////////////////////////
        public string EncodeMD5()
        {
            string passwordSalt = "Condorcar" + Password + "DonovanMarine";
            Password = ConfirmPassword = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordSalt)));
            return Password;
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               IsRegistered                                ///
        /////////////////////////////////////////////////////////////////////////////////
        public bool IsRegistered()
        {
            DAL_CUser dal = new DAL_CUser();
            var m = dal.Get(Pseudo);
            if (m == null) return false;
            else return true;
             
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                               Register                                    ///
        /////////////////////////////////////////////////////////////////////////////////
        public void Register()
        {
            this.EncodeMD5();
            DAL_CUser dal = new DAL_CUser();
            dal.Add(this);
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///                              IsCorrectPassword                            ///
        /////////////////////////////////////////////////////////////////////////////////
        public bool IsCorrectPassword()
        {
            DAL_CUser login = new DAL_CUser();
            var tmp = login.Get(Pseudo);
            this.EncodeMD5();
            if (this.Password == tmp.Password) return true;
            else return false;
        }

       /* public static CUser Login(string _username, string _password)
        {
            DAL_CUser dal = new DAL_CUser();
            CUser tmpUser = dal.Get(_username);
            if (tmpUser == null)
            {
                return null;
            }


        }*/
    }
}
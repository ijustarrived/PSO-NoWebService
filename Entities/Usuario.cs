using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PSO.Entities
{
    [Serializable]
    public class Usuario
    {
        private const int USER_MAX_AMOUNT = 15;

        public enum TiposUsuarios
        {
            EXTERNO = 0,
            PROCESADOR,
            COORDINADOR,
            ADMINISTRADOR,
            SUPERVISOR
        }

        public Usuario()
        {
            ID = 0;

            Password = string.Empty;

            SeguroSocial = string.Empty;

            Nombre = string.Empty;

            ApellidoMaterno = string.Empty;

            ApellidoPaterno = string.Empty;

            LicenciaConducir = string.Empty;

            Celular = string.Empty;

            Tel = string.Empty;

            Direccion = string.Empty;

            Pueblo = 0;

            CodigoPostal = string.Empty;

            DireccionPost = string.Empty;

            PuebloPost = 0;

            CodigoPostalPost = string.Empty;

            FechaNacimiento = new DateTime();

            Email = string.Empty;

            Activo = false;

            Role = new Rol();
        }

        public int ID
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string SeguroSocial
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public string ApellidoPaterno
        {
            get;
            set;
        }

        public string ApellidoMaterno
        {
            get;
            set;
        }

        public string GetNombreCompleto()
        {
            return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
        }

        public string LicenciaConducir
        {
            get;
            set;
        }

        public string Celular
        {
            get;
            set;
        }

        public string Tel
        {
            get;
            set;
        }

        public string Direccion
        {
            get;
            set;
        }

        public int Pueblo
        {
            get;
            set;
        }

        public string CodigoPostal
        {
            get;
            set;
        }

        public string DireccionPost
        {
            get;
            set;
        }

        public int PuebloPost
        {
            get;
            set;
        }

        public string CodigoPostalPost
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public DateTime FechaNacimiento
        {
            get;
            set;
        }

        public Rol Role
        {
            get;
            set;
        }

        public bool Activo
        {
            get;
            set;
        }

        public static string EncryptWord(string word)
        {
            string wordHexed = string.Empty;

            //Convert string to hexa value
            for (int i = 0; i < word.Length; i++)
            {
                wordHexed += string.Format("{0:X}", Convert.ToInt32(word[i]));
            }

            //Reverse
            wordHexed = new string(wordHexed.Reverse().ToArray());

            //Save 1st and last letters
            char firstLetter = wordHexed[wordHexed.Length - 1],
            lastLetter = wordHexed[0];

            #region Swap 1st with last letter

            wordHexed = wordHexed.Remove(wordHexed.Length - 1);

            wordHexed = wordHexed.Remove(0, 1);

            wordHexed = wordHexed.Insert(0, lastLetter.ToString());

            wordHexed = wordHexed.Insert(wordHexed.Length, firstLetter.ToString());

            #endregion

            return wordHexed;
        }

        public static string DecryptWord(string wordHexed)
        {
            string wordDeHexed = string.Empty;

            //Save 1st and last letters
            char firstLetter = wordHexed[wordHexed.Length - 1],
            lastLetter = wordHexed[0];

            #region Swap 1st with last letter

            wordHexed = wordHexed.Remove(wordHexed.Length - 1);

            wordHexed = wordHexed.Remove(0, 1);

            wordHexed = wordHexed.Insert(0, lastLetter.ToString());

            wordHexed = wordHexed.Insert(wordHexed.Length, firstLetter.ToString());

            #endregion

            //Reverse
            wordHexed = new string(wordHexed.Reverse().ToArray());

            //Convert hex value to original word
            wordDeHexed = Encoding.GetEncoding("ISO-8859-1").GetString(HexStringToByte(wordHexed));

            return wordDeHexed;
        }

        /// <summary>
        /// Convert hexadecimal value to byte 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] HexStringToByte(string hex)
        {
            byte[] wordInBytes = new byte[hex.Length / 2];

            for (int i = 0; i < wordInBytes.Length; i++)
            {
                string currentHex = hex.Substring(i * 2, 2);

                wordInBytes[i] = Convert.ToByte(currentHex, 16);
            }

            return wordInBytes;
        }

        public static int GetUserMaxAmount()
        {
            return USER_MAX_AMOUNT;
        }
    }
}
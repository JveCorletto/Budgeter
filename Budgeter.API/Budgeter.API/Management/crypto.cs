namespace Budgeter.API.Management
{
    public class crypto
    {
        public static string Encrypt(string _cadenaAencriptar)
        {
            try
            {
                string result = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
                result = Convert.ToBase64String(encryted);
                return result;
            }
            catch (Exception ex)
            {
                return _cadenaAencriptar;
            }
        }
        
        public static string Decrypt(string _cadenaAdesencriptar)
        {
            try
            {
                string result = string.Empty;
                byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
                result = System.Text.Encoding.Unicode.GetString(decryted);
                return result;
            }
            catch (Exception ex)
            {
                return _cadenaAdesencriptar;
            }
        }

        public static bool VerifyPasswords(string clearPassword, string hashedPassword)
        {
            try
            {
                Encrypt(clearPassword).Equals(hashedPassword);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
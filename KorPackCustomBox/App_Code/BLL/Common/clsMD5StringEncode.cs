using System;
using System.Security.Cryptography;
using System.Text;


/// <summary>
/// Summary description for clsGlobal
/// </summary>
/// 

public class clsMD5StringEncode : IDisposable
{
    


    public clsMD5StringEncode()
    {
        

    }

    public string getMd5Hash(string input)
    {
        // Create a new instance of the MD5CryptoServiceProvider object.
        MD5 md5Hasher = MD5.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    #region IDisposable Members

    public void Dispose()
    {
        GC.SuppressFinalize(true);
        //throw new Exception("The method or operation is not implemented.");
    }

    #endregion
}

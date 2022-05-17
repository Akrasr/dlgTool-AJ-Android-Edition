using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace dlgTool
{
    static class MCEncryptHelper
    {
		public static string ToHashKey(this byte[] data)
		{
			byte[] value = null;
			using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
			{
				value = md5CryptoServiceProvider.ComputeHash(data);
			}
			return BitConverter.ToString(value).ToLower().Replace("-", string.Empty);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0041465F File Offset: 0x0041285F
		public static string ToHashKey(this string str)
		{
			return Encoding.Unicode.GetBytes(str).ToHashKey();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x004640C4 File Offset: 0x004622C4
		public static void WriteToFile(byte[] data, string key, string filePath)
		{
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				tripleDESCryptoServiceProvider.Key = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.Key.Length);
				tripleDESCryptoServiceProvider.IV = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.IV.Length);
				using (FileStream fileStream = File.Create(filePath))
				{
					using (CryptoStream cryptoStream = new CryptoStream(fileStream, tripleDESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(data, 0, data.Length);
					}
				}
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00464180 File Offset: 0x00462380
		public static byte[] EncryptData(string key, byte[] sources)
		{
			byte[] result = null;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				tripleDESCryptoServiceProvider.Key = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.Key.Length);
				tripleDESCryptoServiceProvider.IV = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.IV.Length);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(sources, 0, sources.Length);
					}
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00464244 File Offset: 0x00462444
		public static byte[] ReadFromFile(string key, string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}
			byte[] result = null;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				result = MCEncryptHelper.DecryptData(key, fileStream);
			}
			return result;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00464294 File Offset: 0x00462494
		public static byte[] DecryptData(string key, byte[] sources)
		{
			byte[] result = null;
			using (MemoryStream memoryStream = new MemoryStream(sources))
			{
				result = MCEncryptHelper.DecryptData(key, memoryStream);
			}
			return result;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x004642D8 File Offset: 0x004624D8
		public static byte[] DecryptData(string key, Stream sourceStream)
		{
			byte[] result = null;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				tripleDESCryptoServiceProvider.Key = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.Key.Length);
				tripleDESCryptoServiceProvider.IV = MCEncryptHelper.GenerateKey(key, tripleDESCryptoServiceProvider.IV.Length);
				using (CryptoStream cryptoStream = new CryptoStream(sourceStream, tripleDESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(cryptoStream))
					{
						result = binaryReader.ReadBytes((int)sourceStream.Length);
					}
				}
			}
			return result;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00464398 File Offset: 0x00462598
		public static byte[] GenerateKey(string key, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)((int)key[i % key.Length] + i & 255);
			}
			return array;
		}
	}
}

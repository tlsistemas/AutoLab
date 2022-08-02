using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoLab.Utils
{
    public static class Extensions
    {
        private const string Iv = "abcdefghijklmnop";
        private const string PublicKey = "1234567890123456";

        public static String EncryptSHA256(this String str)
        {
            if (str == null)
                str = "";
            var bytes = Encoding.Default.GetBytes(str);
            var sha = new SHA256Managed();
            bytes = sha.ComputeHash(bytes);
            var rs = Convert.ToBase64String(bytes);
            sha.Clear();
            return rs;
        }
        public static String Encrypt(this Int32 id)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes(id);
                var code = Convert.ToBase64String(bytes);
                code = code.Replace("+", "M=41=5");
                code = code.Replace("/", "B4=RR4");

                return code;
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static Int32 Decrypt(this String id)
        {
            if (String.IsNullOrEmpty(id))
                return 0;
            try
            {
                var code = id.Replace("M=41=5", "+");
                code = code.Replace("B4=RR4", "/");

                byte[] bytes = Convert.FromBase64String(code);
                return BitConverter.ToInt32(bytes, 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static String EncryptAES(this String str) { return EncryptMin(str); }
        public static String DecryptAES(this String str) { return DecryptMin(str); }
        static String CryptoAES(String str, Boolean encrypt = true)
        {
            if (String.IsNullOrEmpty(str)) return String.Empty;
            try
            {
                byte[] key = { 58, 218, 45, 12, 25, 27, 20, 46, 115, 93, 28, 163, 38, 113, 223, 125, 242, 25, 176, 145, 174, 88, 197, 30, 25, 14, 18, 219, 132, 128, 54, 211 };
                byte[] vector = { 147, 65, 192, 145, 24, 4, 114, 120, 232, 122, 222, 223, 85, 65, 111, 132 };
                ICryptoTransform encryptor, decryptor;
                UTF8Encoding encoder;

                RijndaelManaged rm = new RijndaelManaged();
                encryptor = rm.CreateEncryptor(key, vector);
                decryptor = rm.CreateDecryptor(key, vector);
                encoder = new UTF8Encoding();

                byte[] buffer;
                ICryptoTransform transform;
                if (encrypt)
                {
                    buffer = encoder.GetBytes(str);
                    transform = encryptor;
                    return Convert.ToBase64String(Transform(buffer, transform));
                }
                else
                {
                    buffer = Convert.FromBase64String(str);
                    transform = decryptor;
                    return encoder.GetString(Transform(buffer, transform));
                }
            }
            catch
            {
                return String.Empty;
            }
        }
        static byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
        public static string EncryptMin(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string DecryptMin(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static DataTable GetDataTable(this DataSet dataSet, String tableName)
        {
            return dataSet.Tables.Contains(tableName) ? dataSet.Tables[tableName] : new DataTable();
        }
        public static String GetString(this DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (FieldIsNullOrEmpty(table, columnName, rowIndex))
                return "";

            return table.Rows[rowIndex][columnName].ToString().Trim();
        }
        public static Boolean GetBoolean(this DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (FieldIsNullOrEmpty(table, columnName, rowIndex))
                return false;

            return Convert.ToBoolean(table.Rows[rowIndex][columnName]);
        }
        public static DateTime? GetDateTime(this DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (FieldIsNullOrEmpty(table, columnName, rowIndex))
                return DateTime.Now;

            return Convert.ToDateTime(table.Rows[rowIndex][columnName]);
        }
        public static Decimal? GetDecimal(this DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (FieldIsNullOrEmpty(table, columnName, rowIndex))
                return 0m;

            return Convert.ToDecimal(table.Rows[rowIndex][columnName].ToString().Replace(".", ","));
        }
        public static Int32? GetInt32(this DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (FieldIsNullOrEmpty(table, columnName, rowIndex))
                return 0;

            return Convert.ToInt32(table.Rows[rowIndex][columnName]);
        }
        static Boolean FieldIsNullOrEmpty(DataTable table, String columnName, Int32 rowIndex = 0)
        {
            if (table == null || !table.Columns.Contains(columnName) || table.Rows.Count == 0 || table.Rows[rowIndex][columnName] == null || table.Rows[rowIndex][columnName].ToString() == String.Empty)
                return true;
            return false;
        }
        public static String GetString(this DataRow row, String columnName)
        {
            if (FieldIsNullOrEmpty(row, columnName))
                return "";

            return row[columnName].ToString().Trim();
        }
        public static Boolean GetBoolean(this DataRow row, String columnName)
        {
            if (FieldIsNullOrEmpty(row, columnName))
                return false;

            return Convert.ToBoolean(row[columnName]);
        }
        public static DateTime? GetDateTime(this DataRow row, String columnName)
        {
            if (FieldIsNullOrEmpty(row, columnName))
                return null;

            return Convert.ToDateTime(row[columnName]);
        }
        public static Decimal? GetDecimal(this DataRow row, String columnName)
        {
            if (FieldIsNullOrEmpty(row, columnName))
                return 0;

            return Convert.ToDecimal(row[columnName].ToString().Replace(".", ","));
        }
        public static Int32 GetInt32(this DataRow row, String columnName)
        {
            if (FieldIsNullOrEmpty(row, columnName))
                return 0;

            return Convert.ToInt32(row[columnName]);
        }
        static Boolean FieldIsNullOrEmpty(DataRow row, String columnName)
        {
            if (row == null || row[columnName] == null || row.IsNull(columnName))
                return true;
            return false;
        }
        public static DateTime? ToDateTimeNull(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            return Convert.ToDateTime(value);
        }
        public static DateTime ToDateTime(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return DateTime.MinValue;

            return Convert.ToDateTime(value);
        }
        public static Decimal? ToDecimalNull(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            return Convert.ToDecimal(value);
        }
        public static Decimal ToDecimal(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return 0m;

            return Convert.ToDecimal(value);
        }
        public static Int32? ToInt32Null(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            return Convert.ToInt32(value);
        }
        public static Int32 ToInt32(this String value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;

            return Convert.ToInt32(value);
        }
        public static DataTable FromList(this DataTable table, List<dynamic> list)
        {
            if (list.Count == 0) return table;

            var properties = list[0].GetType().GetProperties();
            foreach (var prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (dynamic item in list)
            {
                DataRow row = table.NewRow();
                foreach (var prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static String Join(this String[] values)
        {
            return Join(values, ";", false);
        }
        public static String Join(this String[] values, String separator)
        {
            return Join(values, separator, false);
        }
        public static String Join(this String[] values, String separator, Boolean quoted)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < values.Length; i++)
            {
                var item = String.Format("{0}{1}{2}{3}", (quoted ? "'" : ""), values[i], (quoted ? "'" : ""), (i < values.Length - 1 ? separator : ""));
                sb.Append(item);
            }
            return sb.ToString();
        }
        public static String ElapsedTime(this DateTime date)
        {
            var today = DateTime.Now;
            var diff = today.Subtract(date);

            var days = diff.Days;
            if (days > 0) return String.Format("{0} (Há {1} {2})", date.ToString("dd/MMM").ToLower(), days, days > 1 ? "dias" : "dia");

            var hours = diff.Hours;
            if (hours > 0) return String.Format("{0} (Há {1} {2})", date.ToString(today.Day == date.Day ? "HH:mm" : "dd/MMM").ToLower(), hours, hours > 1 ? "horas" : "hora");

            var minutes = diff.Minutes;
            if (minutes > 0) return String.Format("{0} (Há {1} {2})", date.ToString("HH:mm").ToLower(), minutes, minutes > 1 ? "minutos" : "minuto");

            return String.Format("{0} (Agora)", date.ToString("HH:mm").ToLower());
        }
        public static String ToLongDate(this DateTime date)
        {
            var day = LongValue(Convert.ToDecimal(date.Day), false);
            var month = date.ToString("MMMM").ToUpper();
            var year = LongValue(Convert.ToDecimal(date.Year), false);

            return String.Format("{0} DE {1} DE {2}", day, month, year);
        }
        public static String ToLongValue(this Decimal value)
        {
            return LongValue(value, false);
        }
        public static String ToLongCurrency(this Decimal value)
        {
            return LongValue(value, true);
        }
        static String LongValue(Decimal value, Boolean currency)
        {
            if (value <= 0 | value >= 1000000000000000)
                return "VALOR INVÁLIDO.";
            else
            {
                string stringValue = value.ToString("000000000000000.00");
                string longValue = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    longValue += PartValue(Convert.ToDecimal(stringValue.Substring(i, 3)));
                    if (i == 0 & longValue != string.Empty)
                    {
                        if (Convert.ToInt32(stringValue.Substring(0, 3)) == 1)
                            longValue += " TRILHÃO" + ((Convert.ToDecimal(stringValue.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(stringValue.Substring(0, 3)) > 1)
                            longValue += " TRILHÕES" + ((Convert.ToDecimal(stringValue.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & longValue != string.Empty)
                    {
                        if (Convert.ToInt32(stringValue.Substring(3, 3)) == 1)
                            longValue += " BILHÃO" + ((Convert.ToDecimal(stringValue.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(stringValue.Substring(3, 3)) > 1)
                            longValue += " BILHÕES" + ((Convert.ToDecimal(stringValue.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & longValue != string.Empty)
                    {
                        if (Convert.ToInt32(stringValue.Substring(6, 3)) == 1)
                            longValue += " MILHÃO" + ((Convert.ToDecimal(stringValue.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(stringValue.Substring(6, 3)) > 1)
                            longValue += " MILHÕES" + ((Convert.ToDecimal(stringValue.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & longValue != string.Empty)
                        if (Convert.ToInt32(stringValue.Substring(9, 3)) > 0)
                            longValue += " MIL" + ((Convert.ToDecimal(stringValue.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (currency && longValue.Length > 8)
                            if (longValue.Substring(longValue.Length - 6, 6) == "BILHÃO" | longValue.Substring(longValue.Length - 6, 6) == "MILHÃO")
                                longValue += " DE";
                            else
                                if (longValue.Substring(longValue.Length - 7, 7) == "BILHÕES" | longValue.Substring(longValue.Length - 7, 7) == "MILHÕES" | longValue.Substring(longValue.Length - 8, 7) == "TRILHÕES")
                                longValue += " DE";
                            else
                                    if (longValue.Substring(longValue.Length - 8, 8) == "TRILHÕES")
                                longValue += " DE";

                        if (currency)
                            if (Convert.ToInt64(stringValue.Substring(0, 15)) == 1)
                                longValue += " REAL";
                            else if (Convert.ToInt64(stringValue.Substring(0, 15)) > 1)
                                longValue += " REAIS";

                        if (Convert.ToInt32(stringValue.Substring(16, 2)) > 0 && longValue != string.Empty)
                            longValue += currency ? " E " : " VÍRGULA ";
                    }

                    if (currency && i == 15)
                        if (Convert.ToInt32(stringValue.Substring(16, 2)) == 1)
                            longValue += " CENTAVO";
                        else if (Convert.ToInt32(stringValue.Substring(16, 2)) > 1)
                            longValue += " CENTAVOS";
                }
                return longValue;
            }
        }
        static String PartValue(Decimal value)
        {
            if (value <= 0)
                return string.Empty;
            else
            {
                string stringValue = string.Empty;
                if (value > 0 & value < 1)
                {
                    value *= 100;
                }
                string strValor = value.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) stringValue += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) stringValue += "DUZENTOS";
                else if (a == 3) stringValue += "TREZENTOS";
                else if (a == 4) stringValue += "QUATROCENTOS";
                else if (a == 5) stringValue += "QUINHENTOS";
                else if (a == 6) stringValue += "SEISCENTOS";
                else if (a == 7) stringValue += "SETECENTOS";
                else if (a == 8) stringValue += "OITOCENTOS";
                else if (a == 9) stringValue += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) stringValue += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) stringValue += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) stringValue += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) stringValue += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) stringValue += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) stringValue += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) stringValue += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) stringValue += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) stringValue += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) stringValue += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) stringValue += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) stringValue += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) stringValue += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) stringValue += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) stringValue += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) stringValue += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) stringValue += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) stringValue += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & stringValue != string.Empty) stringValue += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) stringValue += "UM";
                    else if (c == 2) stringValue += "DOIS";
                    else if (c == 3) stringValue += "TRÊS";
                    else if (c == 4) stringValue += "QUATRO";
                    else if (c == 5) stringValue += "CINCO";
                    else if (c == 6) stringValue += "SEIS";
                    else if (c == 7) stringValue += "SETE";
                    else if (c == 8) stringValue += "OITO";
                    else if (c == 9) stringValue += "NOVE";

                return stringValue;
            }
        }
        public static String ToTime(this Decimal time)
        {
            var hour = Math.Truncate(time);
            var minute = 60m * (time - hour);
            return String.Format("{0}:{1}", hour.ToString("00"), minute.ToString("00"));
        }
        public static String ToTitleCase(this String text)
        {
            if (String.IsNullOrEmpty(text)) return "";
            while (text.Contains("  "))
                text = text.Replace("  ", " ");

            var array = text.ToLower().Split(' ');
            for (var i = 0; i < array.Length; i++)
            {
                if (!String.IsNullOrEmpty(array[i]) && array[i].Length > 2)
                    array[i] = array[i].Substring(0, 1).ToUpper() + array[i].Substring(1).ToLower();
            }

            return array.Join(" ");
        }
        public static String First(this String text)
        {
            if (String.IsNullOrEmpty(text)) return "";
            var texts = text.Split(' ');
            return (texts.Length > 0) ? texts[0] : text;
        }
        public static String NoAccent(this String text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(text);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        public static String NoSpecialChars(this String text)
        {
            var regex = new Regex(@"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]");
            return regex.Replace(text, "");
        }
        public static String Substring(this String text, String startLimit, String endLimit)
        {
            var posStart = text.ToUpper().IndexOf(startLimit.ToUpper());
            var posEnd = text.ToUpper().IndexOf(endLimit.ToUpper());

            var subs = "";
            if ((posEnd - posStart) > startLimit.Length)
                subs = text.Substring(posStart, posEnd - posStart).Replace(startLimit, "").Trim();
            return subs;
        }
        public static String[] Split(this String text, String separator)
        {
            var newSeparator = (!separator.Contains("#") ? "#" : !separator.Contains("|") ? "|" : !separator.Contains("@") ? "@" : "&");
            var key = String.Format("[--{0}--]", Security.NewKey());
            text = text.Replace(newSeparator, key);
            text = text.Replace(separator, newSeparator);
            var split = text.Split(newSeparator.ToCharArray());
            for (var i = 0; i < split.Length; i++)
                split[i] = split[i].Replace(key, newSeparator);

            return split;
        }
        public static String Remove(this String text, params String[] values)
        {
            foreach (var value in values)
                text = text.Replace(value, "");
            return text;
        }
        public static String Regex(this String text, String pattern)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(text);
            if (match.Success) return match.Value;
            else return "";
        }
        public static String Coalesce(this String text, params String[] values)
        {
            if (!String.IsNullOrEmpty(text.Trim())) return text;
            foreach (var value in values)
                if (!String.IsNullOrEmpty(value.Trim())) return value;
            return "";
        }
        public static String Encode(this CookieContainer cookie)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, cookie);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        public static String Encode(this String text)
        {
            if (String.IsNullOrEmpty(text)) return "";
            byte[] bytes = new byte[text.Length];
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }
        public static String Encode(this Stream stream)
        {
            Stream InputStream = stream;
            byte[] result;
            using (var streamReader = new MemoryStream())
            {
                InputStream.CopyTo(streamReader);
                result = streamReader.ToArray();
            }
            return Convert.ToBase64String(result);
        }
        public static CookieContainer DecodeToCookie(this String base64)
        {
            if (String.IsNullOrEmpty(base64))
                return null;
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                return (CookieContainer)new BinaryFormatter().Deserialize(stream);
            }
        }
        public static String DecodeToString(this String base64)
        {
            if (String.IsNullOrEmpty(base64))
                return String.Empty;
            var bytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        public static String UrlEncode(this String value)
        {
            return System.Web.HttpUtility.UrlEncode(value);
        }
        public static String UrlDecode(this String value)
        {
            return System.Web.HttpUtility.UrlDecode(value);
        }
        public static String HtmlEncode(this String value)
        {
            return System.Web.HttpUtility.HtmlEncode(value);
        }
        public static String HtmlDecode(this String value)
        {
            return System.Web.HttpUtility.HtmlDecode(value);
        }
        public static void ForceKeepAlive(this HttpWebRequest request)
        {
            request.KeepAlive = true;
            var servicePoint = request.ServicePoint;
            var props = servicePoint.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
            props.SetValue(servicePoint, (byte)0, null);

        }
        public static String NumericOnly(this String value)
        {
            var regex = new Regex("[^0-9]");
            return regex.Replace(value, "");
        }
        public static Boolean IsNumeric(this String value)
        {
            Int32 n;
            return Int32.TryParse(value, out n);
        }

        public static String[] SeparateName(this String name)
        {
            var vector = name.Split(" ").ToList();
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i].Length <= 3)
                {
                    vector[i + 1] = vector[i] + " " + vector[i + 1];
                    vector.RemoveAt(i);
                }

            }
            if (vector.Count > 3)
            {
                String rest = "";
                for (int j = 3; j < vector.Count; j++)
                {
                    rest = rest + " " + vector[j];
                    vector[j] = "";
                }
                vector[2] = vector[2] + " " + rest;
                vector.RemoveAll(x => x == "");
            }
            if (vector.Count > 2)
            {
                vector[2] = vector[2].Replace("  ", " ");
            }
            if (vector.Count == 2)
            {
                vector.Add("");
            }
            if (vector.Count == 1)
            {
                vector.Add("");
                vector.Add("");
            }

            return vector.ToArray();
        }
        public static DateTime BrasiliaDateTime(this DateTime value) => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        public static DateTime GetUltimoDiaUtil(this DateTime value)
        {
            DateTime ultimoDiaUtil = DateTime.Now.BrasiliaDateTime();
            ultimoDiaUtil = ultimoDiaUtil.AddDays(-1);
            if (ultimoDiaUtil.DayOfWeek == DayOfWeek.Sunday)
            {
                ultimoDiaUtil = ultimoDiaUtil.AddDays(-2);
            }
            else if (ultimoDiaUtil.DayOfWeek == DayOfWeek.Saturday)
            {
                ultimoDiaUtil = ultimoDiaUtil.AddDays(-1);
            }

            return ultimoDiaUtil;
        }
    }
}

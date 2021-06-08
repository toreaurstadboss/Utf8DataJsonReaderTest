using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SystemTextJsonTestRun
{
    public static class Utf8JsonReaderSerializer
    {

        public static bool IsValidFile(string filePath)
        {
            try
            {
                string json = ReadFile(filePath);
                return !string.IsNullOrEmpty(json);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidFile(string filePath, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                string json = ReadFile(filePath);
                return !string.IsNullOrEmpty(json);
            }
            catch (JsonException err)
            {
                errorMessage = err.Message;
                return false;
            }
        }



        public static string ReadFile(string filePath)
        {         
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var jsonBytes = File.ReadAllBytes(filePath);
            var jsonSpan = jsonBytes.AsSpan();
            var json = new Utf8JsonReader(jsonSpan);
            var sb = new StringBuilder();

            while (json.Read())
            {
                if (json.TokenType == JsonTokenType.StartObject)
                {
                    sb.Append(Environment.NewLine);
                }
                else if (json.TokenType == JsonTokenType.EndObject)
                {
                    //remove last comma added 

                    sb.RemoveLast(",");

                    sb.Append(Environment.NewLine);
                }

                if (json.CurrentDepth > 0)
                {
                    for (int i = 0; i < json.CurrentDepth; i++)
                    {
                        sb.Append(" "); //space indentation
                    }
                }

                sb.Append(GetTokenRepresentation(json));


                if (json.TokenType == JsonTokenType.EndObject || json.TokenType == JsonTokenType.EndArray)
                {
                    sb.AppendLine();
                }

                if (new[] { JsonTokenType.String, JsonTokenType.Number, JsonTokenType.Null, JsonTokenType.False,
                JsonTokenType.Number, JsonTokenType.None, JsonTokenType.True }.Contains(json.TokenType))
                {
                    sb.AppendLine(",");
                }

            }

            //remove last comma for EndObject 

            sb.RemoveLast(",");

            return sb.ToString(); 


        }


        private static string GetTokenRepresentation(Utf8JsonReader json) =>
          json.TokenType switch
          {
              JsonTokenType.StartObject => $"{{{Environment.NewLine}",
              JsonTokenType.EndObject => "},",
              JsonTokenType.StartArray => $"[{Environment.NewLine}",
              JsonTokenType.EndArray => $"]",
              JsonTokenType.PropertyName => $"\"{json.GetString()}\":",
              JsonTokenType.Comment => json.GetString(),
              JsonTokenType.String => $"\"{json.GetString()}\"",
              JsonTokenType.Number => GetNumberToString(json),
              JsonTokenType.True => json.GetBoolean().ToString().ToLower(),
              JsonTokenType.False => json.GetBoolean().ToString().ToLower(),
              JsonTokenType.Null => string.Empty,
              _ => "Unknown Json token type"
          };

        //TODO: Use the Try methods of the Utf8JsonReader more than trying and failing here 

        private static string GetNumberToString(Utf8JsonReader json)
        {
            try
            {
                if (int.TryParse(json.GetInt32().ToString(), out var res))
                    return res.ToString();
            }
            catch
            {
                try
                {
                    if (float.TryParse(json.GetSingle().ToString(), out var resFloat))
                        return resFloat.ToString();
                }
                catch
                {
                    try
                    {
                        if (decimal.TryParse(json.GetDouble().ToString(), out var resDes))
                            return resDes.ToString();
                    }
                    catch
                    {
                        return "?";
                    }
                }
            }
            return $"?"; //fallback to a string if not possible to deduce the type
        }

    }
}

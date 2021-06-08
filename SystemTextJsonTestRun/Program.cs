using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SystemTextJsonTestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Utf8JsonReader sample");

            bool isValid = Utf8JsonReaderSerializer.IsValidFile("samplewitherror.json");

            bool isValidWithErrorMessage = Utf8JsonReaderSerializer.IsValidFile("samplewitherror.json", 
                out string errorMessage);
            Console.WriteLine(errorMessage);

            //string json = Utf8JsonReaderSerializer.ReadFile("sample.json");
            //string tempFile = Path.ChangeExtension(Path.GetTempFileName(), "json"); 
            //File.WriteAllText(tempFile, json);
            //Console.WriteLine($"Json file read and processed result in location: {tempFile}");
            //Console.WriteLine($"Json file contents: {Environment.NewLine}{json}");

            #region Sample implementation algorithm 

            //var jsonBytes = File.ReadAllBytes("sample.json");
            //var jsonSpan = jsonBytes.AsSpan();
            //var json = new Utf8JsonReader(jsonSpan);
            //var sb = new StringBuilder();

            //while (json.Read())
            //{             
            //    if (json.TokenType == JsonTokenType.StartObject)
            //    {
            //        sb.Append(Environment.NewLine);
            //    }   
            //    else if (json.TokenType == JsonTokenType.EndObject)
            //    {
            //        //remove last comma added 

            //        RemoveLast(sb, ",");                                      

            //        sb.Append(Environment.NewLine);
            //    }                  

            //    if (json.CurrentDepth > 0)
            //    {
            //        for (int i = 0; i < json.CurrentDepth; i++)
            //        {
            //            sb.Append(" "); //space indentation
            //        }
            //    }

            //    sb.Append(GetTokenRepresentation(json));


            //    if (json.TokenType == JsonTokenType.EndObject || json.TokenType == JsonTokenType.EndArray)
            //    {
            //        sb.AppendLine();
            //    }

            //    if (new[] { JsonTokenType.String, JsonTokenType.Number, JsonTokenType.Null, JsonTokenType.False,
            //    JsonTokenType.Number, JsonTokenType.None, JsonTokenType.True }.Contains(json.TokenType)) {
            //        sb.AppendLine(",");
            //    }

            //    //Console.WriteLine(sb.ToString());
            //    //Console.ReadKey();

            //}

            ////remove last comma for EndObject 

            //RemoveLast(sb, ","); 

            //Console.WriteLine(sb.ToString());

            #endregion


        }

        #region Sample implementation algorithm - moved to Utf8JsonReaderSerializer 
        //private static StringBuilder RemoveLast(StringBuilder sb, string value)
        //{
        //    if (sb.Length < 1) return sb;
        //    int removeIndex = sb.ToString().LastIndexOf(value);
        //    if (removeIndex < 0)
        //    {
        //        return sb;
        //    }
        //    sb.Remove(sb.ToString().LastIndexOf(value), value.Length);
        //    return sb;
        //}

        //private static string GetTokenRepresentation(Utf8JsonReader json) =>
        //    json.TokenType switch
        //    {
        //        JsonTokenType.StartObject => $"{{{Environment.NewLine}",
        //        JsonTokenType.EndObject => "},",
        //        JsonTokenType.StartArray => $"[{Environment.NewLine}",
        //        JsonTokenType.EndArray => $"]",
        //        JsonTokenType.PropertyName => $"\"{json.GetString()}\":",
        //        JsonTokenType.Comment => json.GetString(),
        //        JsonTokenType.String => $"\"{json.GetString()}\"",
        //        JsonTokenType.Number => GetNumberToString(json),
        //        JsonTokenType.True => json.GetBoolean().ToString().ToLower(),
        //        JsonTokenType.False => json.GetBoolean().ToString().ToLower(),
        //        JsonTokenType.Null => string.Empty,
        //        _ => "Unknown Json token type"
        //    };

        //private static string GetNumberToString(Utf8JsonReader json)
        //{
        //    try
        //    {
        //        if (int.TryParse(json.GetInt32().ToString(), out var res))
        //            return res.ToString();
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            if (float.TryParse(json.GetSingle().ToString(), out var resFloat))
        //                return resFloat.ToString();
        //        }
        //        catch
        //        {
        //            try
        //            {
        //                if (decimal.TryParse(json.GetDouble().ToString(), out var resDes))
        //                    return resDes.ToString();
        //            }
        //            catch
        //            {
        //                return "?";
        //            }
        //        }
        //    }         
        //    return $"?"; //fallback to a string if not possible to deduce the type
        //}

        #endregion 


    }
}

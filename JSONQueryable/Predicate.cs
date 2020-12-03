using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class only considers logical operation 'and' for the sake of POC and checks for following operators:
/// >=
/// <=
/// >
/// <
/// =
/// </summary>
namespace JSONQueryable
{
    public static class Predicate
    {
        public static bool IsMatch(string jsonObject, string filter)
        {
            var jsonInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonObject);
            var filteredResults = new Dictionary<string, Object>();

            string[] str = new string[] { "and" };

            var splitResult = filter.Split(str, StringSplitOptions.None);
            foreach (var item in splitResult)
            {
                var operatorResult = item.Split(new string[] { ">=" }, StringSplitOptions.None);
                if (operatorResult.Count() > 1)
                {
                    filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && Convert.ToInt32(c.Value) >= Convert.ToInt32(operatorResult[1].Trim())).ToDictionary(c => c.Key, c => c.Value);
                    if (jsonInput.Count == 0)
                    {
                        return false;
                    }
                    continue;
                }

                operatorResult = item.Split(new string[] { "<=" }, StringSplitOptions.None);
                if (operatorResult.Count() > 1)
                {
                    filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && Convert.ToInt32(c.Value) <= Convert.ToInt32(operatorResult[1].Trim())).ToDictionary(c => c.Key, c => c.Value);
                    if (filteredResults.Count == 0)
                    {
                        return false;
                    }
                    continue;
                }

                operatorResult = item.Split('>');
                if (operatorResult.Count() > 1)
                {
                    filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && Convert.ToInt32(c.Value) > Convert.ToInt32(operatorResult[1].Trim())).ToDictionary(c => c.Key, c => c.Value);
                    if (filteredResults.Count == 0)
                    {
                        return false;
                    }
                    continue;
                }

                operatorResult = item.Split('<');
                if (operatorResult.Count() > 1)
                {
                    filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && Convert.ToInt32(c.Value) < Convert.ToInt32(operatorResult[1].Trim())).ToDictionary(c => c.Key, c => c.Value);
                    if (filteredResults.Count == 0)
                    {
                        return false;
                    }
                    continue;
                }

                operatorResult = item.Split(new string[] { "=" }, StringSplitOptions.None);                
                if (operatorResult.Count() > 1)
                {
                    if (operatorResult[1].Trim().StartsWith("'"))
                    {
                        filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && c.Value.ToString() == operatorResult[1].Trim(new char[] {' ', '\''})).ToDictionary(c => c.Key, c => c.Value);
                    }
                    else
                    {
                        filteredResults = jsonInput.Where(c => c.Key == operatorResult[0].Trim() && Convert.ToInt32(c.Value) == Convert.ToInt32(operatorResult[1].Trim())).ToDictionary(c => c.Key, c => c.Value);
                    }

                    if (filteredResults.Count == 0)
                    {
                        return false;
                    }
                }                
            }

            if (jsonInput.Count > 0)
                return true;
            else
                return false;
        }
    }
}

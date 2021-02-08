using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DIgiOutsourceAutomation.Helpers
{
    public class Parameters
    {
        #region Enums
        public enum RenewalServices
        {
            StatusGroupDescription
        }
        public enum Test
        {
            NewReportId, TestName, ApplicationName, Country,
            MachineId
        }
        #endregion

        public static Dictionary<string, object> Variables = new Dictionary<string, object>();

        public static void SaveParameter(string key, object value)
        {
            if (Variables.ContainsKey(key.ToLower()))
            { Variables[key.ToLower()] = value; }
            else
            { Variables.Add(key.ToLower(), value); }
            Console.WriteLine("Parameter : {0} - {1}", key, value);
        }

        public static T GetParameter<T>(string key)
        {

            T value;
            if (!Variables.ContainsKey(key.ToLower()))
                throw new ArgumentException(String.Format("Error : {0} key not found, please use SaveParameter.", key.ToLower()));
            try
            {
                TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                value = (T)conv.ConvertFrom(Variables[key.ToLower()]);
            }
            catch (Exception)
            {

                value = (T)System.Convert.ChangeType(Variables[key.ToLower()], typeof(T));

            }


            return value;

        }

        public static void PrintParameters()
        {
            Console.WriteLine("Parameter List...");
            foreach (var testparam in Variables)
            {
                Console.WriteLine("Parameter : {0} - {1}", testparam.Key, testparam.Value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repower.Common.Validations.Impl;
using ScrignoV2.Business;
using ScrignoV2.Business.Entities;
using Repower.Common.Validations;
using Repower.Common.Validations.Reflection;


namespace TestValidator
{
    class Program
    {
        static void Main(string[] args)
        {

            TestValidatorManager();
            //TestDynamicSetter_V2();

        }

        public static Consultant GetConsultantDemo()
        {
            Consultant cons1 = new Consultant();
            cons1.Name = "Mirko";
            cons1.Surname = "Bonadei";
            cons1.IdentityCode = 99;
            cons1.Email = "chico_herbal-hotmail.com";

            Agency ag = new Agency();
            ag.ID = 1;
            ag.Name = "mirko";
            ag.IDManager = 100;

            return cons1;
        }

        public static Agency GetAgencyDemo()
        {
            Agency ag = new Agency();
            ag.ID = 1;
            ag.Name = "Mirkos";
            ag.IDManager = 100;
            return ag;
        }


        public static bool CheckIdentifier(Consultant cons)
        {
            return cons.IdentityCode >= 100;
        }

        public static bool CheckStringTitleCase(string name)
        {
            return name.Substring(0, 1) == name.Substring(0, 1).ToUpper();
        }

        public static bool CheckEmail(string email)
        {
            return email.Contains("@");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void TestValidatorManager()
        {
            Consultant c = GetConsultantDemo();
            Agency a = GetAgencyDemo();
            ComplexValidator<Consultant, Agency> cc = CustomValidators.GetDefaultConsAggValidator();            
            cc.ExecuteOperations(c, a);

            try
            {
                foreach (var current in cc.GetOperationResults())
                {
                    //Console.WriteLine(string.Format("TypeInfo: {0}, Message: {1}", current.TypeInfo, current.CurrentResult.Message));
                    Console.WriteLine(current.CurrentResult.ToString());
                    //Console.WriteLine(current.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error into results. message: " + ex.Message);
            }
            Console.ReadLine();
        }


        public static void TestDynamicSetter()
        {
            try
            {
                Dictionary<string, object> PropValues = new Dictionary<string, object>();
                PropValues.Add("Name", "Diego");
                PropValues.Add("Surname", "De la Vega");

                DynamicSetter<Consultant> ConsSetter = new DynamicSetter<Consultant>();
                Consultant cc = ConsSetter.SetPropertiesFrom(PropValues);
                Console.WriteLine();
                Console.WriteLine("dopo assegnazione");
                Console.WriteLine(string.Format("Name: {0}, Surname: {1}", cc.Name, cc.Surname));
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore message: " + ex.Message);
                Console.ReadLine();
            }
        }

    }
}

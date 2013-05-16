using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validations.Test.Domain;

namespace Validations.Test
{
    internal class MockDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Salesman GetRightConsultant()
        {
            Salesman cons1 = new Salesman(true);
            cons1.Name = "Mirkos";
            cons1.Surname = "Mimi";
            cons1.IdentityCode = 100;
            cons1.Email = "mimo_mimo@hotmail.com";

            cons1.AddContract(new CarContract() { BrandName = "Audi", Description = "description"});

            return cons1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Salesman GetWrongConsultant()
        {
            Salesman cons1 = new Salesman(true);
            cons1.Name = "Cons";
            cons1.Surname = null;
            cons1.IdentityCode = 70;
            cons1.Email = "chico_herbal-hotmail.com";

            return cons1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Agency GetRightAgency()
        {
            Agency ag = new Agency();
            ag.ID = 1;
            ag.Name = "Agency";
            ag.IDManager = 100;
            return ag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Agency GetWrongAgency()
        {
            Agency ag = new Agency();
            ag.ID = -1;
            ag.Name = "Cons";
            ag.IDManager = 10;
            return ag;
        }
    }
}

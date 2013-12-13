using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Validations.Exceptions;
using Validations.Impl;
using Validations.Test.Domain;

namespace Validations.Test
{
    [TestFixture]
    public class ComplexValidatorTest
    {
        private IComplexValidator<Salesman, Agency> DefaultValidator;

        [TestFixtureSetUp]
        public void OnInit()
        {
            List<IComplexOperation<Salesman, Agency>> operations = new List<IComplexOperation<Salesman, Agency>>();
            operations.Add(new ComplexOperation<Salesman, Agency>("A.Name <> C.Name", "Agency name cannot be equals to consultant name", (n, c) => !n.Name.Equals(c.Name)));
            operations.Add(new ComplexOperation<Salesman, Agency>("A.Name & C.Name title case", "Name is no title case like Agency name", (n, c) => CheckHelper.CheckStringTitleCase(c.Name) && CheckHelper.CheckIdentifier(n)));
            DefaultValidator = new ComplexValidator<Salesman, Agency>(operations);
        }

        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a complex validator, and gets successfully results only.")]
        public void ValidateTest1()
        {
            Salesman cons = new Salesman();
            cons.Name = "Juan";
            cons.Surname = "Rios";
            cons.IdentityCode = 150;
            cons.Email = "Juan_Rios@gmail.com";
            cons.AddContract(new CarContract() { BrandName = "Audi", Description = "description" });

            Agency ag = new Agency(){ID = 1, Name = "ASK"};
            var result = this.DefaultValidator.Validate(cons, ag);
            Assert.IsTrue(result.All(n => n.State == ResultState.Successfully));
        }

        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a complex validator, and gets unsuccessfully results only.")]
        public void ValidateTest2()
        {
            Salesman cons = new Salesman();
            cons.Name = "juan.ask";
            cons.Surname = "Rios";
            cons.IdentityCode = 99;
            cons.Email = "Juan_Rios@gmail.com";
            cons.AddContract(new CarContract() { BrandName = "Audi", Description = "description" });

            Agency ag = new Agency() { ID = -1, Name = "juan.ask" };

            var result = this.DefaultValidator.Validate(cons, ag);
            Assert.IsTrue(result.All(n => n.State == ResultState.Unsuccessfully));
        }

        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a complex validator, and gets a result operation with RuntimeError state.")]
        public void ValidateTest3()
        {
            Salesman cons = new Salesman();
            cons.Name = "";
            cons.Surname = null;
            cons.IdentityCode = 70;
            cons.Email = "chico_herbal-hotmail.com";

            Agency ag = new Agency(){ID = -1, Name = ""};

            var result = this.DefaultValidator.Validate(cons, ag);
            Assert.IsTrue(result.Any(n => n.State == ResultState.RuntimeError));
        }

        [Test]
        [Category("FailedComplexValidator")]
        [Description("Executes a complex validator, and throws an exception because instances to validate are null.")]
        [ExpectedException(typeof(ValidationParameterException))]
        public void FailedValidateTest1()
        {
            List<bool> cases = new List<bool>();
            try
            {
                var result = this.DefaultValidator.Validate(null, null);
                cases.Add(false);
            }
            catch (Exception)
            {
                cases.Add(true);
                throw;
            }

            try
            {
                var result = this.DefaultValidator.Validate(new Salesman(), null);
                cases.Add(false);
            }
            catch (Exception)
            {
                cases.Add(true);
                throw;
            }


            try
            {
                var result = this.DefaultValidator.Validate(null, new Agency());
                cases.Add(false);
            }
            catch (Exception)
            {
                cases.Add(true);
                throw;
            }

            Assert.Throws(typeof(ValidationParameterException), () => cases.All( n => n));
        }

        [Test]
        [Category("FailedComplexValidator")]
        [Description("Executes a complex validator, and throws an exception bacause the set of operations cannot be null.")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedValidateTest2()
        {
            IComplexValidator<Salesman, Agency> validator = new ComplexValidator<Salesman, Agency>(null);;
        }

        [Test]
        [Category("FailedComplexValidator")]
        [Description("Executes a complex validator, and throws an exception bacause the set of operations cannot be empty")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedValidateTest3()
        {
            IComplexValidator<Salesman, Agency> validator = new ComplexValidator<Salesman, Agency>(new List<IComplexOperation<Salesman, Agency>>()); ;
        }

        [Test]
        [Category("FailedComplexValidator")]
        [Description("Executes a complex validator, and throws an exception because target operations must be unique")]
        [ExpectedException(typeof(NonUniqueOperationException))]
        public void FailedValidateTest4()
        {
            var rules = new List<IComplexOperation<Salesman, Agency>>();
            rules.Add(new ComplexOperation<Salesman, Agency>("salesman.name <> agency.name", (salesman, agency) => salesman.Name != agency.Name));
            rules.Add(new ComplexOperation<Salesman, Agency>("salesman.name <> agency.name", (salesman, agency) => salesman.Name.Equals(agency.Name)));
            IComplexValidator<Salesman, Agency> validator = new ComplexValidator<Salesman, Agency>(rules);
        }
    }
}

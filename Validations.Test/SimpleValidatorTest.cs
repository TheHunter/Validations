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
    class SimpleValidatorTest
    {
        private ISimpleValidator<Salesman> DefaultValidator;
        
        [TestFixtureSetUp]
        public void OnInit()
        {
            IList<ISimpleOperation<Salesman>> operations = new List<ISimpleOperation<Salesman>>();
            operations.Add(new SimpleOperation<Salesman>("Consultant.Name", "Consultant name cannot be null or empty.", n => !string.IsNullOrEmpty(n.Name)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Surname", "Consultant Surname must be title case type.", n => CheckHelper.CheckStringTitleCase(n.Surname)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Email", "Consultant Email has a wrong format.", n => CheckHelper.CheckEmail(n.Email)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Identifier", "Consultant identifier must be great than 100.", n => CheckHelper.CheckIdentifier(n)));
            operations.Add(new SimpleOperation<Salesman>("Contracts", "no Contracts", n => n.Contracts.Any()));
            DefaultValidator = new SimpleValidator<Salesman>(operations);
        }

        [Test]
        [Category("SimpleValidator")]
        [Description("Executes a simple validator, and gets successfully results only.")]
        public void ValidateTest1()
        {
            Salesman cons = new Salesman();
            cons.Name = "Juan";
            cons.Surname = "Rios";
            cons.IdentityCode = 150;
            cons.Email = "Juan_Rios@gmail.com";
            cons.AddContract(new CarContract() { BrandName = "Audi", Description = "description" });

            var result = this.DefaultValidator.Validate(cons);
            Assert.IsTrue(result.All(n => n.State == ResultState.Successfully));
        }

        [Test]
        [Category("SimpleValidator")]
        [Description("Executes a simple validator, and gets unsuccessfully results only.")]
        public void ValidateTest2()
        {
            Salesman cons = new Salesman();
            cons.Name = "";
            cons.Surname = "lol";
            cons.IdentityCode = 99;
            cons.Email = "Juan_Rios_gmail.com";

            var result = this.DefaultValidator.Validate(cons);
            Assert.IsTrue(result.All(n => n.State == ResultState.Unsuccessfully));
        }

        [Test]
        [Category("SimpleValidator")]
        [Description("Executes a simple validator, and gets a result operation with RuntimeError state.")]
        public void ValidateTest3()
        {
            Salesman cons = new Salesman();
            cons.Name = "";
            cons.Surname = null;
            cons.IdentityCode = 70;
            cons.Email = "chico_herbal-hotmail.com";

            var result = this.DefaultValidator.Validate(cons);
            Assert.IsTrue(result.Any(n => n.State == ResultState.RuntimeError));
        }

        [Test]
        [Category("FailedSimpleValidator")]
        [Description("Executes a simple validator, and throws an exception because instance to validate is null.")]
        [ExpectedException(typeof(ValidationParameterException))]
        public void FailedValidateTest1()
        {
            this.DefaultValidator.Validate(null);   
        }

        [Test]
        [Category("FailedSimpleValidator")]
        [Description("Executes a simple validator, and throws an exception the set of operations cannot be null.")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedValidateTest2()
        {
            ISimpleValidator<Salesman> validator = new SimpleValidator<Salesman>(null);
        }

        [Test]
        [Category("FailedSimpleValidator")]
        [Description("Executes a simple validator, and throws an exception bacause the set of operations cannot be empty.")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedValidateTest3()
        {
            ISimpleValidator<Salesman> validator = new SimpleValidator<Salesman>(new List<ISimpleOperation<Salesman>>());
        }

        [Test]
        [Category("FailedSimpleValidator")]
        [Description("Executes a simple validator, and throws an exception because target operations must be unique.")]
        [ExpectedException(typeof(NonUniqueOperationException))]
        public void FailedValidateTest4()
        {
            var rules = new List<ISimpleOperation<Salesman>>();
            rules.Add(new SimpleOperation<Salesman>("name", salesman => salesman.Name == null));
            rules.Add(new SimpleOperation<Salesman>("name", salesman => salesman.Name.Equals(string.Empty)));
            ISimpleValidator<Salesman> validator = new SimpleValidator<Salesman>(rules);
        }

        [Test]
        [Category("AgregateValidator")]
        public void TestAgregateValidate()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 0));

            ISimpleValidator<TradeContract> crtValidator = new SimpleValidator<TradeContract>(crtRules);

            List<ISimpleOperation<CarContract>> carRules = new List<ISimpleOperation<CarContract>>();
            carRules.Add(new SimpleOperation<CarContract>("BrandName", contract => contract.BrandName != null));

            ISimpleValidator<CarContract> carValidator = new AggregateValidator<CarContract, TradeContract>(carRules, crtValidator);

            CarContract car = new CarContract {Price = 1500, BrandName = "Porsche"};

            var res = carValidator.Validate(car);
            Assert.IsTrue(res.All(n => n.State == ResultState.Successfully));
        }


        [Test]
        [Category("AgregateValidator")]
        [ExpectedException(typeof(WrongParameterException))]
        public void FailedAgregateValidate()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 0));

            ISimpleValidator<TradeContract> crtValidator = new SimpleValidator<TradeContract>(crtRules);

            List<ISimpleOperation<CarContract>> carRules = new List<ISimpleOperation<CarContract>>();
            carRules.Add(new SimpleOperation<CarContract>("BrandName", contract => contract.BrandName != null));

            ISimpleValidator<CarContract> carValidator = new AggregateValidator<CarContract, TradeContract>(carRules, crtValidator);

            var res = carValidator.Validate(null);
            Assert.IsTrue(res.All(n => n.State == ResultState.Successfully));
        }

        [Test]
        [Category("AgregateValidator")]
        public void AgregateValidate2()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<TradeContract> crtValidator = new SimpleValidator<TradeContract>(crtRules);

            List<ISimpleOperation<CarContract>> carRules = new List<ISimpleOperation<CarContract>>();
            carRules.Add(new SimpleOperation<CarContract>("BrandName", contract => contract.BrandName != null));

            ISimpleValidator<CarContract> carValidator = new AggregateValidator<CarContract, TradeContract>(carRules, crtValidator);

            CarContract car = new CarContract {Price = 55000, BrandName = "Porsche"};

            var res = carValidator.Validate(car);
            Assert.IsTrue(res.Any(n => n.State != ResultState.Successfully));
        }

        [Test]
        [Category("AgregateValidator")]
        public void AgregateValidate3()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<TradeContract> crtValidator = new SimpleValidator<TradeContract>(crtRules);

            List<ISimpleOperation<CarContract>> carRules = new List<ISimpleOperation<CarContract>>();
            carRules.Add(new SimpleOperation<CarContract>("BrandName", contract => contract.BrandName != null));

            ISimpleValidator<CarContract> carValidator = new AggregateValidator<CarContract, TradeContract>(carRules, crtValidator);

            CarContract car = new CarContract { Price = 55000, BrandName = null };

            var res = carValidator.Validate(car);
            Assert.IsTrue(res.All(n => n.State == ResultState.Unsuccessfully));
        }


        [Test]
        [Category("InferredValidator")]
        public void InferredValidatorTest1()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>(crtRules);

            CarContract car = new CarContract { Price = 70001, BrandName = null };

            var res = crtValidator.Validate(car);
            Assert.IsTrue(res.All(n => n.State == ResultState.Successfully));

        }

        [Test]
        [Category("InferredValidator")]
        [ExpectedException(typeof(ValidationParameterException))]
        public void FailedInferredValidator1()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>(crtRules);

            var res = crtValidator.Validate(null);
            Assert.IsTrue(res.All(n => n.State == ResultState.Successfully));

        }

        [Test]
        [Category("InferredValidator")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedInferredValidator2()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>((List<ISimpleOperation<TradeContract>>)null);
        }

        [Test]
        [Category("InferredValidator")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedInferredValidator3()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>(crtRules);
        }


        [Test]
        [Category("InferredValidator")]
        [ExpectedException(typeof(OnBuildingValidatorException))]
        public void FailedInferredValidator4()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));

            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>((ISimpleValidator<TradeContract>)null);
        }

        [Test]
        [Category("InferredValidator")]
        public void InferredValidator1()
        {
            List<ISimpleOperation<TradeContract>> crtRules = new List<ISimpleOperation<TradeContract>>();
            crtRules.Add(new SimpleOperation<TradeContract>("Price", contract => contract.Price > 70000));
            SimpleValidator<TradeContract> crt = new SimpleValidator<TradeContract>(crtRules);

            ISimpleValidator<CarContract> crtValidator = new InferredValidator<CarContract, TradeContract>(crt);

            CarContract car = new CarContract { Price = 70001, BrandName = null };

            var res = crtValidator.Validate(car);
            Assert.IsTrue(res.All(n => n.State == ResultState.Successfully));
        }
    }
}

using System;

namespace Validations.Test.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TradeContract
    {
        private long? number;
        private string description;
        private Salesman owner;
        private DateTime? beginDate;
        private int? price;

        protected TradeContract()
        {
            owner = null;
            description = null;
            number = null;
        }

        public virtual long? Number
        {
            set { this.number = value; }
            get { return this.number; }
        }

        public virtual string Description
        {
            set { this.description = value; }
            get { return this.description; }
        }

        public virtual Salesman Owner
        {
            set { this.owner = value; }
            get { return this.owner; }
        }

        public virtual DateTime? BeginDate
        {
            set { this.beginDate = value; }
            get { return this.beginDate; }
        }


        public virtual int? Price
        {
            get { return price; }
            set { this.price = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract double ComputeReward();

    }
}

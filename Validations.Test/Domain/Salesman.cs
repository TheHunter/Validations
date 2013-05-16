using System;
using System.Collections.Generic;
using System.Linq;

namespace Validations.Test.Domain
{
    [Serializable]
    public class Salesman
    {
        private string name;
        private string surname;
        private string email;
        private int? identityCode;
        private HashSet<Salesman> agents;
        private HashSet<Agency> agencies;
        private HashSet<TradeContract> contracts;

        public Salesman() { }

        public Salesman(bool isTransient)
        {
            this.contracts = new HashSet<TradeContract>();
            this.agents = new HashSet<Salesman>();
            this.agencies = new HashSet<Agency>();
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public virtual string Email
        {
            get { return email; }
            set { this.email = value; }
        }

        public virtual int? IdentityCode
        {
            get { return this.identityCode; }
            set { this.identityCode = value; }
        }

        public virtual IEnumerable<Salesman> Agents
        {
            get { return agents; }
        }

        public virtual IEnumerable<Agency> Agencies
        {
            get { return agencies; }
        }

        public virtual IEnumerable<TradeContract> Contracts
        {
            get { return contracts; }
        }

        public void AddContract(TradeContract contract)
        {
            this.contracts.Add(contract);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Salesman)
                return this.GetHashCode() == obj.GetHashCode();
            return false;
        }

        public override int GetHashCode()
        {
            return (13 * (this.IdentityCode == null ? 0 : this.IdentityCode.Value));
        }

        public override string ToString()
        {
            return string.Format("IDCode: {0}, Name: {1}, Surname: {2}, SubAgents: {3}", this.IdentityCode, this.Name, this.Surname, this.Agents == null ? 0 : this.Agents.Count());
        }
    }
}

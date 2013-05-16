using System;

namespace Validations.Test.Domain
{
    [Serializable]
    public class Agency
    {
        private long id;
        private string name;
        private long idManager;

        public Agency()
        {
            idManager = 0;
            id = 0;
            name = null;
        }

        public virtual long ID
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public virtual string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }

        public virtual long IDManager
        {
            set { this.idManager = value; }
            get { return this.idManager; }
        }
    }
}

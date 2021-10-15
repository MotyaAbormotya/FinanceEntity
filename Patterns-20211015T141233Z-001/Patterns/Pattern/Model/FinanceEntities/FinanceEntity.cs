using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Model.FinanceEntities
{
    public abstract class FinanceEntity : IReadOnlyFinanceEntity
    {
        private static int _id;

        public int ID { get; private set; }
        public int Summ { get; private set; }
        public Category Category { get; set; }
        public DateTime DateTime { get; private set; }
        
        static FinanceEntity()
        {
            _id = 0;
        }

        public FinanceEntity(int summ, Category category, DateTime dateTime)
        {
            ID = ++_id;
            Summ = summ;
            Category = category;
            DateTime = dateTime;
        }

        public void Update(int summ = -1, Category category = null, DateTime dateTime = default)
        {
            if (summ != -1)
                Summ = summ;
            if(Category != null)
                Category = category;
        }
    }

    public interface IReadOnlyFinanceEntity
    {
        int ID { get; }
        int Summ { get; }
        Category Category { get; }
        DateTime DateTime { get; }
    }
}

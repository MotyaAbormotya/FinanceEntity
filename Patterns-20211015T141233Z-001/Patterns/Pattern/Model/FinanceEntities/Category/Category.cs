using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Model.FinanceEntities
{
    public class Category : IReadOnlyCategory
    {
        private static int _id;

        public int ID { get; private set; }
        public string Title { get; private set; }

        static Category()
        {
            _id = 0;
        }

        public Category(string title)
        {
            ID = ++_id;
            Title = title;
        }
        public void Update(string title = "")
        {
            if (title != "")
                Title = title;
        }
    }

    public interface IReadOnlyCategory
    {
        int ID { get;}
        string Title { get; }
    }
}

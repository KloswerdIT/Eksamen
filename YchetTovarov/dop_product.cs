using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YchetTovarov
{
    public partial class product
    {
        public string category
        {
            get
            {
                var cat = App.DB.category.FirstOrDefault(p => p.id == id_category);
                return cat.name;
            }
        }
    }
    
}

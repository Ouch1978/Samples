using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Wpf_ObjectDataProvider
{
    public class Products 
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>
        {
            new Product{ Id =  1 , Name = "LG GW910" , OS = "WP7" },
            new Product{ Id =  2 , Name = "SonyEricsson X10" , OS = "Android" },
            new Product{ Id =  3 , Name = "HTC HD7" , OS = "WP7" },
            new Product{ Id =  4 , Name = "iPhone 4" , OS = "iOS" },
            new Product{ Id =  5 , Name = "Samsung Focus" , OS = "WP7" },
        };

        public List<Product> GetWindowPhones()
        {
            return _products.Where( p => p.OS == "WP7" ).ToList();
        }

        public List<Product> GetAndroidPhones()
        {
            return _products.Where( p => p.OS == "Android" ).ToList();

        }
    }
}

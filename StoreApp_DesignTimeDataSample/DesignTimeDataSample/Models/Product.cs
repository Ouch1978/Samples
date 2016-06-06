using System;
using Windows.UI.Xaml.Media.Imaging;

namespace DesignTimeDataSample.Models
{
    public class Product
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal OriginalPrice
        {
            get;
            set;
        }

        public decimal DiscountedPrice
        {
            get;
            set;
        }

        public int Inventory
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool IsAvailable
        {
            get; 
            set;
        }

        public DateTime DiscountBeginTime
        {
            get; 
            set;
        }

        public BitmapImage Image
        {
            get;
            set;
        }

        private string _imageUri;
        public string ImageUri
        {
            get
            {
                return _imageUri;
            }
            set
            {
                _imageUri = value;
                Image.UriSource = new Uri( value );
            }
        }
    }
}
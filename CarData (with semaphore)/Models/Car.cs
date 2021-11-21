using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarData__with_semaphore_
{
    [Newtonsoft.Json.JsonObject(Title = "root")]
    public class Car : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnpropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public Car()
        {

        }

        private string _CarModel { get; set; }

        public string CarModel
        {
            get { return _CarModel; }
            set { _CarModel = value; OnpropertyChanged(); }
        }

        private string _CarVendor { get; set; }

        public string CarVendor
        {
            get { return _CarVendor; }
            set { _CarVendor = value; OnpropertyChanged(); }
        }

        private long _CarYear { get; set; }

        public long CarYear
        {
            get { return _CarYear; }
            set { _CarYear = value; OnpropertyChanged(); }
        }

        public string FullData => $" {CarModel}  {CarVendor}  {CarYear} ";

        public override string ToString()
        {
            return FullData;
        }
    }
}

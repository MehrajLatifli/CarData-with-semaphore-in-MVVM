using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarData__with_semaphore_
{
    public class CarlistRoot : INotifyPropertyChanged
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


        private ObservableCollection<Car> _Cars { get; set; }

        public ObservableCollection<Car> Cars
        {
            get { return _Cars; }
            set { _Cars = value; OnpropertyChanged(); }
        }


        public CarlistRoot()
        {
            Cars = new ObservableCollection<Car>();
        }

        public void Add(string carmodel, string carvendor, long caryear)
        {

            Cars.Add(new Car
            {

                CarModel = carmodel,
                CarVendor=carvendor,
                CarYear=caryear


            });

        }









    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestUI.Controls
{
    using OxyPlot;
    using OxyPlot.Series;

    public class PlotterViewModel
    {
        private Func<double, double> _Function = (double x) => { return x; };

        public PlotterViewModel()
        {
            this.MyModel = new PlotModel { Title = "Plot" };
        }

        public Func<double, double> Function
        {
            get
            {
                return _Function;
            }
            set
            {
                this.MyModel.Series.Clear();
                this.MyModel.Series.Add(new FunctionSeries(value, 0, 20, 0.1, ""));
                _Function = value;
            }
        } 

        public PlotModel MyModel { get; private set; }
        
    }
}
